using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetFireRiskRatingResult
{
    public EFraRiskRating? FireRiskRatingId { get; set; }
    public bool? HasInternalFireSafetyRisks { get; set; }
}