namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppliedForPlanningPermission.GetAppliedForPlanningPermission;

public class GetAppliedForPlanningPermissionResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool? AppliedForPlanningPermission { get; set; }
    public int Version { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }
}
