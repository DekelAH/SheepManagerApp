using Assets.Scripts.DTO;
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

        private static readonly string _herdId = "c4ad98db-2b1d-4fd5-9427-1dcd4a01a581";

        private static readonly LocalFileStorageSystem _localFileStorageSystem = new();

        private static int _currentSheepDataViewTagNumber;

        #endregion


        #region Methods

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private async static void CreateInstance()
        {
            _instance = new ApplicationDataManager();
            Debug.Log("<----- HerdDataManager Created ----->");
            //await SetHerdModel();
            //LoadHerdData();

            //SubsribeToEvents();
        }

        private static void SubsribeToEvents()
        {
            UserDataProvider.Instance.Get.HerdModel.DataChange += OnDataBalanceChange;
        }

        private static async UniTask SetHerdModel()
        {
            var herdModel = await NetworkManager.GetHerd(Guid.Parse(_herdId));
            var allMales = await GetMaleSheeps(herdModel.herdSheeps);
            var allFemales = await GetFemaleSheeps(herdModel.herdSheeps);

            UserDataProvider.Instance.Get.HerdModel.InitializeHerdData(herdModel.herdId, herdModel.herdName,
                                                             herdModel.herdSheeps, await SetMatches(allMales, allFemales));

            SaveHerdData();
        }

        private static void OnDataBalanceChange()
        {
            SaveHerdData();
        }

        private static void SaveHerdData()
        {
            switch (UserDataProvider.Instance.Get.ModelSaveName)
            {
                case "Local File":
                    _localFileStorageSystem.Save();
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
                    _localFileStorageSystem.Load();
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

        public async UniTask RegisterUser(RegisterRequest registerRequest)
        {
            var registerResponse = await NetworkManager.CreateUser(registerRequest);
            if (registerResponse is not null)
            {
                UserDataProvider.Instance.Get.InitializeUserData(registerResponse.userId, registerResponse.personName,
                                                             registerResponse.email, registerResponse.herdId);
            }      
        }

        public static async UniTask UpdateHerdToServer()
        {
            var herd = UserDataProvider.Instance.Get.HerdModel;
            HerdResponse herdResponse = new()
            {
                herdId = herd.HerdId,
                herdName = string.Empty,
                herdSheeps = herd.Sheeps,
                matches = herd.Matches
            };

            await NetworkManager.UpdateHerd(herdResponse);
        }

        public static void EditSheep(SheepUpdateRequest sheepUpdateRequest)
        {
            UserDataProvider.Instance.Get.HerdModel.UpdateSheep(sheepUpdateRequest);
        }

        public static void AddSheep(SheepAddRequest sheepAddRequest)
        {
            sheepAddRequest.herdId = _herdId;
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
        public static string HerdId => _herdId;
        public static HerdResponse Herd => UserDataProvider.Instance.Get.HerdModel.ToHerdResponse();

        #endregion
    }
}
