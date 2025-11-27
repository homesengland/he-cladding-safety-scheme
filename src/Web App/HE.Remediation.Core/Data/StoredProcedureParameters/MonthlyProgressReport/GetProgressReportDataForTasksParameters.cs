namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport;

public class GetProgressReportDataForTasksParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
}