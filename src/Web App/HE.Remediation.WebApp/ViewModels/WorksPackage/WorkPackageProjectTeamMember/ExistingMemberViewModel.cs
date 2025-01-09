using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.ProjectTeam;
using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeamMember;

public class ExistingMemberViewModel : WorkPackageBaseViewModel
{
    public bool? SameAsPrevious { get; set; }
    
    public Guid? SelectedPreviousTeamMember { get; set; }
    
    public ETeamRole? TeamRole { get; set; }
    
    public List<ProjectTeamMembersResult> TeamMembers { get; set; }
}