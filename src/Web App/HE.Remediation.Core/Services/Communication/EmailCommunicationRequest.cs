using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Services.Communication;

public record EmailCommunicationRequest(Guid ApplicationId, EEmailType EmailType)
{

}