using HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class UnsafeCladdingCostsViewModel : VariationRequestBaseViewModel
    {
        public string VariationRemovalOfCladdingAmountText { get; set; }

        public decimal? VariationRemovalOfCladdingAmount =>
            decimal.TryParse(this.VariationRemovalOfCladdingAmountText, out var amount)
                ? amount
                : null;

        public string UnsafeCladdingRemovalDescription { get; set; }
        public string VariationRemovalOfCladdingDescription { get; set; }
        public decimal? WorkPackageRemovalOfCladdingAmount { get; set; }
        public string WorkPackageRemovalOfCladdingDescription { get; set; }
    }
}
