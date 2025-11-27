using System.Text.Json;
using CSS_SSSF = HE.Remediation.Core.UseCase.DataIngest.CSS_SSSF;
using RAS = HE.Remediation.Core.UseCase.DataIngest.RAS;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HE.Remediation.Core.Services.DataIngestion
{
    public class DataIngestionConsumer
    {
        private readonly DataIngestionBatchChannel _channel;
        private readonly ILogger<DataIngestionConsumer> _logger;
        private readonly ISender _sender;

        public DataIngestionConsumer(
            DataIngestionBatchChannel channel,
            ILogger<DataIngestionConsumer> logger,
            ISender sender)
        {
            _channel = channel;
            _logger = logger;
            _sender = sender;
        }

        public async Task ConsumeAsync(CancellationToken cancellationToken)
        {
            await foreach (var (jobContext, batch) in _channel.ReadAllAsync(cancellationToken))
            {
                foreach (var row in batch)
                {
                    try
                    {
                        var data = JsonSerializer.Deserialize<Dictionary<string, string>>(row.RowJson);

                        if(jobContext.TargetScheme == Enums.EApplicationScheme.CladdingSafetyScheme || 
                                jobContext.TargetScheme == Enums.EApplicationScheme.SocialSector)
                        {
                            await _sender.Send(new CSS_SSSF.CreateImportRequest(data, row.Id, jobContext.JobId, jobContext.TargetScheme), cancellationToken);
                        }

                        if (jobContext.TargetScheme == Enums.EApplicationScheme.ResponsibleActorsScheme)
                        {
                            await _sender.Send(new RAS.CreateImportRequest(data, row.Id, jobContext.JobId, jobContext.TargetScheme), cancellationToken);
                        }


                        await jobContext.IncrementSuccess();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Row {DataIngestionId} failed to import", row.Id);
                        await jobContext.IncrementFail();
                    }
                }
            }
        }
    }
}
