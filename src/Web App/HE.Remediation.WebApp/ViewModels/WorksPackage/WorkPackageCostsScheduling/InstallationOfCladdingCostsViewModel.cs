using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class InstallationOfCladdingCostsViewModel : WorkPackageBaseViewModel
{
    public string NewCladdingAmountText { get; set; }
    public decimal? NewCladdingAmount => decimal.TryParse(NewCladdingAmountText, out var amount) ? amount : null;
    public string NewCladdingDescription { get; set; }

    public string ExternalWorksAmountText { get; set; }
    public decimal? ExternalWorksAmount => decimal.TryParse(ExternalWorksAmountText, out var amount) ? amount : null;
    public string ExternalWorksDescription { get; set; }

    public string InternalWorksAmountText { get; set; }
    public decimal? InternalWorksAmount => decimal.TryParse(this.InternalWorksAmountText, out var amount) ? amount : null;
    public string InternalWorksDescription { get; set; }
}