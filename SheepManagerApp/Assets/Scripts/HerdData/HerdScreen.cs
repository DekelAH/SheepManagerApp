using Assets.Scripts.DTO;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Providers;
using Assets.Scripts.SheepDataBtns;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.HerdData
{
    public class HerdScreen : MonoBehaviour
    {
        #region Fields

        private List<SheepResponse> _sheepResponseList;

        #endregion

        #region Editor

        [SerializeField]
        private SheepDataButton _sheepDataButtonPrefabRef;

        [SerializeField]
        private RectTransform _panelRectTransform;

        #endregion

        #region Methods

        private void Start()
        {
            CreateBtns(HerdDataManager.Herd.herdSheeps);
        }

        public void OnCloseBtnClick()
        {
            SectionHandler.LoadSection(SceneNameProvider.GetMainMenuName);
        }

        public void OnAddBtnClick()
        {
            SectionHandler.LoadSection(SceneNameProvider.GetAddSheepScreenName);
        }

        private void CreateBtns(List<SheepResponse> sheepResponseList)
        {
            _sheepResponseList = sheepResponseList;
            foreach (var sheepResponse in _sheepResponseList)
            {
                _sheepDataButtonPrefabRef.SetTagNumberValue(sheepResponse.tagNumber);
                Instantiate(_sheepDataButtonPrefabRef, _panelRectTransform);
            }
        }

        #endregion
    }
}
