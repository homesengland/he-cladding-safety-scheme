using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.ProjectTeam;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.ExistingMember.Get;

public class GetExistingMemberResponse
{
    public ETeamRole TeamRole { get; set; }
    public List<ProjectTeamMembersResult> TeamMembers { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }
}