using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageInternalDefects;

public class InternalDefectsCostViewModel : WorkPackageBaseViewModel
{
    public decimal? InternalDefectsCosts { get; set; }
    public string Description { get; set; }
}