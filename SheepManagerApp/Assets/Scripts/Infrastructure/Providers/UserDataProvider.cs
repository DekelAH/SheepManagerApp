using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Providers
{
    public class UserDataProvider
    {
        #region Consts

        private const string SAVE_SELECTOR_RESOURCE_NAME = "SaveSelector";

        #endregion

        #region Fields

        private static UserDataProvider _instance;
        private static SaveSelector _saveSelector;

        #endregion

        #region Constructor

        private UserDataProvider(string saveSelectorResourceName)
        {
            _saveSelector = Resources.Load<SaveSelector>(saveSelectorResourceName);
        }

        #endregion

        #region Properties

        public static UserDataProvider Instance
        {
            get
            {
                _instance ??= new UserDataProvider(SAVE_SELECTOR_RESOURCE_NAME);

                return _instance;
            }
        }

        public UserModel Get => _saveSelector.GetSaveType;

        #endregion
    }
}
