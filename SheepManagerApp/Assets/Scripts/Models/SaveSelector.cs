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
        private HerdModel _herdModel;

        [SerializeField]
        private SaveTypes _saveType;

        #endregion

        #region Methods

        private HerdModel SaveTypeSelector()
        {
            switch (_saveType)
            {
                case SaveTypes.LocalFile:
                    _herdModel.SetModelSaveType(LOCAL_FILE);
                    return _herdModel;
                case SaveTypes.PlayerPrefs:
                    _herdModel.SetModelSaveType(PLAYER_PREFS);
                    return _herdModel;
                default:
                    return null;
            }
        }

        #endregion

        #region Properties

        public HerdModel GetSaveType => SaveTypeSelector();

        #endregion
    }
}