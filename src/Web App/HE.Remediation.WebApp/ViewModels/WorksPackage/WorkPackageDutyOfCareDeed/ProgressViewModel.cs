using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageDutyOfCareDeed;

public class ProgressViewModel : WorkPackageBaseViewModel
{
    public DateTime? DateSentToGrantCertifyingOfficer { get; set; }

    public DateTime? DateSignedByGrantCertifyingOfficer { get; set; }

    public ETaskStatus Status { get; set; }
}
