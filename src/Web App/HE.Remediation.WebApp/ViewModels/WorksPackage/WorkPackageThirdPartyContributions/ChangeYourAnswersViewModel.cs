using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;
using System.ComponentModel.DataAnnotations;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageThirdPartyContributions
{
    public class ChangeYourAnswersViewModel : WorkPackageBaseViewModel
    {
        [Required] public bool? Proceed { get; set; }
    }
}
