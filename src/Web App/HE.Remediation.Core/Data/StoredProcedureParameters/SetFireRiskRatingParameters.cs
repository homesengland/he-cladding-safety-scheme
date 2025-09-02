namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class SetFireRiskRatingParameters
{
    public Guid ApplicationId { get; set; }
    public int FireRiskRatingId { get; set; }
    public bool HasInternalFireSafetyRisks { get; set; }
}