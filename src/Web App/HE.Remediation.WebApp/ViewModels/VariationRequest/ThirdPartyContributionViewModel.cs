using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class ThirdPartyContributionViewModel : VariationRequestBaseViewModel
{
    public IEnumerable<EFundingStillPursuing> ContributionPursuingTypes { get; set; } = new List<EFundingStillPursuing>();
    public decimal? ContributionAmount { get; set; }
    public string ContributionNotes { get; set; }
    public bool? IsScopeVariation { get; set; }
    public bool? IsTimescaleVariation { get; set; }
    public bool? IsCostsVariation { get; set; }
}