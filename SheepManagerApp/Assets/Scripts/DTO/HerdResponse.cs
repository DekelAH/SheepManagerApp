

using Assets.Scripts.Models;
using System.Collections.Generic;

namespace Assets.Scripts.DTO
{
    public class HerdResponse
    {
        #region Properties

        public string herdId;
        public string herdName;
        public List<SheepResponse> herdSheeps;
        public List<MatchResponse> matches;
        public List<VaccineResponse> vaccines;

        #endregion
    }

    public static class HerdExtensionMethods
    {
        public static HerdResponse ToHerdResponse(this HerdModel herd)
        {
            return new HerdResponse()
            {
                herdId = herd.HerdId,
                herdName = herd.HerdName,
                herdSheeps = herd.Sheeps,
                matches = herd.Matches,
                vaccines = herd.Vaccines
            };
        }
    }
}
