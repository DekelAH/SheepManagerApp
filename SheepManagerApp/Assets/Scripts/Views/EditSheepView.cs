using Assets.Scripts.DTO;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Providers;
using System.Linq;
using TMPro;
using UnityEngine;

public class EditSheepView : MonoBehaviour
{
    #region Editor

    [SerializeField]
    private TMP_InputField _tagNumberText;

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
    private TMP_InputField _isPregnantText;

    [SerializeField]
    private TMP_InputField _isDeadText;

    [SerializeField]
    private TMP_InputField _isSoldText;

    #endregion

    #region Fields

    private string _sheepId;
    private string _herdId;

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

    private void SetSheepFields()
    {
        var sheepToDisplay = ApplicationDataManager.Herd.herdSheeps.FirstOrDefault(s => s.tagNumber ==
                                                                             ApplicationDataManager.CurrentSheepDataViewTagNumber);
        _sheepId = sheepToDisplay.sheepId;
        _herdId = sheepToDisplay.herdId;
        _tagNumberText.text = "#" + sheepToDisplay.tagNumber.ToString();
        _weightText.text = sheepToDisplay.weight.ToString();
        _genderText.text = sheepToDisplay.gender;
        _bloodTypeText.text = sheepToDisplay.bloodType;
        _raceText.text = sheepToDisplay.race;
        _selectionText.text = sheepToDisplay.selection;
        _birthDateText.text = sheepToDisplay.birthdate.Trim();
        _fatherTagNumberText.text = sheepToDisplay.fatherTagNumber.ToString();
        _motherTagNumberText.text = sheepToDisplay.motherTagNumber.ToString();
        _isPregnantText.text = sheepToDisplay.isPregnant.ToString();
        _isDeadText.text = sheepToDisplay.isDead.ToString();
        _isSoldText.text = sheepToDisplay.isSold.ToString();
    }

    private SheepUpdateRequest SetSheepUpdateRequest()
    {
        return new SheepUpdateRequest()
        {
            sheepId = _sheepId.Trim(),
            herdId = _herdId.Trim(),
            tagNumber = int.Parse(_tagNumberText.text.Remove(0,1)),
            weight = double.Parse(_weightText.text),
            gender = _genderText.text.Trim(),
            race = _raceText.text.Trim(),
            bloodType = _bloodTypeText.text.Trim(),
            selection = _selectionText.text.Trim(),
            birthdate = _birthDateText.text.Trim(),
            motherTagNumber = int.Parse(_motherTagNumberText.text),
            fatherTagNumber = int.Parse(_fatherTagNumberText.text),
            isDead = bool.Parse(_isDeadText.text.Trim()),
            isSold = bool.Parse(_isSoldText.text.Trim()),
            isPregnant = bool.Parse(_isPregnantText.text.Trim())
        };
    }

    #endregion
}
