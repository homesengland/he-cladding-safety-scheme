using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingPartOfDevelopment.GetBuildingPartOfDevelopment;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingRemediation.GetBuildingRemediationResponsibilityReason
{
    public class GetBuildingRemediationResponsibilityReasonRequest : IRequest<GetBuildingRemediationResponsibilityResponse>
    {
        private GetBuildingRemediationResponsibilityReasonRequest() { }

        public static readonly GetBuildingRemediationResponsibilityReasonRequest Request = new();
    }
}
