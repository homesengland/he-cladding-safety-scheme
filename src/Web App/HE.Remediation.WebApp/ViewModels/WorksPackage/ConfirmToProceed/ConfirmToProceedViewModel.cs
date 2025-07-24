using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.ConfirmToProceed
{
    public class ConfirmToProceedViewModel : WorkPackageBaseViewModel
    {
        public ENoYes? IsConfirmedToProceed { get; set; }
    }
}
