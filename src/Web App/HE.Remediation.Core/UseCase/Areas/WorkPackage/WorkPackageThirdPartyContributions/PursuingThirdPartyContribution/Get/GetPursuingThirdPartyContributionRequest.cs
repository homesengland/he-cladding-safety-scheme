using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.PursuingThirdPartyContribution.Get;

public class GetPursuingThirdPartyContributionRequest : IRequest<GetPursuingThirdPartyContributionResponse>
{
    private GetPursuingThirdPartyContributionRequest()
    {
    }

    public static GetPursuingThirdPartyContributionRequest Request => new();
}
