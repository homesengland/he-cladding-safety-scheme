using HE.Remediation.WebApp.ViewModels.ClosingReport.Shared;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class ConfirmationViewModel : ClosingReportBaseViewModel
{
    public bool? FinalCostReport { get; set; }
    public bool? ExitFraew { get; set; }
    public bool? CompletionCertificate { get; set; }
    public bool? InformedPracticalCompletion { get; set; } 
}