using HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class AdjustScopeViewModel : VariationRequestBaseViewModel
{
    public string ChangeOfScope { get; set; }

    public bool? IsTimescaleVariation { get; set; }

    public bool? IsCostsVariation { get; set; }

    public bool? IsThirdPartyContributionVariation { get; set; }
}
