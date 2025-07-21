using Microsoft.Extensions.Options;
using System.Threading.Channels;

namespace HE.Remediation.Core.Services.DataIngestion
{
    public class DataIngestionBatchChannel
    {
        private readonly Channel<(JobContext, List<UnprocessedRow>)> _channel;

        public DataIngestionBatchChannel(IOptions<DataIngestionOptions> options)
        {
            var channelOptions = new BoundedChannelOptions(options.Value.ChannelCapacity)
            {
                FullMode = BoundedChannelFullMode.Wait
            };
            _channel = Channel.CreateBounded<(JobContext, List<UnprocessedRow>)>(channelOptions);
        }

        public async ValueTask WriteAsync(JobContext context, List<UnprocessedRow> batch, CancellationToken cancellationToken)
        {
            await _channel.Writer.WriteAsync((context, batch), cancellationToken);
        }

        public IAsyncEnumerable<(JobContext, List<UnprocessedRow>)> ReadAllAsync(CancellationToken cancellationToken)
        {
            return _channel.Reader.ReadAllAsync(cancellationToken);
        }

        public void Complete()
        {
            _channel.Writer.TryComplete();
        }
    }
}
