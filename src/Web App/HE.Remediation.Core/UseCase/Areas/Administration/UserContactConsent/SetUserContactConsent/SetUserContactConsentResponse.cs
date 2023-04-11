using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.UserContactConsent.SetUserContactConsent;

public class SetUserContactConsentResponse: IRequest<Unit>
{
    public ENoYes? UserConsent { get; set; }
}
