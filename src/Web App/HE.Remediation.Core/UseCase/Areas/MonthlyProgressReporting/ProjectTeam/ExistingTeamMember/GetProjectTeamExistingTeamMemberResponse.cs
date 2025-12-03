using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.ProjectTeam;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.ExistingTeamMember;
public class GetProjectTeamExistingTeamMemberResponse
{
    public ETeamRole TeamRole { get; set; }

    public List<GetTeamMembersResult> TeamMembers { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }
}