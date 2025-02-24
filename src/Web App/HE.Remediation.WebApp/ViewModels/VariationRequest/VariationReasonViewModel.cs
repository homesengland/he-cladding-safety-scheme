using Amazon.S3.Model;
using HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;
using Humanizer;

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
}
