using Assets.Scripts.DTO;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Providers;
using TMPro;
using UnityEngine;

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
    private TMP_InputField _pregnant;

    [SerializeField]
    private TMP_InputField _isDead;

    [SerializeField]
    private TMP_InputField _sold;

    #endregion

    #region Methods

    public void OnSubmitBtnClick()
    {
        var sheepAddRequest = SetSheepAddRequest();
        HerdDataManager.AddSheep(sheepAddRequest);
        SectionHandler.LoadSection(SceneNameProvider.GetHerdScreenName);
    }

    public void OnCloseBtnClick()
    {
        SectionHandler.LoadSection(SceneNameProvider.GetHerdScreenName);
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
            isDead = bool.Parse(_isDead.text.Trim()),
            isSold = bool.Parse(_sold.text.Trim()),
            isPregnant = bool.Parse(_pregnant.text.Trim())
        };
    }

    #endregion
}
