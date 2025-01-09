using HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class CostProfileViewModel : VariationRequestBaseViewModel
{
    public decimal? ApprovedGrantFunding { get; set; }

    public decimal? GrantPaidToDate { get; set; }

    public decimal? UnclaimedGrantFunding { get; set; }

    public int? ProjectDuration { get; set; }

    public decimal? TotalAmount { get; set; }

    public IReadOnlyCollection<CostProfileItemViewModel> CostsProfile { get; set; }

    public bool? IsThirdPartyContributionVariation { get; set; }
}
