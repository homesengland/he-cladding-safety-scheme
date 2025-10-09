using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BuildingSafetyRegulator.Get;

public class GetBuildingControlApprovalResponse
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public bool IsSubmitted { get; set; }
    public ENoYes? IsBuildingControlApprovalApplied { get; set; }
}