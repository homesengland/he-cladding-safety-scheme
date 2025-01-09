using HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class CheckYourAnswersViewModel : VariationRequestBaseViewModel
{
    public bool? IsCostVariation { get; set; }

    public bool? IsScopeVariation { get; set; }

    public bool? IsTimescaleVariation { get; set; }

    public bool? IsThirdPartyContributionVariation { get; set; }

    public int? NewEndMonth { get; set; }

    public int? NewEndYear { get; set; }

    public string ChangeOfScope { get; set; }

    public bool? ChangeOfCosts { get; set; }

    public decimal? TotalApprovedFunding { get; set; }

    public decimal? VariationRequested { get; set; }

    public decimal? TotalRequestedAmount { get; set; }

    public int? Duration { get; set; }

    public List<string> ThirdPartyContributionTypes { get; set; }

    public decimal ThirdPartyContributionAmount { get; set; }

    public string ThirdPartyContributionNotes { get; set; }

    public string VariationSummary { get; set; }
}
