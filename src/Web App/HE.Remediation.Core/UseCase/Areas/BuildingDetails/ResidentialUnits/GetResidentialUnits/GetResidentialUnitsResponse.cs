using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ResidentialUnits.GetResidentialUnits
{
    public class GetResidentialUnitsResponse
    {
        public int? ResidentialUnitsCount { get; set; }
        public ENoYes? NonResidentialUnits { get; set; }
        public bool? WorksAlreadyCompleted { get; set; }
    }
}
