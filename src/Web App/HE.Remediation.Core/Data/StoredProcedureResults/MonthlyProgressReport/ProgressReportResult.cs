namespace HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport;

public class ProgressReportResult
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateDue { get; set; }
    public DateTime? DateSubmitted { get; set; }
    public int Version { get; set; }
}
