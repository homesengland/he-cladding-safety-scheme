using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;
using System.ComponentModel.DataAnnotations;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackagePlanningPermission
{
    public class ChangeAnswersViewModel : WorkPackageBaseViewModel
    {
        [Required] public bool? Proceed { get; set; }
    }
}
