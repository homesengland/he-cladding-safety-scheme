using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class GrantCertifyingOfficerSignatoriesViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public string ReturnUrl { get; set; }

    public string Signatory { get; set; }
    public string EmailAddress { get; set; }
    public DateTime? DateAppointed { get; set; }

    public int Version { get; set; }
    public bool IsGcoComplete { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }

    public ESubmitAction SubmitAction { get; set; }
}