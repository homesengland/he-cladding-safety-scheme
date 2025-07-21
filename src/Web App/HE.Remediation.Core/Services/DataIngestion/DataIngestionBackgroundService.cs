using System.Data;
using Dapper;
using HE.Remediation.Core.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HE.Remediation.Core.Services.DataIngestion
{
    public class DataIngestionBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DataIngestionBackgroundService> _logger;
        private readonly DataIngestionOptions _options;

        public DataIngestionBackgroundService(
            IServiceProvider serviceProvider,
            IOptions<DataIngestionOptions> options,
            ILogger<DataIngestionBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _options = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("{ServiceName} is running.", nameof(DataIngestionBackgroundService));

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var serviceScope = _serviceProvider.CreateScope())
                {
                    var db = serviceScope.ServiceProvider.GetRequiredService<IDbConnection>();

                    try
                    {
                        var jobs = (await db.QueryAsync<(Guid DataIngestionId, int TotalRows)>("DequeueDataIngestionJobs", commandType: CommandType.StoredProcedure)).ToList();

                        foreach (var job in jobs)
                        {
                            _logger.LogInformation("Starting data ingestion job {DataIngestionId} with {TotalRows} rows", job.DataIngestionId, job.TotalRows);

                            using (var jobScope = _serviceProvider.CreateScope())
                            {
                                var jobContext = new JobContext(job.DataIngestionId, job.TotalRows);
                                jobContext.StatusReportEventAsync += OnStatusReportAsync;
                                jobContext.JobCompleteEventAsync += OnJobCompleteAsync;

                                var producer = jobScope.ServiceProvider.GetRequiredService<DataIngestionProducer>();
                                var consumer = jobScope.ServiceProvider.GetRequiredService<DataIngestionConsumer>();

                                var producerTask = producer.ProduceAsync(jobContext, stoppingToken);
                                var consumerTask = consumer.ConsumeAsync(stoppingToken);

                                await Task.WhenAll(producerTask, consumerTask);
                            }

                            _logger.LogInformation("Completed data ingestion job {DataIngestionId}.", job.DataIngestionId);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "{DataIngestionBackgroundService} could not retrieve data ingestion jobs.", nameof(DataIngestionBackgroundService));
                    }

                    async Task OnStatusReportAsync(object sender, JobContextEventArgs e)
                    {
                        _logger.LogInformation("Job {JobId} update. Processed: {RowsProcessed}, Success: {SuccessCount}, Fail: {FailCount}", e.JobId, e.TotalProcessed, e.SuccessCount, e.FailCount);
                        await PersistStatusAsync(e, EImportStatus.Importing);
                    }

                    async Task OnJobCompleteAsync(object sender, JobContextEventArgs e)
                    {
                        _logger.LogInformation("Job {JobId} complete. Success: {SuccessCount}, Fail: {FailCount}", e.JobId, e.SuccessCount, e.FailCount);
                        await PersistStatusAsync(e, EImportStatus.Imported);
                    }

                    async Task PersistStatusAsync(JobContextEventArgs e, EImportStatus status)
                    {
                        await db.ExecuteAsync(
                            "UpdateDataIngestionJobResult",
                                new
                                {
                                    DataIngestionId = e.JobId,
                                    ProcessedCount = e.SuccessCount,
                                    FailedCount = e.FailCount,
                                    StatusId = status
                                }, 
                                commandType: CommandType.StoredProcedure
                        );
                    }
                }

                await Task.Delay(_options.PollingIntervalSeconds * 1000, stoppingToken);
            }
        }
    }
}
