namespace HE.Remediation.Core.Data.StoredProcedureResults.VariationRequest
{
    public class GetVariationPreliminaryCostsResult
    {
        public decimal? VariationMainContractorPreliminariesAmount { get; set; }
        public string VariationMainContractorPreliminariesDescription { get; set; }
        public decimal? VariationAccessAmount { get; set; }
        public string VariationAccessDescription { get; set; }
        public decimal? VariationOverheadsAndProfitAmount { get; set; }
        public string VariationOverheadsAndProfitDescription { get; set; }
        public decimal? VariationContractorContingenciesAmount { get; set; }
        public string VariationContractorContingenciesDescription { get; set; }

        public decimal? WorkPackageMainContractorPreliminariesAmount { get; set; }
        public string WorkPackageMainContractorPreliminariesDescription { get; set; }
        public decimal? WorkPackageAccessAmount { get; set; }
        public string WorkPackageAccessDescription { get; set; }
        public decimal? WorkPackageOverheadsAndProfitAmount { get; set; }
        public string WorkPackageOverheadsAndProfitDescription { get; set; }
        public decimal? WorkPackageContractorContingenciesAmount { get; set; }
        public string WorkPackageContractorContingenciesDescription { get; set; }
    }
}
