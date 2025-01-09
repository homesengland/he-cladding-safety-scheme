namespace HE.Remediation.Core.Services.Communication
{
    public interface IBackgroundEmailCommunicationQueue
    {
        ValueTask QueueBackgroundEmailCommunicationAsync(SendEmailCommunicationTask sendEmailCommunicationTask);
        ValueTask<SendEmailCommunicationTask> DequeueAsync(CancellationToken cancellationToken);
    }
}
