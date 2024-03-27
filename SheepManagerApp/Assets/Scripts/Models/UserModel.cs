using UnityEngine;

namespace Assets.Scripts.Models
{
    [CreateAssetMenu(menuName = "DataModels/UserModel", fileName = "UserModel")]
    public class UserModel : ScriptableObject
    {
        #region Editor

        [SerializeField]
        private string _userId;

        [SerializeField]
        private string _personName;

        [SerializeField]
        private string _email;

        [SerializeField]
        private string _herdId;

        [SerializeField]
        private int _dataVersion;

        [SerializeField]
        private HerdModel _herdModel;

        #endregion

        #region Fields

        private string _modelSaveName;

        #endregion

        #region Methods

        public void InitializeUserData(string userId, string personName, string email, string herdId)
        {
            _userId = userId;
            _personName = personName;
            _email = email;
            _herdId = herdId;
        }

        public void SetDataVersion(int dataVersion)
        {
            _dataVersion = dataVersion;
        }

        public void SetHerdModel(HerdModel herdModel)
        {
            _herdModel = herdModel;
        }

        public void SetModelSaveType(string saveTypeName)
        {
            _modelSaveName = saveTypeName;
        }

        #endregion

        #region Properties

        public string UserId => _userId;
        public string PersonName => _personName;
        public string Email => _email;
        public string HerdId => _herdId;
        public int DataVersion => _dataVersion;
        public HerdModel HerdModel => _herdModel;
        public string ModelSaveName => _modelSaveName;

        #endregion
    }
}
