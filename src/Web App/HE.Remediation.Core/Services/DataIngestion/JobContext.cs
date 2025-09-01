using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Services.DataIngestion
{
    public class JobContext
    {
        public Guid JobId { get; }
        public int TotalRows { get; }
        public EApplicationScheme TargetScheme { get; }

        public int SuccessCount => _successCount;
        public int FailCount => _failCount;

        private int _successCount;
        private int _failCount;

        public event Func<object, JobContextEventArgs, Task> StatusReportEventAsync;
        public event Func<object, JobContextEventArgs, Task> JobCompleteEventAsync;

        public JobContext(Guid jobId, int totalRows, EDataIngestionImportType importType)
        {
            JobId = jobId;
            TotalRows = totalRows;
            TargetScheme = importType switch
            {
                EDataIngestionImportType.SocialSectorSelfFunded => EApplicationScheme.SocialSector,
                EDataIngestionImportType.SocialSectorCss => EApplicationScheme.CladdingSafetyScheme,
                _ => EApplicationScheme.CladdingSafetyScheme
            };
        }

        public async Task IncrementSuccess(int count = 1)
        {
            Interlocked.Add(ref _successCount, count);
            await EmitStatusEvents();
        }

        public async Task IncrementFail(int count = 1)
        {
            Interlocked.Add(ref _failCount, count);
            await EmitStatusEvents();
        }

        private async Task EmitStatusEvents()
        {
            var totalProcessed = _successCount + _failCount;

            if(totalProcessed >= TotalRows)
            {
                await JobCompleteEventAsync.Invoke(this, new JobContextEventArgs(JobId, _successCount, _failCount));
                return;
            }

            if (totalProcessed > 0 && totalProcessed % 200 == 0)
            {
                await StatusReportEventAsync.Invoke(this, new JobContextEventArgs(JobId, _successCount, _failCount));
            }
        }

        public bool IsComplete => (SuccessCount + FailCount) >= TotalRows;
    }

    public class JobContextEventArgs(Guid jobId, int successCount, int failCount) : EventArgs
    {
        public Guid JobId { get; } = jobId;
        public int SuccessCount { get; } = successCount;
        public int FailCount { get; } = failCount;
        public int TotalProcessed => SuccessCount + FailCount;
    }
}
