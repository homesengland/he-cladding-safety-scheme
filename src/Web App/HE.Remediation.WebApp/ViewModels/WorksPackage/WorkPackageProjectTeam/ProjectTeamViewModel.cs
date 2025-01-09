using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.ProjectTeam;
using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeam;

public class ProjectTeamViewModel : WorkPackageBaseViewModel
{
    public List<ProjectTeamMembersResult> TeamMembers { get; set; }
    
    public bool IsCopy { get; set; }

    public List<ETeamRole> MissingRoles { get; set; } = new List<ETeamRole>();
    public bool? HasChasCertificationValue { get; set; }
}