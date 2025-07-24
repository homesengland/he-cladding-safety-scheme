using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class WhenStartOnSiteViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public int? StartMonth { get; set; }
    public int? StartYear { get; set; }
    public bool? BuildingControlRequired { get; set; }
    public int Version { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }

    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}
