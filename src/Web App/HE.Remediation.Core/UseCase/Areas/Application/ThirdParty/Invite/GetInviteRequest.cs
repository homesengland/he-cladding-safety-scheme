using MediatR;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Invite;

public class GetInviteRequest(Guid teamMemberId, ETeamMemberSource source) : IRequest<GetInviteResponse>
{
    public Guid TeamMemberId { get; set; } = teamMemberId;
    public ETeamMemberSource Source { get; set; } = source;
}
