using Assets.Scripts.DTO;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Managers;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignUpView : MonoBehaviour
{
    #region Editor

    [SerializeField]
    private TMP_InputField _userName;

    [SerializeField]
    private TMP_InputField _email;

    [SerializeField]
    private TMP_InputField _password;

    [SerializeField]
    private TMP_InputField _confirmPassword;

    [SerializeField]
    private Button _submitBtn;

    #endregion

    #region Fields

    private bool _isUserNameValid;
    private bool _isEmailValid;
    private bool _isPasswordValid;
    private bool _isConfirmPasswordValid;

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

    private async void SetRegisterRequest()
    {
        RegisterRequest registerRequest = new()
        {
            personName = _userName.text,
            email = _email.text,
            password = _password.text,
            confirmPassword = _confirmPassword.text
        };

        var isLoggedIn = await ApplicationDataManager.Instance.RegisterUser(registerRequest);
        if (isLoggedIn)
        {
            UserSessionManager.Instance.SetIsUserLoggedIn(isLoggedIn);
            SectionHandler.LoadSection("Create Herd");
        }
        else
        {
            Debug.Log("User is not logged in");
        }
    }

    private void CheckSubmitBtnStatus()
    {
        if (_isUserNameValid && _isEmailValid && _isPasswordValid && _isConfirmPasswordValid)
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
        SetRegisterRequest();
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

    public void IsUserNameValid()
    {
        var givenUserName = _userName.text;
        if (givenUserName.Any(char.IsLetter) && givenUserName.Length >= 3)
        {
            _isUserNameValid = true;
            Debug.Log("Allow UserName");
        }
        else
        {
            _isUserNameValid = false;
            Debug.Log("Dont Allow UserName");
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

    public void IsConfirmPasswordValid()
    {
        var givenConfirmPassword = _confirmPassword.text;
        var givenPassword = _password.text;

        if (givenConfirmPassword.Equals(givenPassword))
        {
            _isConfirmPasswordValid = true;
            Debug.Log("Allow Confirm Password");
        }
        else
        {
            _isConfirmPasswordValid = false;
            Debug.Log("Dont Allow Confirm Password");
        }
    }

    #endregion
}
