using Assets.Scripts.DTO;
using Assets.Scripts.Infrastructure.Managers;
using Assets.Scripts.Infrastructure.Network;
using Assets.Scripts.Infrastructure.Providers;
using Assets.Scripts.Matches;
using Assets.Scripts.StorageSystem;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class ApplicationDataManager
    {
        #region Fields

        private static ApplicationDataManager _instance;
        private static readonly LocalFileStorageSystem _localFileStorageSystem = new();

        private static int _currentSheepDataViewTagNumber;
        private static int _serverDataVersion;

        #endregion


        #region Methods

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void CreateInstance()
        {
            _instance = new ApplicationDataManager();
            Debug.Log("<----- HerdDataManager Created ----->");

            SubsribeToEvents();
        }

        private static void SubsribeToEvents()
        {
            UserDataProvider.Instance.Get.HerdModel.DataChange += OnDataBalanceChange;
        }

        private static async UniTask GetHerdFromServer()
        {
            if (!UserSessionManager.Instance.IsOffileMod && UserDataProvider.Instance.Get.DataVersion < _serverDataVersion)
            {
                var herdResponse = await NetworkManager.GetHerd(Guid.Parse(UserDataProvider.Instance.Get.HerdId));
                var allMales = await GetMaleSheeps(herdResponse.herdSheeps);
                var allFemales = await GetFemaleSheeps(herdResponse.herdSheeps);

                UserDataProvider.Instance.Get.HerdModel.InitializeHerdData(herdResponse.herdId, herdResponse.herdName,
                                                                 herdResponse.herdSheeps, await SetMatches(allMales, allFemales),
                                                                 herdResponse.vaccines);

                SaveHerdData();
            }
        }

        private async static void OnDataBalanceChange()
        {
            var herdModel = UserDataProvider.Instance.Get.HerdModel;
            var allMales = await GetMaleSheeps(herdModel.Sheeps);
            var allFemales = await GetFemaleSheeps(herdModel.Sheeps);

            UserDataProvider.Instance.Get.HerdModel.InitializeHerdData(herdModel.HerdId, herdModel.HerdName,
                                                 herdModel.Sheeps, await SetMatches(allMales, allFemales), herdModel.Vaccines);
            SaveHerdData();
        }

        private static void SaveUserData()
        {
            switch (UserDataProvider.Instance.Get.ModelSaveName)
            {
                case "Local File":
                    _localFileStorageSystem.SaveUser(); 
                    break;
                case "PlayerPrefs":

                    break;
                default:
                    break;
            }
        }

        private static void LoadUserData()
        {
            switch (UserDataProvider.Instance.Get.ModelSaveName)
            {
                case "Local File":
                    _localFileStorageSystem.LoadUser();
                    break;
                case "PlayerPrefs":

                    break;
                default:
                    break;
            }
        }

        private static void SaveHerdData()
        {
            switch (UserDataProvider.Instance.Get.ModelSaveName)
            {
                case "Local File":
                    _localFileStorageSystem.SaveHerd();
                    break;
                case "Player Prefs":

                    break;
                default:
                    break;
            }
        }

        private static void LoadHerdData()
        {
            switch (UserDataProvider.Instance.Get.ModelSaveName)
            {
                case "Local File":
                    _localFileStorageSystem.LoadHerd();
                    break;
                case "Player Prefs":

                    break;
                default:
                    break;
            }
        }

        private static async UniTask<List<MatchResponse>> SetMatches(List<SheepResponse> allMales, List<SheepResponse> allFemales)
        {
            return await MatchesCreator.CreateMatches(allMales, allFemales);
        }

        private static UniTask<List<SheepResponse>> GetMaleSheeps(List<SheepResponse> allSheeps)
        {
            var maleSheeps = allSheeps.Where(s => s.gender == "Male").ToList();
            return UniTask.FromResult(maleSheeps);
        }

        private static UniTask<List<SheepResponse>> GetFemaleSheeps(List<SheepResponse> allSheeps)
        {
            var femaleSheeps = allSheeps.Where(s => s.gender == "Female").ToList();
            return UniTask.FromResult(femaleSheeps);
        }

        public async UniTask SendNewHerdToServer(HerdAddRequest newHerd)
        {
            var newHerdResponse = await NetworkManager.CreateHerd(newHerd);
            if (newHerdResponse is not null)
            {
                var sheepResponseList = new List<SheepResponse>();
                var herdMatches = new List<MatchResponse>();
                var vaccineResponseList = new List<VaccineResponse>();
                UserDataProvider.Instance.Get.HerdModel.InitializeHerdData
                    (
                        newHerdResponse.herdId,
                        newHerdResponse.herdName,
                        sheepResponseList,
                        herdMatches,
                        vaccineResponseList
                    );
                UserDataProvider.Instance.Get.SetHerdId(newHerdResponse.herdId);
            }
        }

        public async UniTask<bool> RegisterUser(RegisterRequest registerRequest)
        {
            try
            {
                var registerResponse = await NetworkManager.CreateUser(registerRequest);
                if (registerResponse is not null)
                {
                    UserDataProvider.Instance.Get.InitializeUserData(registerResponse.userId, registerResponse.personName,
                                                                 registerResponse.email, registerResponse.herdId);
                    SaveUserData();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                Debug.LogError("Can't connect to server, reach the developer.");
            }

            return false;
        }

        public async UniTask<bool> LoginRequestToServer(LoginRequest loginRequest)
        {
            try
            {
                if (UserSessionManager.Instance.HasInternetConnection)
                {
                    var loginResponse = await NetworkManager.Login(loginRequest);
                    if (loginResponse is not null)
                    {
                        UserDataProvider.Instance.Get.InitializeUserData(loginResponse.userId, loginResponse.personName,
                                                                         loginResponse.email, loginResponse.herdId);
                        await GetHerdFromServer();
                        LoadUserData();
                        LoadHerdData();
                        return true;
                    }
                }
                else
                {
                    Debug.LogWarning("Please check your WIFI connection");
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                Debug.LogError("Can't connect to server, reach the developer.");
            }

            return false;
        }

        public static async UniTask<bool> LogoutRequestToServer()
        {
            return await NetworkManager.Logout();
        }

        public static async UniTask UpdateHerdToServer()
        {
            if (UserSessionManager.Instance.IsUserLoggedIn)
            {
                var herd = UserDataProvider.Instance.Get.HerdModel;
                HerdResponse herdResponse = new()
                {
                    herdId = herd.HerdId,
                    herdName = herd.HerdName,
                    herdSheeps = herd.Sheeps,
                };

                UserDataProvider.Instance.Get.IncrementDataVersion();
                await NetworkManager.UpdateHerd(herdResponse);
            }
        }

        public static void EditSheep(SheepUpdateRequest sheepUpdateRequest)
        {
            UserDataProvider.Instance.Get.HerdModel.UpdateSheep(sheepUpdateRequest);
        }

        public static void AddSheep(SheepAddRequest sheepAddRequest)
        {
            sheepAddRequest.herdId = UserDataProvider.Instance.Get.HerdId;
            UserDataProvider.Instance.Get.HerdModel.AddSheepToHerd(sheepAddRequest);
        }

        public static void SetCurrentSheepDataView(int currentSheepDataViewTagNumber)
        {
            _currentSheepDataViewTagNumber = currentSheepDataViewTagNumber;
        }

        #endregion

        #region Properties

        public static ApplicationDataManager Instance => _instance;
        public static int CurrentSheepDataViewTagNumber => _currentSheepDataViewTagNumber;
        public static string HerdId => UserDataProvider.Instance.Get.HerdId;
        public static HerdResponse Herd => UserDataProvider.Instance.Get.HerdModel.ToHerdResponse();

        #endregion
    }
}
