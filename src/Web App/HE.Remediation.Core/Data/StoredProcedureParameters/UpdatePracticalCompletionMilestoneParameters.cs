namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class UpdatePracticalCompletionMilestoneParameters
{
    public Guid ApplicationId { get; set; }
    public DateTime PracticalCompletionDate { get; set; }
}