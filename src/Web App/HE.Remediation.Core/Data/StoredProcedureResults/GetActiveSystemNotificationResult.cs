namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetActiveSystemNotificationResult
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}