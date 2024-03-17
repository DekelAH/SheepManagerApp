using TMPro;
using UnityEngine;

public class MatchDataContainer : MonoBehaviour
{
    #region Editor

    [SerializeField]
    private TextMeshProUGUI _femaleTagNumber;

    [SerializeField]
    private TextMeshProUGUI _maleTagNumber;

    #endregion

    #region Methods

    public void SetTagNumbers(int femaleTagNumber, int maleTagNumber)
    {
        _maleTagNumber.text = maleTagNumber.ToString();
        _femaleTagNumber.text = femaleTagNumber.ToString();
    }

    #endregion

    #region Properties

    public string FemaleTagNumber => _femaleTagNumber.text;
    public string MaleTagNumber => _maleTagNumber.text;

    #endregion
}
