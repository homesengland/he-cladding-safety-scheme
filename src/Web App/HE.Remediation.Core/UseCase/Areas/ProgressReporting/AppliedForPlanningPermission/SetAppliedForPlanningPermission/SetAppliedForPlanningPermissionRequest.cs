
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppliedForPlanningPermission.SetAppliedForPlanningPermission;

public class SetAppliedForPlanningPermissionRequest : IRequest
{
    public bool? AppliedForPlanningPermission { get; set; }
}
