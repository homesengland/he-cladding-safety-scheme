
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonPlanningNotApplied.SetReasonPlanningNotApplied;

public class SetReasonPlanningNotAppliedRequest : IRequest
{
    public string ReasonPlanningPermissionNotApplied { get; set; }

    public bool? PlanningPermissionNeedsSupport { get; set; }

}

