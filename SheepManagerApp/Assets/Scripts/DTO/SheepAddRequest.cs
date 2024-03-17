using System;

namespace Assets.Scripts.DTO
{
    public class SheepAddRequest
    {
        #region Properties

        public int tagNumber;
        public string herdId;
        public double weight;
        public string gender;
        public string race;
        public string bloodType;
        public string selection;
        public string birthdate;
        public int motherTagNumber;
        public int fatherTagNumber;
        public bool isDead;
        public bool isSold;
        public bool isPregnant;

        #endregion
    }
}
