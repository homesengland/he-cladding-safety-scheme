using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.ProjectTeam;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeam;

public class RegulatoryComplianceViewModel : WorkPackageBaseViewModel
{
    public List<ProjectTeamMembersResult> TeamMembers { get; set; }
    public Guid? RegulatoryCompliancePersonId { get; set; }
}