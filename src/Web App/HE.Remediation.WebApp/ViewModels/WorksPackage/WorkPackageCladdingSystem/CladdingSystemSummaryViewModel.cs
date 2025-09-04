using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCladdingSystem;

public class CladdingSystemSummaryViewModel : WorkPackageBaseViewModel
{
    public Guid? FireRiskCladdingSystemsId { get; set; }

    public string CladdingSystemTypeName { get; set; }

    public ETaskStatus CladdingSystemTaskStatusId { get; set; }
}