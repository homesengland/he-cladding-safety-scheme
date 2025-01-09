using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeamMember;

public class AddViewModel : WorkPackageBaseViewModel
{
    public ETeamRole? TeamRole { get; set; }
    
    public List<ETeamRole> AvailableTeamRoles { get; set; }
}