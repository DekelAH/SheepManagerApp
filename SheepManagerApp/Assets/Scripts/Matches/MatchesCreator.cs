using Assets.Scripts.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assets.Scripts.Matches
{
    public static class MatchesCreator
    {
        #region Methods

        public static async Task<List<MatchResponse>> CreateMatches(List<SheepResponse> allMales, List<SheepResponse> allFemales)
        {
            if (allMales.Count == 0 || allFemales.Count == 0)
            {
                var paramName = "No sheeps to match";
                throw new ArgumentNullException(paramName);
            }

            var matches = await MatchesCreation(allMales, allFemales);
            return matches;
        }

        private static Task<List<MatchResponse>> MatchesCreation(List<SheepResponse> males,
                                                                 List<SheepResponse> females)
        {
            var matchesCreatedList = new List<MatchResponse>();

            foreach (var male in males)
            {
                foreach (var female in females)
                {
                    var age = female.GetAge();
                    if (!HaveGeneticRelation(male, female) && age >= 1.0)
                    {
                        if (female is { gender: "Female", isPregnant: false, isDead: false, isSold: false, selection: "Breed", weight: >= 60.0 })
                        {
                            var newMatch = new MatchResponse()
                            {
                                matchId = Guid.NewGuid().ToString(),
                                maleTagNumber = male.tagNumber,
                                femaleTagNumber = female.tagNumber
                            };
                            matchesCreatedList.Add(newMatch);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return Task.FromResult(matchesCreatedList);
        }

        private static bool HaveGeneticRelation(SheepResponse maleToCheck, SheepResponse femaleToCheck)
        {
            if (maleToCheck.tagNumber != femaleToCheck.fatherTagNumber)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
