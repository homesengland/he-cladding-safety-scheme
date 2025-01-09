using HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class InstallationOfCladdingCostsViewModel : VariationRequestBaseViewModel
    {
        public string VariationNewCladdingAmountText { get; set; }

        public decimal? VariationNewCladdingAmount =>
            decimal.TryParse(this.VariationNewCladdingAmountText, out var amount)
                ? amount
                : null;

        public string VariationNewCladdingDescription { get; set; }
        public decimal? WorkPackageNewCladdingAmount { get; set; }
        public string WorkPackageNewCladdingDescription { get; set; }


        public string VariationOtherEligibleWorkToExternalWallAmountText { get; set; }

        public decimal? VariationOtherEligibleWorkToExternalWallAmount =>
            decimal.TryParse(this.VariationOtherEligibleWorkToExternalWallAmountText, out var amount)
                ? amount
                : null;

        public string VariationOtherEligibleWorkToExternalWallDescription { get; set; }
        public decimal? WorkPackageOtherEligibleWorkToExternalWallAmount { get; set; }
        public string WorkPackageOtherEligibleWorkToExternalWallDescription { get; set; }


        public string VariationInternalMitigationWorksAmountText { get; set; }

        public decimal? VariationInternalMitigationWorksAmount =>
            decimal.TryParse(this.VariationInternalMitigationWorksAmountText, out var amount)
                ? amount
                : null;

        public string VariationInternalMitigationWorksDescription { get; set; }
        public decimal? WorkPackageInternalMitigationWorksAmount { get; set; }
        public string WorkPackageInternalMitigationWorksDescription { get; set; }
    }
}
