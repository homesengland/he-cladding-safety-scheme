namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.IneligibleCostsChanges.Get
{
    public class GetIneligibleCostsChangesResponse
    {
        public string ApplicationReferenceNumber { get; set; }

        public string BuildingName { get; set; }

        public bool IsSubmitted { get; set; }


        public bool HasCostVariation { get; set; } = true;

        public bool? IsThirdPartyContributionVariation { get; set; }

        public decimal? VariationIneligibleAmount { get; set; }

        public string VariationIneligibleDescription { get; set; }

        public decimal? WorkPackageIneligibleAmount { get; set; }

        public string WorkPackageIneligibleDescription { get; set; }
    }
}
