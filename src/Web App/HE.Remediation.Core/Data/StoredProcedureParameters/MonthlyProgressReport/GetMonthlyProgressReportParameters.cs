namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport;

public class GetMonthlyProgressReportParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
}