namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.InstallationOfCladdingCosts.Get
{
    public class GetInstallationOfCladdingCostsResponse
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public bool IsSubmitted { get; set; }
        public bool HasCostVariation { get; set; } = true;
        public bool? IsThirdPartyContributionVariation { get; set; }
        public decimal? VariationNewCladdingAmount { get; set; }
        public string VariationNewCladdingDescription { get; set; }
        public decimal? WorkPackageNewCladdingAmount { get; set; }
        public string WorkPackageNewCladdingDescription { get; set; }
        public decimal? VariationOtherEligibleWorkToExternalWallAmount { get; set; }
        public string VariationOtherEligibleWorkToExternalWallDescription { get; set; }
        public decimal? WorkPackageOtherEligibleWorkToExternalWallAmount { get; set; }
        public string WorkPackageOtherEligibleWorkToExternalWallDescription { get; set; }
        public decimal? VariationInternalMitigationWorksAmount { get; set; }
        public string VariationInternalMitigationWorksDescription { get; set; }
        public decimal? WorkPackageInternalMitigationWorksAmount { get; set; }
        public string WorkPackageInternalMitigationWorksDescription { get; set; }
    }
}
