using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class SoughtQuotesViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool? QuotesSought { get; set; }
    public int Version { get; set; }

    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}
