using System;

namespace Assets.Scripts.DTO
{
    [Serializable]
    public class VaccineResponse
    {
        public string vaccineId;
        public string vaccineName;
        public string vaccinationDate;
        public bool isMandatory;
        public bool isForBirth;
        public int sheepTagNumber;
    }
}
