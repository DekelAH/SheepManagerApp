using Assets.Scripts.DTO;
using Assets.Scripts.Infrastructure.Network;
using Assets.Scripts.Infrastructure.Providers;
using Assets.Scripts.StorageSystem;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class HerdDataManager
    {
        #region Fields

        private static HerdDataManager _instance;

        private static readonly string _herdId = "c4ad98db-2b1d-4fd5-9427-1dcd4a01a581";

        private static readonly LocalFileStorageSystem _localFileStorageSystem = new();

        private static int _currentSheepDataViewTagNumber;

        #endregion


        #region Methods

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private async static void CreateInstance()
        {
            _instance = new HerdDataManager();
            Debug.Log("<----- HerdDataManager Created ----->");
            LoadHerdData();
            await SetHerdModel();

            SubsribeToEvents();
        }

        private static void SubsribeToEvents()
        {
            HerdDataProvider.Instance.Get.DataChange += OnDataBalanceChange;
        }

        private static async UniTask SetHerdModel()
        {
            var herdModel = await NetworkManager.GetHerd(Guid.Parse(_herdId));
            HerdDataProvider.Instance.Get.InitializeHerdData(herdModel.herdId, herdModel.herdName,
                                                             herdModel.herdSheeps, herdModel.matches);
            SaveHerdData();
        }

        private static void OnDataBalanceChange()
        {
            SaveHerdData();
        }

        private static void SaveHerdData()
        {
            switch (HerdDataProvider.Instance.Get.HerdModelSaveName)
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
            switch (HerdDataProvider.Instance.Get.HerdModelSaveName)
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

        public static async void EditSheep(SheepUpdateRequest sheepAddRequest)
        {
            var sheepResponse = await NetworkManager.EditSheep(sheepAddRequest);
            HerdDataProvider.Instance.Get.UpdateSheep(sheepResponse);
        }

        public static async void AddSheep(SheepAddRequest addRequest)
        {
            addRequest.herdId = _herdId;
            var sheepResponse = await NetworkManager.AddSheep(addRequest);
            HerdDataProvider.Instance.Get.AddSheepToHerd(sheepResponse);
        }

        public static void SetCurrentSheepDataView(int currentSheepDataViewTagNumber)
        {
            _currentSheepDataViewTagNumber = currentSheepDataViewTagNumber;
        }

        #endregion

        #region Properties

        public static HerdDataManager Instance => _instance;
        public static int CurrentSheepDataViewTagNumber => _currentSheepDataViewTagNumber;
        public static string HerdId => _herdId;
        public static HerdResponse Herd => HerdDataProvider.Instance.Get.ToHerdResponse();

        #endregion
    }
}
