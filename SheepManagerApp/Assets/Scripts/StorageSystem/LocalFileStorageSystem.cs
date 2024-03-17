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
                JsonUtility.FromJsonOverwrite(json, HerdDataProvider.Instance.Get);
                HerdDataProvider.Instance.Get.InitializeHerdData(
                    HerdDataProvider.Instance.Get.HerdId,
                    HerdDataProvider.Instance.Get.HerdName,
                    HerdDataProvider.Instance.Get.Sheeps,
                    HerdDataProvider.Instance.Get.Matches
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
            string json = JsonUtility.ToJson(HerdDataProvider.Instance.Get);
            File.WriteAllText(Application.persistentDataPath + "/save.txt", json);

            Debug.Log("<--- Saved In Local JSON File --->");
        }

        #endregion
    }
}
