using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class PlanningPermissionPlannedSubmitDateViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public int? PlanningPermissionPlannedSubmitMonth { get; set; }
    public int? PlanningPermissionPlannedSubmitYear { get; set; }
    public int Version { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }

    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}
