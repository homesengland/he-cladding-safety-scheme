using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.ThirdPartyContribution.Get;

public class GetThirdPartyContributionRequest : IRequest<GetThirdPartyContributionResponse>
{
    private GetThirdPartyContributionRequest()
    {
    }

    public static GetThirdPartyContributionRequest Request => new();
}
