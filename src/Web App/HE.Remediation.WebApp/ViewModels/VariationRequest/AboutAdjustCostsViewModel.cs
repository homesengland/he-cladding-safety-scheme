using HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class AboutAdjustCostsViewModel : VariationRequestBaseViewModel
{
    public bool? IsScopeVariation { get; set; }
    public bool? IsTimescaleVariation { get; set; }
    public bool? IsThirdPartyContributionVariation { get; set; }
}
