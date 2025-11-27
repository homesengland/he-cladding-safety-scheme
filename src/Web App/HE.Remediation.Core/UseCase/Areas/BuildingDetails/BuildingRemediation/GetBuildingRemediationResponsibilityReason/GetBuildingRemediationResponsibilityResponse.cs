using HE.Remediation.Core.Enums;
namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingRemediation.GetBuildingRemediationResponsibilityReason
{
    public class GetBuildingRemediationResponsibilityResponse
    {
        public EBuildingRemediationResponsibilityType? BuildingRemediationResponsibilityType { get; set; }
        public EApplicationScheme ApplicationScheme { get; set; }
    }
}
