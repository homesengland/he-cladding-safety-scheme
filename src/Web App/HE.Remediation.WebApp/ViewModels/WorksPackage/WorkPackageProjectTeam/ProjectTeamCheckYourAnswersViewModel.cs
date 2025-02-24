using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.ProjectTeam;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeam;

public class ProjectTeamCheckYourAnswersViewModel : WorkPackageBaseViewModel
{
    public List<ProjectTeamMembersResult> TeamMembers { get; set; }
    public string RegulatoryCompliancePerson { get; set; }
    public string RegulatoryCompliancePersonRole { get; set; }
}