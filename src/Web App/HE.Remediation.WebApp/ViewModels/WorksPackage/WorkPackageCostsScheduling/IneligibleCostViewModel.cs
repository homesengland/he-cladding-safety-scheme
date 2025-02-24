using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class IneligibleCostViewModel : WorkPackageBaseViewModel
{
    public ENoYes? IneligibleCosts { get; set; }
}