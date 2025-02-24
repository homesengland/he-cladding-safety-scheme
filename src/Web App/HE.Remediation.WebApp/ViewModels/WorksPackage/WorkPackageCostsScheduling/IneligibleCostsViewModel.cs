using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class IneligibleCostsViewModel : WorkPackageBaseViewModel
{
    public string IneligibleAmountText { get; set; }
    public decimal? IneligibleAmount => decimal.TryParse(IneligibleAmountText, out var amount) ? amount : null;
    public string IneligibleDescription { get; set; }
}