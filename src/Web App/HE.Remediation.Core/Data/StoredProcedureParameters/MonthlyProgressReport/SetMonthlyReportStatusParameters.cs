namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport;

public class SetMonthlyReportStatusParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public int TaskStatusId { get; set; }
}