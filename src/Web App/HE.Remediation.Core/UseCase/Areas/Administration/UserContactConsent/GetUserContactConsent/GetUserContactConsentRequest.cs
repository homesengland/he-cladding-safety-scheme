using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Administration.UserContactConsent.GetUserContactConsent;

public class GetUserContactConsentRequest: IRequest<GetUserContactConsentResponse>
{
    private GetUserContactConsentRequest()
    {
    }

    public static readonly GetUserContactConsentRequest Request = new();
}
