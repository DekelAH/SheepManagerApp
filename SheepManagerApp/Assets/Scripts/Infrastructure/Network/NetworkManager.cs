

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

        private const string HERD_SERVER_ADDRESS = "https://localhost:7035/api/herds/";
        private const string SHEEP_SERVER_ADDRESS = "https://localhost:7035/api/sheeps/";

        #endregion

        #region SheepsWebRequestsMethods

        public static async UniTask<HerdResponse> GetHerd(Guid herdId)
        {
            return await GetHerdWebRequest(herdId);
        }

        public static async UniTask<SheepResponse> AddSheep(SheepAddRequest sheepAddRequest)
        {
            return await AddSheepWebRequest(sheepAddRequest);
        }

        public static async UniTask<SheepResponse> EditSheep(SheepUpdateRequest sheepUpdateRequest)
        {
            return await EditSheepWebRequest(sheepUpdateRequest);
        }

        private static async UniTask<SheepResponse> EditSheepWebRequest(SheepUpdateRequest sheepUpdateRequest)
        {
            var json = JsonUtility.ToJson(sheepUpdateRequest);
            using (var request = UnityWebRequest.Put(SHEEP_SERVER_ADDRESS + $"{sheepUpdateRequest.sheepId}", json))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                var response = await request.SendWebRequest();
                if (!string.IsNullOrEmpty(response.error))
                {
                    throw new Exception("Error accured while sending EditSheep put request.");
                }

                var responseJson = response.downloadHandler.text;
                SheepResponse sheepResponse = JsonUtility.FromJson<SheepResponse>(responseJson);

                return sheepResponse;
            }
        }

        private static async UniTask<SheepResponse> AddSheepWebRequest(SheepAddRequest sheepAddRequest)
        {
            var json = JsonUtility.ToJson(sheepAddRequest);
            using (var request = UnityWebRequest.Post(SHEEP_SERVER_ADDRESS, json, "application/json"))
            {
                var response = await request.SendWebRequest();
                if (!string.IsNullOrEmpty(response.error))
                {
                    throw new Exception("Error accured while sending AddSheep post request.");
                }

                var responseJson = response.downloadHandler.text;
                SheepResponse sheepResponse = JsonUtility.FromJson<SheepResponse>(responseJson);

                return sheepResponse;
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
