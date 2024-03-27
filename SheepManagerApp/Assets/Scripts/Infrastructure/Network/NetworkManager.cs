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
        private const string REGISTER_SERVER_ADDRESS = "https://localhost:7035/api/account/register";
        private const string LOGIN_SERVER_ADDRESS = "https://localhost:7035/api/account/login";

        #endregion

        #region AccountWebRequestsMethods

        public static async UniTask<RegisterResponse> CreateUser(RegisterRequest registerRequest)
        {
            return await CreateUserWebRequest(registerRequest);
        }

        private static async UniTask<RegisterResponse> CreateUserWebRequest(RegisterRequest registerRequest)
        {
            var json = JsonUtility.ToJson(registerRequest);
            using (var request = UnityWebRequest.Post(REGISTER_SERVER_ADDRESS, json, "application/json"))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                var response = await request.SendWebRequest();
                if (!string.IsNullOrEmpty(response.error))
                {
                    throw new Exception("Error accured while sending CreateUser post request.");
                }

                var responseJson = response.downloadHandler.text;
                RegisterResponse registerResponse = JsonUtility.FromJson<RegisterResponse>(responseJson);

                return registerResponse;
            }
        }

        #endregion

        #region HerdWebRequestsMethods

        public static async UniTask<HerdResponse> GetHerd(Guid herdId)
        {
            return await GetHerdWebRequest(herdId);
        }

        public static async UniTask<HerdResponse> UpdateHerd(HerdResponse herdToUpdate)
        {
            return await UpdateHerdWebRequest(herdToUpdate);
        }

        private static async UniTask<HerdResponse> UpdateHerdWebRequest(HerdResponse herdToUpdate)
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
