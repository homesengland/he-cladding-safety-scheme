using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class HasGrantCertifyingOfficerViewModel
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string ReturnUrl { get; set; }

    public bool? DoYouHaveAGrantCertifyingOfficer { get; set; }
    public int Version { get; set; }
    public bool IsGcoComplete { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }

    public ESubmitAction SubmitAction { get; set; }
}