using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;
using System.ComponentModel.DataAnnotations;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageKeyDates
{
    public class ChangeAnswersViewModel : WorkPackageBaseViewModel
    {
        [Required] public bool? Proceed { get; set; }
    }
}
