namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectSupport;
public class SetProjectSupportCheckYourAnswersParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public int TaskStatusId { get; set; }
}
