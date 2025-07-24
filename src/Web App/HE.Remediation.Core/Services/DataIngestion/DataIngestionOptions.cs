namespace HE.Remediation.Core.Services.DataIngestion
{
    public class DataIngestionOptions
    {
        public int PollingIntervalSeconds { get; set; } = 30;
        public int ChannelCapacity { get; set; } = 10;
        public int BatchSize { get; set; } = 100;
    }
}
