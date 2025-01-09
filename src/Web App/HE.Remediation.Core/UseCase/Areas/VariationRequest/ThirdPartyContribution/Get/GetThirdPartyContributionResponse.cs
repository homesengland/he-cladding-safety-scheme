using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.ThirdPartyContribution.Get;

public class GetThirdPartyContributionResponse
{
    public IEnumerable<EFundingStillPursuing> ContributionPursuingTypes { get; set; }
    public decimal? ContributionAmount { get; set; }
    public string ContributionNotes { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
    public bool? IsScopeVariation { get; set; }
    public bool? IsTimescaleVariation { get; set; }
    public bool? IsCostsVariation { get; set; }
}
