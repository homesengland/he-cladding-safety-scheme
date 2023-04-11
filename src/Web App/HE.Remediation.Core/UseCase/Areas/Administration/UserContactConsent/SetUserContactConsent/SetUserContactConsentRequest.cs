using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.UserContactConsent.SetUserContactConsent;

public class SetUserContactConsentRequest: IRequest<Unit>
{
    public ENoYes? UserConsent { get; set; }
}
