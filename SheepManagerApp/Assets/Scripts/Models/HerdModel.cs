using Assets.Scripts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Models
{
    [CreateAssetMenu(menuName = "DataModels/HerdModel", fileName = "HerdModel")]
    public class HerdModel : ScriptableObject
    {
        #region Events

        public event Action DataChange;

        #endregion

        #region Editor

        [SerializeField]
        private string _herdId;

        [SerializeField]
        private string _herdName;

        [SerializeField]
        private List<SheepResponse> _sheeps;

        [SerializeField]
        private List<MatchResponse> _matches;

        [SerializeField]
        private List<VaccineResponse> _vaccines;

        #endregion

        #region Methods

        public void InitializeHerdData(string herdId, string herdName, List<SheepResponse> sheeps,
                                       List<MatchResponse> matches, List<VaccineResponse> vaccines)
        {
            _herdId = herdId; 
            _herdName = herdName;
            _sheeps = sheeps;
            _matches = matches;
            _vaccines = vaccines;
        }

        public void SetHerdId(string herdId)
        {
            if (string.IsNullOrEmpty(herdId))
            {
                return;
            }

            _herdId = herdId;
        }

        public void SetHerdName(string herdName)
        {
            if (string.IsNullOrEmpty(herdName)) 
            { 
                return; 
            }
            _herdName = herdName;
        }

        public void SetHerdMatches(List<MatchResponse> matches)
        {          
            _matches = matches;
        }

        public void SetHerdSheeps(List<SheepResponse> sheeps)
        {
            _sheeps = sheeps;
        }

        public void AddSheepToHerd(SheepAddRequest sheepAddRequest)
        {
            if (sheepAddRequest == null)
            {
                return;
            }

            SheepResponse newSheep = new()
            {
                sheepId = Guid.NewGuid().ToString(),
                tagNumber = sheepAddRequest.tagNumber,
                herdId = sheepAddRequest.herdId,
                weight = sheepAddRequest.weight,
                gender = sheepAddRequest.gender,
                race = sheepAddRequest.race,
                bloodType = sheepAddRequest.bloodType,
                selection = sheepAddRequest.selection,
                birthdate = sheepAddRequest.birthdate,
                motherTagNumber = sheepAddRequest.motherTagNumber,
                fatherTagNumber = sheepAddRequest.fatherTagNumber,
                isDead = sheepAddRequest.isDead,
                isSold = sheepAddRequest.isSold,
                isPregnant = sheepAddRequest.isPregnant
            };

            _sheeps.Add(newSheep);
            DataChange?.Invoke();
        }

        public void UpdateSheep(SheepUpdateRequest sheepToUpdate)
        {
            var updatedSheep = _sheeps.FirstOrDefault(s => s.sheepId == sheepToUpdate.sheepId);
            updatedSheep.sheepId = sheepToUpdate.sheepId;
            updatedSheep.tagNumber = sheepToUpdate.tagNumber;
            updatedSheep.herdId = sheepToUpdate.herdId;
            updatedSheep.weight = sheepToUpdate.weight;
            updatedSheep.gender = sheepToUpdate.gender;
            updatedSheep.race = sheepToUpdate.race;
            updatedSheep.bloodType = sheepToUpdate.bloodType;
            updatedSheep.selection = sheepToUpdate.selection;
            updatedSheep.birthdate = sheepToUpdate.birthdate;
            updatedSheep.motherTagNumber = sheepToUpdate.motherTagNumber;
            updatedSheep.fatherTagNumber = sheepToUpdate.fatherTagNumber;
            updatedSheep.isDead = sheepToUpdate.isDead;
            updatedSheep.isPregnant = sheepToUpdate.isPregnant;
            updatedSheep.isSold = sheepToUpdate.isSold;

            DataChange?.Invoke();
        }

        #endregion

        #region Properties

        public string HerdId => _herdId;
        public string HerdName => _herdName;
        public List<SheepResponse> Sheeps => _sheeps;
        public List<MatchResponse> Matches => _matches;
        public List<VaccineResponse> Vaccines => _vaccines;

        #endregion
    }
}
