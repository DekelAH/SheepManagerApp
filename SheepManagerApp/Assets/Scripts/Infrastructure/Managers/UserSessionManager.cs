using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Infrastructure.Managers
{
    public class UserSessionManager : MonoBehaviour
    {
        #region Editor

        [SerializeField]
        private bool _hasInternetConnection;

        [SerializeField]
        private bool _isUserLoggedIn;

        [SerializeField]
        private bool _isOfflineMod;

        #endregion

        #region Methods

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this);
            TriggerOnlineConnectionTest();
        }

        public void TriggerOnlineConnectionTest()
        {
            StartCoroutine(CheckInternetConnection(async isConnected =>
            {
                if (isConnected)
                {
                    _hasInternetConnection = true;
                    _isOfflineMod = false;
                    Debug.Log("Internet Available!");
                }
                else
                {
                    _hasInternetConnection = false;
                    _isOfflineMod = true;
                    if (await ApplicationDataManager.LogoutRequestToServer())
                    {
                        SetIsUserLoggedIn(false);
                    }
                    
                    Debug.Log("Internet Connection is Not Available");
                    Debug.Log("User is Logged-out");
                }
            }));
        }

        public void SetIsUserLoggedIn(bool isLoggedIn)
        {
            _isUserLoggedIn = isLoggedIn;
        }

        private IEnumerator CheckInternetConnection(Action<bool> action)
        {
            UnityWebRequest request = new("https://google.com");
            yield return request.SendWebRequest();
            if (request.error != null)
            {
                action(false);
            }
            else
            {
                action(true);
            }
        }

        #endregion

        #region Properties
        public static UserSessionManager Instance { get; private set; }
        public bool HasInternetConnection => _hasInternetConnection;
        public bool IsUserLoggedIn => _isUserLoggedIn;
        public bool IsOffileMod => _isOfflineMod;

        #endregion
    }
}
