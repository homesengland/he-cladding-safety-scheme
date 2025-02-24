using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Services.Communication
{
    public interface ICommunicationService
    {
        Task QueueEmailCommunication(EmailCommunicationRequest request);

        Task<bool> InsertEmailCommunication(Guid applicationId, EEmailType emailType);
    }
}
