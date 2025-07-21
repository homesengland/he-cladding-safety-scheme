using Microsoft.Extensions.Logging;
using HE.Remediation.Core.Interface;
using Microsoft.Extensions.Options;
using HE.Remediation.Core.Data;
using System.Data;
using Microsoft.Extensions.DependencyInjection;

namespace HE.Remediation.Core.Services.DataIngestion
{
    public class DataIngestionProducer
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DataIngestionBatchChannel _channel;
        private readonly DataIngestionOptions _options;
        private readonly ILogger<DataIngestionProducer> _logger;

        public DataIngestionProducer(
            IServiceProvider serviceProvider,
            DataIngestionBatchChannel channel,
            IOptions<DataIngestionOptions> options,
            ILogger<DataIngestionProducer> logger)
        {
            _serviceProvider = serviceProvider;
            _channel = channel;
            _options = options.Value;
            _logger = logger;
        }

        public async Task ProduceAsync(JobContext jobContext, CancellationToken cancellationToken)
        {
            // Producer needs it's own db connection scope 
            using (var scope = _serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<IDbConnectionWrapper>();

                try
                {
                    int offset = 0;
                    while (offset < jobContext.TotalRows)
                    {
                        var batch = (await db.QueryAsync<UnprocessedRow>("GetUnprocessedRowBatch", new { DataIngestionId = jobContext.JobId, Skip = offset, Take = _options.BatchSize })).ToList();

                        if (batch.Count == 0)
                            break;

                        await _channel.WriteAsync(jobContext, [.. batch], cancellationToken);
                        offset += batch.Count;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error getting batched UnprocessedRows from database.");
                }

                _channel.Complete();
            };


        }
    }
}
