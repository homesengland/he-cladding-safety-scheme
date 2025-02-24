namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Costs.Get
{
    public class GetCostsResponse
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public bool IsSubmitted { get; set; }
        public bool HasCostVariation { get; set; } = true;
        public bool? IsThirdPartyContributionVariation { get; set; }
        public decimal? UnsafeCladdingRemovalAmount { get; set; }
        public decimal? NewCladdingAmount { get; set; }
        public decimal? ExternalWorksAmount { get; set; }
        public decimal? InternalWorksAmount { get; set; }
        public decimal? MainContractorPreliminariesAmount { get; set; }
        public decimal? AccessAmount { get; set; }
        public decimal? MainContractorOverheadAmount { get; set; }
        public decimal? ContractorContingenciesAmount { get; set; }
        public decimal? FraewSurveyAmount { get; set; }
        public decimal? FeasibilityStageAmount { get; set; }
        public decimal? PostTenderStageAmount { get; set; }
        public decimal? PropertyManagerAmount { get; set; }
        public decimal? IrrecoverableVatAmount { get; set; }
        public decimal? VariationUnsafeCladdingRemovalAmount { get; set; }
        public decimal? VariationNewCladdingAmount { get; set; }
        public decimal? VariationExternalWorksAmount { get; set; }
        public decimal? VariationInternalWorksAmount { get; set; }
        public decimal? VariationMainContractorPreliminariesAmount { get; set; }
        public decimal? VariationAccessAmount { get; set; }
        public decimal? VariationMainContractorOverheadAmount { get; set; }
        public decimal? VariationContractorContingenciesAmount { get; set; }
        public decimal? VariationFraewSurveyAmount { get; set; }
        public decimal? VariationFeasibilityStageAmount { get; set; }
        public decimal? VariationPostTenderStageAmount { get; set; }
        public decimal? VariationPropertyManagerAmount { get; set; }
        public decimal? VariationIrrecoverableVatAmount { get; set; }
    }
}
