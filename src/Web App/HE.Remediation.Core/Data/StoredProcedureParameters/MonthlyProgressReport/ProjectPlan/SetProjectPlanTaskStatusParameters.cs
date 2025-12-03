namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan;

public class SetProjectPlanTaskStatusParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public int TaskStatusId { get; set; }
}