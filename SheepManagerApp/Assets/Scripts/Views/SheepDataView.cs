using Assets.Scripts.DTO;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Providers;
using Cysharp.Threading.Tasks;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SheepDataView : MonoBehaviour
{
    #region Editor

    [SerializeField]
    private TextMeshProUGUI _tagNumber;

    [SerializeField]
    private TextMeshProUGUI _weight;

    [SerializeField]
    private TextMeshProUGUI _gender;

    [SerializeField]
    private TextMeshProUGUI _bloodType;

    [SerializeField]
    private TextMeshProUGUI _race;

    [SerializeField]
    private TextMeshProUGUI _selection;

    [SerializeField]
    private TextMeshProUGUI _birthDate;

    [SerializeField]
    private TextMeshProUGUI _fatherTagNumber;

    [SerializeField]
    private TextMeshProUGUI _motherTagNumber;

    [SerializeField]
    private Toggle _pregnantCheckMarkBtn;

    [SerializeField]
    private Toggle _deadCheckMarkBtn;

    [SerializeField]
    private Toggle _soldCheckMarkBtn;

    #endregion

    #region Fields

    private SheepResponse _sheep;

    #endregion

    #region Methods

    private void Start()
    {
        SetSheep();
        SetSheepDataFields();
    }

    public void OnClickEditBtn()
    {
        SectionHandler.LoadSection(SceneNameProvider.GetEditSheepScreenName);
    }

    public void OnClickCloseBtn()
    {
        SectionHandler.LoadSection(SceneNameProvider.GetHerdScreenName);
    }

    private void SetToggleOff()
    {
        _pregnantCheckMarkBtn.interactable = false;
        _deadCheckMarkBtn.interactable = false;
        _soldCheckMarkBtn.interactable = false;
    }

    private async void SetSheep()
    {
        var tagNumber = ApplicationDataManager.CurrentSheepDataViewTagNumber;
        _sheep = await GetSheepByTag(tagNumber);
        _tagNumber.text = tagNumber.ToString();
    }

    private UniTask<SheepResponse> GetSheepByTag(int tagNumber)
    {
        var herdId = ApplicationDataManager.HerdId;
        var sheep = UserDataProvider.Instance.Get.HerdModel.Sheeps.FirstOrDefault(s => s.tagNumber == tagNumber);
        return UniTask.FromResult(sheep);
    }

    private void SetSheepDataFields()
    {
        SetToggleOff();

        if (_sheep != null)
        {
            _tagNumber.text = " #" + _tagNumber.text;
            _weight.text = _sheep.weight.ToString();
            _gender.text = _sheep.gender.ToString();
            _bloodType.text = _sheep.bloodType.ToString();
            _race.text = _sheep.race.ToString();
            _selection.text = _sheep.selection.ToString();
            _birthDate.text = _sheep.birthdate;
            _fatherTagNumber.text = _sheep.fatherTagNumber.ToString();
            _motherTagNumber.text = _sheep.motherTagNumber.ToString();
            _pregnantCheckMarkBtn.isOn = _sheep.isPregnant;
            _deadCheckMarkBtn.isOn = _sheep.isDead;
            _soldCheckMarkBtn.isOn = _sheep.isSold;
        }
    }

    #endregion
}
