using Assets.Scripts.DTO;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Infrastructure.Network
{
    public static class NetworkManager
    {
        #region Consts

        // To Scriptable object
        private const string HERD_SERVER_ADDRESS = "https://localhost:7035/api/herds/";

        #endregion


        #region SheepsWebRequestsMethods

        public static async UniTask<HerdResponse> GetHerd(Guid herdId)
        {
            return await GetHerdWebRequest(herdId);
        }

        public static async UniTask<HerdResponse> UpdateHerd(HerdResponse herdToUpdate)
        {
            return await UpdateHerdRequest(herdToUpdate);
        }

        private static async UniTask<HerdResponse> UpdateHerdRequest(HerdResponse herdToUpdate)
        {
            var json = JsonUtility.ToJson(herdToUpdate);
            using (var request = UnityWebRequest.Put(HERD_SERVER_ADDRESS + $"{herdToUpdate.herdId}", json))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                var response = await request.SendWebRequest();
                if (!string.IsNullOrEmpty(response.error))
                {
                    throw new Exception("Error accured while sending UpdateHerd put request.");
                }

                var responseJson = response.downloadHandler.text;
                HerdResponse herdResponse = JsonUtility.FromJson<HerdResponse>(responseJson);

                return herdResponse;
            }
        }

        private static async UniTask<HerdResponse> GetHerdWebRequest(Guid herdId)
        {
            using (var request = UnityWebRequest.Get(HERD_SERVER_ADDRESS + $"{herdId}"))
            {
                var response = await request.SendWebRequest();
                if (!string.IsNullOrEmpty(response.error))
                {
                    throw new Exception("Error accured while sending GetHerd request.");
                }

                var responseJson = response.downloadHandler.text;
                HerdResponse herdResponse = JsonUtility.FromJson<HerdResponse>(responseJson);

                return herdResponse;
            }
        }

        #endregion
    }
}
