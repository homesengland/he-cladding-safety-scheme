using HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class VariationReasonViewModel : VariationRequestBaseViewModel
{
    public bool? IsCostVariation { get; set; }

    public bool? IsScopeVariation { get; set; }

    public bool? IsTimescaleVariation { get; set; }

    public bool? IsThirdPartyContributionVariation { get; set; }

    public bool VariationSelection { get; set; }

    public decimal? ApprovedGrantFunding { get; set; }

    public decimal? GrantPaidToDate { get; set; }

    public decimal? UnclaimedGrantFunding { get; set; }

    public bool NoMonthlyPaymentsOutstanding { get; set; }
    
    public bool ClosingReportStarted { get; set; }
}
