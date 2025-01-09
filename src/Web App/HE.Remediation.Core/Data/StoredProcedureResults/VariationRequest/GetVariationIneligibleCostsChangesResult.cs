namespace HE.Remediation.Core.Data.StoredProcedureResults.VariationRequest
{
    public class GetVariationIneligibleCostsChangesResult
    {
        public decimal? VariationIneligibleAmount { get; set; }

        public string VariationIneligibleDescription { get; set; }

        public decimal? WorkPackageIneligibleAmount { get; set; }

        public string WorkPackageIneligibleDescription { get; set; }
    }
}
