using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetThirdPartyContributionsChanged;

public class GetThirdPartyContributionsChangedRequest : IRequest<GetThirdPartyContributionsChangedResponse>
{
    private GetThirdPartyContributionsChangedRequest()
    {
    }

    public static readonly GetThirdPartyContributionsChangedRequest Request = new();
}
