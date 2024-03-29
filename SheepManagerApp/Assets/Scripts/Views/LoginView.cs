using Assets.Scripts.DTO;
using Assets.Scripts.Infrastructure.Managers;
using Assets.Scripts.Infrastructure;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginView : MonoBehaviour
{
    #region Editor

    [SerializeField]
    private TMP_InputField _email;

    [SerializeField]
    private TMP_InputField _password;

    [SerializeField]
    private Button _submitBtn;

    #endregion

    #region Fields

    private bool _isEmailValid;
    private bool _isPasswordValid;

    #endregion

    #region Methods

    private void Start()
    {
        _submitBtn.enabled = false;
    }

    private void FixedUpdate()
    {
        CheckSubmitBtnStatus();
    }

    private async void SetLoginRequest()
    {
        LoginRequest loginRequest = new()
        {
            email = _email.text,
            password = _password.text,
        };

        var isLoggedIn = await ApplicationDataManager.Instance.LoginRequestToServer(loginRequest);
        if (isLoggedIn)
        {
            UserSessionManager.Instance.SetIsUserLoggedIn(isLoggedIn);
            SectionHandler.LoadSection("Main Menu");
        }
        else
        {
            Debug.Log("User is not logged in");
        }
    }

    private void CheckSubmitBtnStatus()
    {
        if (_isEmailValid && _isPasswordValid)
        {
            _submitBtn.enabled = true;
        }
        else
        {
            _submitBtn.enabled = false;
        }
    }

    public void OnSubmitBtnClick()
    {
        SetLoginRequest();
    }

    public void IsEmailValid()
    {
        var givenEmail = _email.text;
        var emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        if (emailRegex.IsMatch(givenEmail))
        {
            _isEmailValid = true;
            Debug.Log("Allow Email");
        }
        else
        {
            _isEmailValid = false;
            Debug.Log("Dont Allow Email");
        }
    }

    public void IsPasswordValid()
    {
        var givenPassword = _password.text;
        if (givenPassword.Any(char.IsLetter) &&
            givenPassword.Any(char.IsDigit) &&
            givenPassword.Any(char.IsUpper) &&
            givenPassword.Length >= 8)
        {
            _isPasswordValid = true;
            Debug.Log("Allow Password");
        }
        else
        {
            _isPasswordValid = false;
            Debug.Log("Dont Allow Password");
        }
    }

    #endregion
}
