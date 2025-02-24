using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class RemoveSubcontractorViewModel : WorkPackageBaseViewModel
{
    public Guid SubcontractorId { get; set; }
    public bool? Confirm { get; set; }
}
