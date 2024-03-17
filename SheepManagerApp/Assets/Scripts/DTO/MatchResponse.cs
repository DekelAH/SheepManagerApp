using Assets.Scripts.Models;
using System;

namespace Assets.Scripts.DTO
{
    [Serializable]
    public class MatchResponse
    {
        #region Properties

        public string matchId;
        public int maleTagNumber;
        public int femaleTagNumber;

        #endregion
    }
}
