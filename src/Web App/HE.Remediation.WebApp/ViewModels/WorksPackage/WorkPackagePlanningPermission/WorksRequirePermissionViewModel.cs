
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackagePlanningPermission;

public class WorksRequirePermissionViewModel : WorkPackageBaseViewModel
{
    public bool? PermissionRequired { get; set; }
    public string ReasonPermissionNotRequired { get; set; }
}
