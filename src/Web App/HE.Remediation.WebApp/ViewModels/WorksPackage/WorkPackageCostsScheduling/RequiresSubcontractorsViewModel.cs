using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class RequiresSubcontractorsViewModel : WorkPackageBaseViewModel
{
    public ENoYes? RequiresSubcontractors { get; set; }
    
    public ENoYes? SoughtQuotes { get; set; }
}