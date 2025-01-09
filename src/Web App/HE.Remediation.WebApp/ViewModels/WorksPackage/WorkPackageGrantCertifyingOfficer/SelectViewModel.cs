using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageGrantCertifyingOfficer;

public class SelectViewModel : WorkPackageBaseViewModel
{
    public Guid? SelectedProjectTeamMemberId { get; set; }

    public IReadOnlyCollection<CandidateViewModel> Candidates { get; set; }
}
