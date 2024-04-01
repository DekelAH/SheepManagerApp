using Assets.Scripts.Infrastructure.Providers;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.StorageSystem
{
    public class LocalFileStorageSystem : StorageSystem
    {
        #region Methods

        #region User Data File Actions
        protected override void LoadUserInternal()
        {
            if (File.Exists(Application.dataPath + "/saveUser.txt"))
            {
                string json = File.ReadAllText(Application.dataPath + "/saveUser.txt");
                JsonUtility.FromJsonOverwrite(json, UserDataProvider.Instance.Get);
                Debug.Log(UserDataProvider.Instance.Get.HerdModel.HerdName);
                UserDataProvider.Instance.Get.InitializeUserData(
                    UserDataProvider.Instance.Get.UserId,
                    UserDataProvider.Instance.Get.PersonName,
                    UserDataProvider.Instance.Get.Email,
                    UserDataProvider.Instance.Get.HerdId
                    );

                Debug.Log("<--- Loaded User From Local JSON File --->");
            }
            else
            {
                Debug.LogError("<--- Didn't Load User From Local JSON File --->");
            }
        }

        protected override void SaveUserInternal()
        {
            string json = JsonUtility.ToJson(UserDataProvider.Instance.Get);
            File.WriteAllText(Application.dataPath + "/saveUser.txt", json);

            Debug.Log("<--- Saved User In Local JSON File --->");
        }
        #endregion

        #region Herd Data File Actions
        protected override void LoadHerdInternal()
        {
            if (File.Exists(Application.dataPath + "/saveHerd.txt"))
            {
                string json = File.ReadAllText(Application.dataPath + "/saveHerd.txt");
                JsonUtility.FromJsonOverwrite(json, UserDataProvider.Instance.Get.HerdModel);
                Debug.Log(UserDataProvider.Instance.Get.HerdModel.HerdName);
                UserDataProvider.Instance.Get.HerdModel.InitializeHerdData(
                    UserDataProvider.Instance.Get.HerdModel.HerdId,
                    UserDataProvider.Instance.Get.HerdModel.HerdName,
                    UserDataProvider.Instance.Get.HerdModel.Sheeps,
                    UserDataProvider.Instance.Get.HerdModel.Matches,
                    UserDataProvider.Instance.Get.HerdModel.Vaccines
                    );

                Debug.Log("<--- Loaded Herd From Local JSON File --->");
            }
            else
            {
                Debug.LogError("<--- Didn't Load Herd From Local JSON File, File Does'nt Exist --->");
            }
        }

        protected override void SaveHerdInternal()
        {
            string json = JsonUtility.ToJson(UserDataProvider.Instance.Get.HerdModel);
            File.WriteAllText(Application.dataPath + "/saveHerd.txt", json);

            Debug.Log("<--- Saved Herd In Local JSON File --->");
        }
        #endregion

        #endregion
    }
}
