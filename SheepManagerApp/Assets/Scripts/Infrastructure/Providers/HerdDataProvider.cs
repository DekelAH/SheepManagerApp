using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Providers
{
    public class HerdDataProvider
    {
        #region Consts

        private const string SAVE_SELECTOR_RESOURCE_NAME = "SaveSelector";

        #endregion

        #region Fields

        private static HerdDataProvider _instance;
        private static SaveSelector _saveSelector;

        #endregion

        #region Constructor

        private HerdDataProvider(string saveSelectorResourceName)
        {
            _saveSelector = Resources.Load<SaveSelector>(saveSelectorResourceName);
        }

        #endregion

        #region Properties

        public static HerdDataProvider Instance
        {
            get
            {
                _instance ??= new HerdDataProvider(SAVE_SELECTOR_RESOURCE_NAME);

                return _instance;
            }
        }

        public HerdModel Get => _saveSelector.GetSaveType;
        #endregion
    }
}
