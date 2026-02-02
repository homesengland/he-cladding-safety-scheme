using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.InviteMember;

public class SetInviteMemberRequest(Guid teamMemberId, string auth0UserId, ETeamMemberSource source) : IRequest<SetInviteMemberResponse>
{
    public Guid TeamMemberId { get; set; } = teamMemberId;
    public string Auth0UserId { get; set; } = auth0UserId;
    public ETeamMemberSource Source { get; set; } = source;
}
