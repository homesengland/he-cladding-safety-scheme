using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BuildingSafetyRegulator.Set;

public class SetBuildingControlApprovalRequest : IRequest
{
    public ENoYes IsBuildingControlApprovalApplied { get; set; }
}