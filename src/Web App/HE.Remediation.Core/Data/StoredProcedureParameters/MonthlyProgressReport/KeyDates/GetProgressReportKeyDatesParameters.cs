namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates;

public abstract class GetProgressReportKeyDatesParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
}