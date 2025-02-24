using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.PlanningPermissionPlannedSubmitDate.SetPlanningPermissionPlannedSubmitDate
{
    public class SetPlanningPermissionPlannedSubmitDateRequest : IRequest<Unit>
    {
        public int? PlanningPermissionPlannedSubmitMonth { get; set; }
        public int? PlanningPermissionPlannedSubmitYear { get; set; }
    }
}