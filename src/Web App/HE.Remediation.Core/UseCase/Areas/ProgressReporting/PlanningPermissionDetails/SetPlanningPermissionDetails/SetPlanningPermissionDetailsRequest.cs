using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.PlanningPermissionDetails.SetPlanningPermissionDetails
{
    public class SetPlanningPermissionDetailsRequest : IRequest<Unit>
    {
        public int? PlanningPermissionSubmittedMonth { get; set; }
        public int? PlanningPermissionSubmittedYear { get; set; }
        public int? PlanningPermissionApprovedMonth { get; set; }
        public int? PlanningPermissionApprovedYear { get; set; }
    }
}