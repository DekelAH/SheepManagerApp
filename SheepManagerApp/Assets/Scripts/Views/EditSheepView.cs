using Assets.Scripts.DTO;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Providers;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditSheepView : MonoBehaviour
{
    #region Editor

    [SerializeField]
    private TextMeshProUGUI _tagNumberText;

    [SerializeField]
    private TMP_InputField _weightText;

    [SerializeField]
    private TextMeshProUGUI _genderText;

    [SerializeField]
    private TextMeshProUGUI _bloodTypeText;

    [SerializeField]
    private TextMeshProUGUI _raceText;

    [SerializeField]
    private TextMeshProUGUI _selectionText;

    [SerializeField]
    private TMP_InputField _birthDateText;

    [SerializeField]
    private TMP_InputField _fatherTagNumberText;

    [SerializeField]
    private TMP_InputField _motherTagNumberText;

    [SerializeField]
    private Toggle _pregnantCheckMarkBtn;

    [SerializeField]
    private Toggle _deadCheckMarkBtn;

    [SerializeField]
    private Toggle _soldCheckMarkBtn;

    #endregion

    #region Fields

    private string _sheepId;
    private string _herdId;

    private bool _isDead;
    private bool _isSold;
    private bool _isPregnant;

    #endregion

    #region Methods

    private void Start()
    {
        SetSheepFields();
    }

    public void OnSubmitBtnClick()
    {
        var sheepUpdateRequest = SetSheepUpdateRequest();
        ApplicationDataManager.EditSheep(sheepUpdateRequest);
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

    private void SetSheepFields()
    {
        var sheepToDisplay = ApplicationDataManager.Herd.herdSheeps.FirstOrDefault(s => s.tagNumber ==
                                                                             ApplicationDataManager.CurrentSheepDataViewTagNumber);
        _sheepId = sheepToDisplay.sheepId;
        _herdId = sheepToDisplay.herdId;
        _tagNumberText.text = sheepToDisplay.tagNumber.ToString();
        _weightText.text = sheepToDisplay.weight.ToString();
        _genderText.text = sheepToDisplay.gender;
        _bloodTypeText.text = sheepToDisplay.bloodType;
        _raceText.text = sheepToDisplay.race;
        _selectionText.text = sheepToDisplay.selection;
        _birthDateText.text = sheepToDisplay.birthdate.Trim();
        _fatherTagNumberText.text = sheepToDisplay.fatherTagNumber.ToString();
        _motherTagNumberText.text = sheepToDisplay.motherTagNumber.ToString();
        _isPregnant = sheepToDisplay.isPregnant;
        _isDead = sheepToDisplay.isDead;
        _isSold = sheepToDisplay.isSold;
    }

    private SheepUpdateRequest SetSheepUpdateRequest()
    {
        return new SheepUpdateRequest()
        {
            sheepId = _sheepId.Trim(),
            herdId = _herdId.Trim(),
            tagNumber = int.Parse(_tagNumberText.text),
            weight = double.Parse(_weightText.text),
            gender = _genderText.text.Trim(),
            race = _raceText.text.Trim(),
            bloodType = _bloodTypeText.text.Trim(),
            selection = _selectionText.text.Trim(),
            birthdate = _birthDateText.text.Trim(),
            motherTagNumber = int.Parse(_motherTagNumberText.text),
            fatherTagNumber = int.Parse(_fatherTagNumberText.text),
            isDead = _isDead,
            isSold = _isSold,
            isPregnant = _isPregnant
        };
    }

    #endregion
}
