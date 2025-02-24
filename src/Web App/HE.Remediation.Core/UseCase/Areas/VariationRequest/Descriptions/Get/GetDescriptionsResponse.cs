namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Descriptions.Get
{
    public class GetDescriptionsResponse
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public bool IsSubmitted { get; set; }
        public bool HasCostVariation { get; set; } = true;
        public bool? IsThirdPartyContributionVariation { get; set; }
        public string VariationFraewSurveyCostsDescription { get; set; }
        public string VariationRemovalOfCladdingDescription { get; set; }
        public string VariationNewCladdingDescription { get; set; }
        public string VariationOtherEligibleWorkToExternalWallDescription { get; set; }
        public string VariationInternalMitigationWorksDescription { get; set; }
        public string VariationMainContractorPreliminariesDescription { get; set; }
        public string VariationAccessDescription { get; set; }
        public string VariationOverheadsAndProfitDescription { get; set; }
        public string VariationContractorContingenciesDescription { get; set; }
        public string VariationFeasibilityStageDescription { get; set; }
        public string VariationPostTenderStageDescription { get; set; }
        public string VariationIrrecoverableVatDescription { get; set; }
        public string VariationPropertyManagerDescription { get; set; }
    }
}
