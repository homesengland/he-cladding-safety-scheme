
namespace HE.Remediation.Core.Services.Communication.Collaboration
{
    public interface ICommunicationService
    {
        Task SendEmailInvite(CollaborationEmailRequest emailRequest, CancellationToken cancellationToken = default);
    }
}