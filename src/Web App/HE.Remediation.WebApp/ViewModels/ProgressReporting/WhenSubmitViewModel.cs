using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class WhenSubmitViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public int? SubmissionMonth { get; set; }
    public int? SubmissionYear { get; set; }
    public bool? HasAppliedForBuildingControl { get; set; }
    public int Version { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }
    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}
