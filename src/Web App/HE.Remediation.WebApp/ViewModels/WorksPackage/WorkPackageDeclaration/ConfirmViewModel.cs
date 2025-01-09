using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageDeclaration;

public class ConfirmViewModel : WorkPackageBaseViewModel
{
    public bool? AllCostsReasonable { get; set; }
    
    public bool? AllContractualRequirementsMet { get; set; }

    public bool? AllCostsSetOutInFull { get; set; }

    public bool? AcceptGrantAwardBasedOnCosts { get; set; }
}
