using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Administration.UserContactConsent.SetUserContactConsent;

public class SetUserContactConsentRequest: IRequest<Unit>
{
    public ENoYes? UserConsent { get; set; }
}
