using Assets.Scripts.DTO;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Providers;
using Cysharp.Threading.Tasks;
using System.Linq;
using TMPro;
using UnityEngine;

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
    private TextMeshProUGUI _pregnant;

    [SerializeField]
    private TextMeshProUGUI _dead;

    [SerializeField]
    private TextMeshProUGUI _sold;

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

    private async void SetSheep()
    {
        var tagNumber = HerdDataManager.CurrentSheepDataViewTagNumber;
        _sheep = await GetSheepByTag(tagNumber);
        _tagNumber.text = tagNumber.ToString();
    }

    private UniTask<SheepResponse> GetSheepByTag(int tagNumber)
    {
        var herdId = HerdDataManager.HerdId;
        var sheep = HerdDataProvider.Instance.Get.Sheeps.FirstOrDefault(s => s.tagNumber == tagNumber);
        return UniTask.FromResult(sheep);
    }

    private void SetSheepDataFields()
    {
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
            _pregnant.text = _sheep.isPregnant.ToString();
            _dead.text = _sheep.isDead.ToString();
            _sold.text = _sheep.isSold.ToString();
        }
    }

    #endregion
}
