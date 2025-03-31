using HE.Remediation.WebApp.ViewModels.ClosingReport.Shared;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class DeclarationViewModel : ClosingReportBaseViewModel
{
    public DateTime? DateOfCompletion { get; set; }

    public bool? FraewRiskToLifeReduced { get; set; }

    public bool? DischargedObligations { get; set; }    

    public DateTime? ApplicationCreationDate { get; set; }
}