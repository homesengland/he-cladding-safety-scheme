namespace HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest
{
    public class UpsertVariationSelectionParameters
    {
        public bool? IsCostVariation { get; set; }

        public bool? IsScopeVariation { get; set; }

        public bool? IsTimescaleVariation { get; set; }

        public bool? IsThirdPartyContributionVariation { get; set; }
}
}