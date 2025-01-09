using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.GetRemoveTeamMember;

public class GetRemoveTeamMemberRequest : IRequest<GetRemoveTeamMemberResponse>
{
    public Guid TeamMemberId { get; set; }

    public GetRemoveTeamMemberRequest()
    {
    }
}
