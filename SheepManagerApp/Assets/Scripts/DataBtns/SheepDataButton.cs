
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Providers;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.SheepDataBtns
{
    public class SheepDataButton : MonoBehaviour
    {
        #region Editor

        [SerializeField]
        private GameObject _btn;

        [SerializeField]
        private TextMeshProUGUI _btnTxt;

        #endregion

        #region Fields

        [SerializeField]
        private int _tagNumber;

        #endregion

        #region Methods

        public void SetTagNumberValue(int tagNumber)
        {
            _btnTxt.text = "#" + tagNumber.ToString();
            _tagNumber = tagNumber;
        }

        public void OnClickButton()
        {
            ApplicationDataManager.SetCurrentSheepDataView(_tagNumber);
            SectionHandler.LoadSection(SceneNameProvider.GetSheepDataScreenName);
        }

        #endregion
    }
}
