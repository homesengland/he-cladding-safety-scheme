using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class UnsafeCladdingCostsViewModel : WorkPackageBaseViewModel
{
    public string UnsafeCladdingRemovalAmountText { get; set; }

    public decimal? UnsafeCladdingRemovalAmount =>
        decimal.TryParse(this.UnsafeCladdingRemovalAmountText, out var amount)
            ? amount
            : null;

    public string UnsafeCladdingRemovalDescription { get; set; }
}