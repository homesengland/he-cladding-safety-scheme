namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates;

public abstract class GetDatesChangedParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
}