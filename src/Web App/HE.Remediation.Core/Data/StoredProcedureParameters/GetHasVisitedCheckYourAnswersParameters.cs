namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class GetHasVisitedCheckYourAnswersParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
}