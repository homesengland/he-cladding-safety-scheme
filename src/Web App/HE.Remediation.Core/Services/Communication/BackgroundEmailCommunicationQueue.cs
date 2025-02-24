using System.Threading.Channels;

namespace HE.Remediation.Core.Services.Communication
{
    public class BackgroundEmailCommunicationQueue : IBackgroundEmailCommunicationQueue
    {
        private readonly Channel<SendEmailCommunicationTask> _queue;

        public BackgroundEmailCommunicationQueue()
        {
            _queue = Channel.CreateUnbounded<SendEmailCommunicationTask>();
        }

        public async ValueTask QueueBackgroundEmailCommunicationAsync(SendEmailCommunicationTask sendEmailCommunicationTask)
        {
            if (sendEmailCommunicationTask == null)
            {
                throw new ArgumentNullException(nameof(sendEmailCommunicationTask));
            }

            await _queue.Writer.WriteAsync(sendEmailCommunicationTask);
        }

        public async ValueTask<SendEmailCommunicationTask> DequeueAsync(CancellationToken cancellationToken)
        {
            var sendEmailCommunicationTask = await _queue.Reader.ReadAsync(cancellationToken);

            return sendEmailCommunicationTask;
        }
    }
}
