namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan;

public class GetMonthlyReportProjectPlanDocumentsParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
}