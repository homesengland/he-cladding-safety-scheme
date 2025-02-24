namespace HE.Remediation.Core.Data.StoredProcedureResults.VariationRequest;

public class GetVariationReasonResult
{
    public bool? IsCostVariation { get; set; }

    public bool? IsScopeVariation { get; set; }

    public bool? IsTimescaleVariation { get; set; }

    public bool? IsCostsVariation { get; set; }

    public bool? IsThirdPartyContributionVariation { get; set; }
}
