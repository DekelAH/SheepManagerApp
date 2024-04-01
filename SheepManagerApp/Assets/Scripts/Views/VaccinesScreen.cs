using Assets.Scripts.DTO;
using Assets.Scripts.Infrastructure.Providers;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.SheepDataBtns;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class VaccinesScreen : MonoBehaviour
    {
        #region Fields

        private List<VaccineResponse> _vaccineResponseList;

        #endregion

        #region Editor

        [SerializeField]
        private VaccineDataButton _vaccineDataButtonPrefabRef;

        [SerializeField]
        private RectTransform _panelRectTransform;

        #endregion

        #region Methods

        private void Start()
        {
            CreateBtns(UserDataProvider.Instance.Get.HerdModel.Vaccines);
        }

        public void OnCloseBtnClick()
        {
            SectionHandler.LoadSection(SceneNameProvider.GetMainMenuName);
        }

        public void OnAddBtnClick()
        {
            SectionHandler.LoadSection(SceneNameProvider.GetAddSheepScreenName);
        }

        private void CreateBtns(List<VaccineResponse> vaccineResponseList)
        {
            _vaccineResponseList = vaccineResponseList;
            if (vaccineResponseList is not null && vaccineResponseList.Count > 0)
            {
                foreach (var vaccineResponse in _vaccineResponseList)
                {
                    _vaccineDataButtonPrefabRef.SetTagNumberValue(vaccineResponse.sheepTagNumber);
                    Instantiate(_vaccineDataButtonPrefabRef, _panelRectTransform);
                }
            }
        }

        #endregion
    }
}
