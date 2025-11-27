namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.Submission;

public class SetAsSubmittedParameters
{
    public Guid ProgressReportId { get; set; }
    public Guid? UserId { get; internal set; }
}