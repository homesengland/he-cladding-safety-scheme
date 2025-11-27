using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingRemediation.SetBuildingRemediationResponsibilityReason
{
    public class SetBuildingRemediationResponsibilityReasonRequest : IRequest<Unit>
    {
        public EBuildingRemediationResponsibilityType? BuildingRemediationResponsibilityType { get; set; }
    }
}
