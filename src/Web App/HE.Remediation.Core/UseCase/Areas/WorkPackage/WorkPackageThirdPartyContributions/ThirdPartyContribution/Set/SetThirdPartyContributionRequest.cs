using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.ThirdPartyContribution.Set;

public class SetThirdPartyContributionRequest : IRequest
{
    public IEnumerable<EFundingStillPursuing> ContributionPursuingTypes { get; set; }
    public decimal ContributionAmount { get; set; }
    public string ContributionNotes { get; set; }
}
