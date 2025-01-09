using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;
using System.ComponentModel.DataAnnotations;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageGrantCertifyingOfficer
{
    public class ChangeAnswersViewModel : WorkPackageBaseViewModel
    {
        [Required] public bool? Proceed { get; set; }
    }
}
