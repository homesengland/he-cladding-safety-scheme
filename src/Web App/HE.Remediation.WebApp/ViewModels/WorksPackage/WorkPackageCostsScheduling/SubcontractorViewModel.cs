using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class SubcontractorViewModel : WorkPackageBaseViewModel
{
    public Guid? SubcontractorId { get; set; }
    public string CompanyName { get; set; }
    public string CompanyRegistration { get; set; }
}