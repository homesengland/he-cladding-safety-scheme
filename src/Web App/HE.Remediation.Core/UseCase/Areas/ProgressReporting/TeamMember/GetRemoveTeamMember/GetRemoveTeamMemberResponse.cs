namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.GetRemoveTeamMember;

public class GetRemoveTeamMemberResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public Guid TeamMemberId { get; set; }

    public string TeamMemberName { get; set; }
}
