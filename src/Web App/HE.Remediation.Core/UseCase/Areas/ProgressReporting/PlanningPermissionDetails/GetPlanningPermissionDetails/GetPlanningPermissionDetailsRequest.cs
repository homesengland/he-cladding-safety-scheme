using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.PlanningPermissionDetails.GetPlanningPermissionDetails
{
    public class GetPlanningPermissionDetailsRequest : IRequest<GetPlanningPermissionDetailsResponse>
    {
        private GetPlanningPermissionDetailsRequest()
        {

        }

        public static readonly GetPlanningPermissionDetailsRequest Request = new();
    }
}