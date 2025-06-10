using System.Threading.Channels;

namespace HE.Remediation.Core.Services.Communication.Collaboration
{
    public class BackgroundCollaborationCommunicationQueue : IBackgroundCollaborationCommunicationQueue
    {
        private readonly Channel<CollaborationEmailRequest> _queue;

        public BackgroundCollaborationCommunicationQueue()
        {
            _queue = Channel.CreateUnbounded<CollaborationEmailRequest>();
        }

        public async ValueTask QueueAsync(CollaborationEmailRequest communicationRequest, CancellationToken cancellationToken)
        {
            if (communicationRequest == null)
            {
                throw new ArgumentNullException(nameof(communicationRequest));
            }

            await _queue.Writer.WriteAsync(communicationRequest, cancellationToken);
        }

        public IAsyncEnumerable<CollaborationEmailRequest> ReadAllAsync(CancellationToken cancellationToken)
        {
            return _queue.Reader.ReadAllAsync(cancellationToken);
        }
    }
}
