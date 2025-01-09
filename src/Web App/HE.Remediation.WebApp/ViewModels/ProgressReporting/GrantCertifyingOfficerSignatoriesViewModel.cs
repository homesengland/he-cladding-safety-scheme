using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class GrantCertifyingOfficerSignatoriesViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public string ReturnUrl { get; set; }

    public string Signatory { get; set; }
    public string EmailAddress { get; set; }
    public int? DateAppointedDay { get; set; }
    public int? DateAppointedMonth { get; set; }
    public int? DateAppointedYear { get; set; }

    public int Version { get; set; }
    public bool IsGcoComplete { get; set; }

    public ESubmitAction SubmitAction { get; set; }
}