using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingRemediation.SetBuildingRemediationResponsibilityReason
{
    public class SetBuildingRemediationResponsibilityReasonRequest : IRequest<Unit>
    {
        public EBuildingRemediationResponsibilityType? BuildingRemediationResponsibilityType { get; set; }
    }
}
