namespace HE.Remediation.Core.Services.DataIngestion
{
    public class UnprocessedRow
    {
        public Guid Id { get; set; }
        public string RowJson { get; set; }
    }
}
