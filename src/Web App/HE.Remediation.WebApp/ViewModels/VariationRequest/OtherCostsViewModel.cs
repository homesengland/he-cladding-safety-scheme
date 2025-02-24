using HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class OtherCostsViewModel : VariationRequestBaseViewModel
    {
        public string VariationFraewSurveyCostsAmountText { get; set; }

        public decimal? VariationFraewSurveyCostsAmount =>
            decimal.TryParse(this.VariationFraewSurveyCostsAmountText, out var amount)
                ? amount
                : null;

        public string VariationFraewSurveyCostsDescription { get; set; }
        public decimal? WorkPackageFraewSurveyCostsAmount { get; set; }


        public string VariationFeasibilityStageAmountText { get; set; }

        public decimal? VariationFeasibilityStageAmount =>
            decimal.TryParse(this.VariationFeasibilityStageAmountText, out var amount)
                ? amount
                : null;

        public string VariationFeasibilityStageDescription { get; set; }
        public decimal? WorkPackageFeasibilityStageAmount { get; set; }
        public string WorkPackageFeasibilityStageDescription { get; set; }


        public string VariationPostTenderStageAmountText { get; set; }

        public decimal? VariationPostTenderStageAmount =>
            decimal.TryParse(this.VariationPostTenderStageAmountText, out var amount)
                ? amount
                : null;

        public string VariationPostTenderStageDescription { get; set; }
        public decimal? WorkPackagePostTenderStageAmount { get; set; }
        public string WorkPackagePostTenderStageDescription { get; set; }


        public string VariationIrrecoverableVatAmountText { get; set; }

        public decimal? VariationIrrecoverableVatAmount =>
            decimal.TryParse(this.VariationIrrecoverableVatAmountText, out var amount)
                ? amount
                : null;

        public string VariationIrrecoverableVatDescription { get; set; }
        public decimal? WorkPackageIrrecoverableVatAmount { get; set; }
        public string WorkPackageIrrecoverableVatDescription { get; set; }


        public string VariationPropertyManagerAmountText { get; set; }

        public decimal? VariationPropertyManagerAmount =>
            decimal.TryParse(this.VariationPropertyManagerAmountText, out var amount)
                ? amount
                : null;

        public string VariationPropertyManagerDescription { get; set; }
        public decimal? WorkPackagePropertyManagerAmount { get; set; }
        public string WorkPackagePropertyManagerDescription { get; set; }
    }
}
