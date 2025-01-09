namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.AboutCosts.Get
{
    public class GetAboutCostsResponse
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public bool IsSubmitted { get; set; }
        public bool? IsScopeVariation { get; set; }
        public bool? IsTimescaleVariation { get; set; }
        public bool? IsThirdPartyContributionVariation { get; set; }

    }
}
