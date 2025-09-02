namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class SetFraVisitedCheckYourAnswersParameters
{
    public Guid ApplicationId { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
}