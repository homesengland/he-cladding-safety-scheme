using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Services.Communication.Collaboration
{
    public record CollaborationEmailRequest(EEmailType EmailType, string EmailTo, Dictionary<string, string> Parameters, string TraceInfo);
}
