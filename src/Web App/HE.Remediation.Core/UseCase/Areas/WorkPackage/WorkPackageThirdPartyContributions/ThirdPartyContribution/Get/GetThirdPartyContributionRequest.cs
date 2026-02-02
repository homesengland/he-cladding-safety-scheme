using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.ThirdPartyContribution.Get;

public class GetThirdPartyContributionRequest : IRequest<GetThirdPartyContributionResponse>
{
    private GetThirdPartyContributionRequest()
    {
    }

    public static GetThirdPartyContributionRequest Request => new();
}
