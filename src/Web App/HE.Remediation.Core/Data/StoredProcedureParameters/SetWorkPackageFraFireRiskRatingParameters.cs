namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class SetWorkPackageFraFireRiskRatingParameters
{
    public Guid ApplicationId { get; set; }
    public int FireRiskRatingId { get; set; }
    public bool HasInteralFireSafetyRisks { get; set; }
}