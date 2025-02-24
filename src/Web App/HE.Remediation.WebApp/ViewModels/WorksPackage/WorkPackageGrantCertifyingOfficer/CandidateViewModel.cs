using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageGrantCertifyingOfficer;

public class CandidateViewModel
{
    public Guid ProjectTeamMemberId { get; set; }

    public string Name { get; set; }

    public ETeamRole RoleId { get; set; }

    public string RoleName { get; set; }
}
