using Assets.Scripts.Infrastructure.Providers;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.StorageSystem
{
    public class LocalFileStorageSystem : StorageSystem
    {
        #region Methods

        protected override void LoadInternal()
        {
            if (File.Exists(Application.persistentDataPath + "/save.txt"))
            {
                string json = File.ReadAllText(Application.dataPath + "/save.txt");
                JsonUtility.FromJsonOverwrite(json, UserDataProvider.Instance.Get);
                UserDataProvider.Instance.Get.HerdModel.InitializeHerdData(
                    UserDataProvider.Instance.Get.HerdModel.HerdId,
                    UserDataProvider.Instance.Get.HerdModel.HerdName,
                    UserDataProvider.Instance.Get.HerdModel.Sheeps,
                    UserDataProvider.Instance.Get.HerdModel.Matches
                    );

                Debug.Log("<--- Loaded From Local JSON File --->");
            }
            else
            {
                Debug.LogError("<--- Didn't Load From Local JSON File --->");
            }
        }

        protected override void SaveInternal()
        {
            string json = JsonUtility.ToJson(UserDataProvider.Instance.Get);
            File.WriteAllText(Application.dataPath + "/save.txt", json);

            Debug.Log("<--- Saved In Local JSON File --->");
        }

        #endregion
    }
}
