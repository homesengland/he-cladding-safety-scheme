namespace HE.Remediation.Core.Data.StoredProcedureResults.VariationRequest
{
    public class GetVariationOtherCostsResult
    {

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


        public decimal? WorkPackageFraewSurveyCostsAmount { get; set; }
        public decimal? WorkPackageFeasibilityStageAmount { get; set; }
        public string WorkPackageFeasibilityStageDescription { get; set; }
        public decimal? WorkPackagePostTenderStageAmount { get; set; }
        public string WorkPackagePostTenderStageDescription { get; set; }
        public decimal? WorkPackageIrrecoverableVatAmount { get; set; }
        public string WorkPackageIrrecoverableVatDescription { get; set; }
        public decimal? WorkPackagePropertyManagerAmount { get; set; }
        public string WorkPackagePropertyManagerDescription { get; set; }
    }
}
