namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class SetAlternateFundingVisitedCheckYourAnswersParameters
{
    public Guid ApplicationId { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
}