using HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class PreliminaryCostsViewModel : VariationRequestBaseViewModel
    {
        public string VariationMainContractorPreliminariesAmountText { get; set; }

        public decimal? VariationMainContractorPreliminariesAmount =>
            decimal.TryParse(this.VariationMainContractorPreliminariesAmountText, out var amount)
                ? amount
                : null;

        public string VariationMainContractorPreliminariesDescription { get; set; }
        public decimal? WorkPackageMainContractorPreliminariesAmount { get; set; }
        public string WorkPackageMainContractorPreliminariesDescription { get; set; }


        public string VariationAccessAmountText { get; set; }

        public decimal? VariationAccessAmount =>
            decimal.TryParse(this.VariationAccessAmountText, out var amount)
                ? amount
                : null;

        public string VariationAccessDescription { get; set; }
        public decimal? WorkPackageAccessAmount { get; set; }
        public string WorkPackageAccessDescription { get; set; }


        public string VariationOverheadsAndProfitAmountText { get; set; }

        public decimal? VariationOverheadsAndProfitAmount =>
            decimal.TryParse(this.VariationOverheadsAndProfitAmountText, out var amount)
                ? amount
                : null;

        public string VariationOverheadsAndProfitDescription { get; set; }
        public decimal? WorkPackageOverheadsAndProfitAmount { get; set; }
        public string WorkPackageOverheadsAndProfitDescription { get; set; }


        public string VariationContractorContingenciesAmountText { get; set; }

        public decimal? VariationContractorContingenciesAmount =>
            decimal.TryParse(this.VariationContractorContingenciesAmountText, out var amount)
                ? amount
                : null;

        public string VariationContractorContingenciesDescription { get; set; }
        public decimal? WorkPackageContractorContingenciesAmount { get; set; }
        public string WorkPackageContractorContingenciesDescription { get; set; }
    }
}
