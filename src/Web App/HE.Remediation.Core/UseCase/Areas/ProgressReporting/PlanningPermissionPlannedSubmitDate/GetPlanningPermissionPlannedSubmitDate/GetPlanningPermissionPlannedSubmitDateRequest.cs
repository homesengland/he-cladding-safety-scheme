using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.PlanningPermissionPlannedSubmitDate.GetPlanningPermissionPlannedSubmitDate
{
    public class GetPlanningPermissionPlannedSubmitDateRequest : IRequest<GetPlanningPermissionPlannedSubmitDateResponse>
    {
        private GetPlanningPermissionPlannedSubmitDateRequest()
        {

        }

        public static readonly GetPlanningPermissionPlannedSubmitDateRequest Request = new();
    }
}