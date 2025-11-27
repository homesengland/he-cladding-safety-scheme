using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.TeamMember;
public class GetRemoveTeamMemberRequest : IRequest<GetRemoveTeamMemberResponse>
{
    public Guid TeamMemberId { get; set; }

    public GetRemoveTeamMemberRequest()
    {
    }
}
