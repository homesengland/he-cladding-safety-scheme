namespace HE.Remediation.Core.Services.Communication.Collaboration
{
    public interface IBackgroundCollaborationCommunicationQueue
    {
        ValueTask QueueAsync(CollaborationEmailRequest communicationRequest, CancellationToken cancellationToken);
        IAsyncEnumerable<CollaborationEmailRequest> ReadAllAsync(CancellationToken cancellationToken);
    }
}