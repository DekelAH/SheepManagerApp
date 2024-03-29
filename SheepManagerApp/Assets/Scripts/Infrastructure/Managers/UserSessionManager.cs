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
            StartCoroutine(CheckInternetConnection(isConnected =>
            {
                if (isConnected)
                {
                    _hasInternetConnection = true;
                    Debug.Log("Internet Available!");
                }
                else
                {
                    _hasInternetConnection= false;
                    Debug.Log("Internet Not Available");
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

        #endregion
    }
}
