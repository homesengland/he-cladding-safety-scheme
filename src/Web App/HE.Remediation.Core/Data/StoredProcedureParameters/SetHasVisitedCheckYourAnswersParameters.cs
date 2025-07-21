namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class SetHasVisitedCheckYourAnswersParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
}