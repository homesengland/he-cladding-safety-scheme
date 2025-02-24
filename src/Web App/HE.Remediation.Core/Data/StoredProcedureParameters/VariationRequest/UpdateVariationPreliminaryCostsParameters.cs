namespace HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest
{
    public class UpdateVariationPreliminaryCostsParameters
    {
        public Guid VariationRequestId { get; set; }
        public decimal? VariationMainContractorPreliminariesAmount { get; set; }
        public string VariationMainContractorPreliminariesDescription { get; set; }
        public decimal? VariationAccessAmount { get; set; }
        public string VariationAccessDescription { get; set; }
        public decimal? VariationOverheadsAndProfitAmount { get; set; }
        public string VariationOverheadsAndProfitDescription { get; set; }
        public decimal? VariationContractorContingenciesAmount { get; set; }
        public string VariationContractorContingenciesDescription { get; set; }
    }
}
