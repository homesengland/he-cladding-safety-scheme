using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.TeamMember;

public class GetRemoveTeamMemberResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public Guid TeamMemberId { get; set; }

    public string TeamMemberName { get; set; }
    public ETeamRole Role { get; set; }
}
