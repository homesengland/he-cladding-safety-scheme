namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class SetWorkPackageFraVisitedCheckYourAnswersParameters
{
    public Guid ApplicationId { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
}