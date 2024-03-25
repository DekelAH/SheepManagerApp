using System;

namespace Assets.Scripts.DTO
{
    [Serializable]
    public class SheepResponse
    {
        #region Properties

        public string sheepId;
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

        #region Methods

        public double GetAge()
        {
            var sheepBirthdate = DateTime.Parse(birthdate);
            DateTime today = DateTime.Today;
            int years = today.Year - sheepBirthdate.Year;
            DateTime last = sheepBirthdate.AddYears(years);

            if (last > today)
            {
                last = last.AddYears(-1);
                years--;
            }

            DateTime next = last.AddYears(1);
            double yearDays = (next - last).Days;
            double days = (today - last).Days;
            double exactAge = (double)years + (days / yearDays);

            return exactAge;
        }

        #endregion
    }
}