using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.ThirdPartyContribution.Set;

public class SetThirdPartyContributionRequest : IRequest
{
    public IEnumerable<EFundingStillPursuing> ContributionPursuingTypes { get; set; }
    public decimal ContributionAmount { get; set; }
    public string ContributionNotes { get; set; }
}
