namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.UnsafeCladdingCosts.Get
{
    public class GetUnsafeCladdingCostsResponse
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public bool IsSubmitted { get; set; }
        public bool HasCostVariation { get; set; } = true;
        public bool? IsThirdPartyContributionVariation { get; set; }
        public decimal? VariationRemovalOfCladdingAmount { get; set; }
        public string VariationRemovalOfCladdingDescription { get; set; }
        public decimal? WorkPackageRemovalOfCladdingAmount { get; set; }
        public string WorkPackageRemovalOfCladdingDescription { get; set; }
    }
}
