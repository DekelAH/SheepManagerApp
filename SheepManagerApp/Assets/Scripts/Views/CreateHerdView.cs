using Assets.Scripts.DTO;
using Assets.Scripts.Infrastructure;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateHerdView : MonoBehaviour
{
    #region Editor

    [SerializeField]
    private TMP_InputField _herdName;

    [SerializeField]
    private Button _submitBtn;

    #endregion

    #region Fields

    private bool _isHerdNameValid;

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

    public void OnSubmitBtnClick()
    {
        SetCreateNewHerdRequest();
    }

    public void IsHerdNameValid()
    {
        var givenHerdName = _herdName.text;
        if (givenHerdName.Any(char.IsLetter) && givenHerdName.Length >= 3)
        {
            _isHerdNameValid = true;
            Debug.Log("Allow HerdName");
        }
        else
        {
            _isHerdNameValid = false;
            Debug.Log("Dont Allow HerdName");
        }
    }

    private async void SetCreateNewHerdRequest()
    {
        HerdAddRequest newHerd = new()
        {
            herdName = _herdName.text,
        };

        await ApplicationDataManager.Instance.SendNewHerdToServer(newHerd);
        SectionHandler.LoadSection("Herd Data");
    }



    private void CheckSubmitBtnStatus()
    {
        if (_isHerdNameValid)
        {
            _submitBtn.enabled = true;
        }
        else
        {
            _submitBtn.enabled = false;
        }
    }

    #endregion
}
