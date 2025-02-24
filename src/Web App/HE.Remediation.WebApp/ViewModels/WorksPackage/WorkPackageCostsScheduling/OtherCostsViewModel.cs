using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class OtherCostsViewModel : WorkPackageBaseViewModel
{
    public string FraewSurveyAmountText { get; set; }
    public decimal? FraewSurveyAmount => decimal.TryParse(FraewSurveyAmountText, out var amount) ? amount : null;

    public string FeasibilityStageAmountText { get; set; }
    public decimal? FeasibilityStageAmount => decimal.TryParse(FeasibilityStageAmountText, out var amount) ? amount : null;
    public string FeasibilityStageDescription { get; set; }

    public string PostTenderStageAmountText { get; set; }
    public decimal? PostTenderStageAmount => decimal.TryParse(PostTenderStageAmountText, out var amount) ? amount : null;
    public string PostTenderStageDescription { get; set; }
    
    public string PropertyManagerAmountText { get; set; }
    public decimal? PropertyManagerAmount => decimal.TryParse(PropertyManagerAmountText, out var amount) ? amount : null;
    public string PropertyManagerDescription { get; set; }

    public string IrrecoverableVatAmountText { get; set; }
    public decimal? IrrecoverableVatAmount => decimal.TryParse(IrrecoverableVatAmountText, out var amount) ? amount : null;
    public string IrrecoverableVatDescription { get; set; }
}