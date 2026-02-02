using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.InstallationOfCladdingCosts.Set
{
    public class SetInstallationOfCladdingCostsRequest : IRequest
    {
        public decimal? VariationNewCladdingAmount { get; set; }
        public string VariationNewCladdingDescription { get; set; }
        public decimal? VariationOtherEligibleWorkToExternalWallAmount { get; set; }
        public string VariationOtherEligibleWorkToExternalWallDescription { get; set; }
        public decimal? VariationInternalMitigationWorksAmount { get; set; }
        public string VariationInternalMitigationWorksDescription { get; set; }
    }
}
