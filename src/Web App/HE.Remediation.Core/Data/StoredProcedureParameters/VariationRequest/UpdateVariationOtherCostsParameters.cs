namespace HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest
{
    public class UpdateVariationOtherCostsParameters
    {
        public Guid VariationRequestId { get; set; }
        public decimal? VariationFraewSurveyCostsAmount { get; set; }
        public string VariationFraewSurveyCostsDescription { get; set; }
        public decimal? VariationFeasibilityStageAmount { get; set; }
        public string VariationFeasibilityStageDescription { get; set; }
        public decimal? VariationPostTenderStageAmount { get; set; }
        public string VariationPostTenderStageDescription { get; set; }
        public decimal? VariationIrrecoverableVatAmount { get; set; }
        public string VariationIrrecoverableVatDescription { get; set; }
        public decimal? VariationPropertyManagerAmount { get; set; }
        public string VariationPropertyManagerDescription { get; set; }
    }
}
