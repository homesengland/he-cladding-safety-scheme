using HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class IneligibleCostsChangesViewModel : VariationRequestBaseViewModel
    {
        public string VariationIneligibleAmountText { get; set; }

        public decimal? VariationIneligibleAmount =>
            decimal.TryParse(this.VariationIneligibleAmountText, out var amount)
                ? amount
                : null;

        public string VariationIneligibleDescription { get; set; }
        public decimal? WorkPackageIneligibleAmount { get; set; }
        public string WorkPackageIneligibleDescription { get; set; }
    }
}
