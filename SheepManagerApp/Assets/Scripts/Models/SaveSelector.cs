using System;
using UnityEngine;

namespace Assets.Scripts.Models
{
    [CreateAssetMenu(menuName = "DataModels/SaveSelector", fileName = "SaveSelector")]
    public class SaveSelector : ScriptableObject
    {
        #region Consts

        private const string LOCAL_FILE = "Local File";
        private const string PLAYER_PREFS = "Player Prefs";

        #endregion

        #region Editor

        [SerializeField]
        private UserModel _userModel;

        [SerializeField]
        private SaveTypes _saveType;

        #endregion

        #region Methods

        private UserModel SaveTypeSelector()
        {
            switch (_saveType)
            {
                case SaveTypes.LocalFile:
                    _userModel.SetModelSaveType(LOCAL_FILE);
                    return _userModel;
                case SaveTypes.PlayerPrefs:
                    _userModel.SetModelSaveType(PLAYER_PREFS);
                    return _userModel;
                default:
                    return null;
            }
        }

        #endregion

        #region Properties

        public UserModel GetSaveType => SaveTypeSelector();

        #endregion
    }
}