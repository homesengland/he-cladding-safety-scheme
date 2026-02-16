using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetThirdPartyContributionsChanged;

public class SetThirdPartyContributionsChangedRequest : IRequest<SetThirdPartyContributionsChangedResponse>
{
    public bool? ThirdPartyContributionsChanged { get; set; }
}
