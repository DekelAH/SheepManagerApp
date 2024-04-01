using Assets.Scripts.DTO;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Providers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddSheepView : MonoBehaviour
{
    #region Editor

    [SerializeField]
    private TMP_InputField _tagNumber;

    [SerializeField]
    private TMP_InputField _weight;

    [SerializeField]
    private TextMeshProUGUI _gender;

    [SerializeField]
    private TextMeshProUGUI _bloodType;

    [SerializeField]
    private TextMeshProUGUI _race;

    [SerializeField]
    private TextMeshProUGUI _selection;

    [SerializeField]
    private TMP_InputField _birthDate;

    [SerializeField]
    private TMP_InputField _fatherTagNumber;

    [SerializeField]
    private TMP_InputField _motherTagNumber;

    [SerializeField]
    private Toggle _pregnantCheckMarkBtn;

    [SerializeField]
    private Toggle _deadCheckMarkBtn;

    [SerializeField]
    private Toggle _soldCheckMarkBtn;

    #endregion

    #region Fields

    private bool _isDead;
    private bool _isSold;
    private bool _isPregnant;

    #endregion

    #region Methods

    public void OnSubmitBtnClick()
    {
        var sheepAddRequest = SetSheepAddRequest();
        ApplicationDataManager.AddSheep(sheepAddRequest);
        SectionHandler.LoadSection(SceneNameProvider.GetHerdScreenName);
    }

    public void OnCloseBtnClick()
    {
        SectionHandler.LoadSection(SceneNameProvider.GetHerdScreenName);
    }

    public void OnIsPregnantCheckMarkClick()
    {
        if (_pregnantCheckMarkBtn.isOn)
        {
            _isPregnant = true;
        }
        else
        {
            _isPregnant = false;
        }
    }

    public void OnIsDeadCheckMarkClick()
    {
        if (_deadCheckMarkBtn.isOn)
        {
            _isDead = true;
        }
        else
        {
            _isDead = false;
        }
    }

    public void OnIsSoldCheckMarkClick()
    {
        if (_soldCheckMarkBtn.isOn)
        {
            _isSold = true;
        }
        else
        {
            _isSold = false;
        }
    }

    private SheepAddRequest SetSheepAddRequest()
    {
        return new SheepAddRequest()
        {
            tagNumber = int.Parse(_tagNumber.text),
            weight = double.Parse(_weight.text),
            gender = _gender.text.Trim(),
            race = _race.text.Trim(),
            bloodType = _bloodType.text.Trim(),
            selection = _selection.text.Trim(),
            birthdate = _birthDate.text.Trim(),
            motherTagNumber = int.Parse(_motherTagNumber.text),
            fatherTagNumber = int.Parse(_fatherTagNumber.text),
            isDead = _isDead,
            isSold = _isSold,
            isPregnant = _isPregnant
        };
    }

    #endregion
}
