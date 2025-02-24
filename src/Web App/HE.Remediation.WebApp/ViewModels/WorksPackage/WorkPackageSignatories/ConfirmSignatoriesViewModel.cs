using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageSignatories;

public class ConfirmSignatoriesViewModel : WorkPackageBaseViewModel
{
    public bool? AreSignatoriesCorrect { get; set; }

    public IEnumerable<string> Signatories { get; set; }
}
