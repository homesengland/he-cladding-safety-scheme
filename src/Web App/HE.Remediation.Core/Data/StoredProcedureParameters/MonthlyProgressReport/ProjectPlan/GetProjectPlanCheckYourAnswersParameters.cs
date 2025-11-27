namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan;

public class GetProjectPlanCheckYourAnswersParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
}