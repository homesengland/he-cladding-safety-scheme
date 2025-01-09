namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Scope.Get
{
    public class GetAdjustScopeResponse
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public bool IsSubmitted { get; set; }
        public string ChangeOfScope { get; set; }
        public bool? IsTimescaleVariation { get; set; }
        public bool? IsCostsVariation { get; set; }
        public bool? IsThirdPartyContributionVariation { get; set; }
    }
}
