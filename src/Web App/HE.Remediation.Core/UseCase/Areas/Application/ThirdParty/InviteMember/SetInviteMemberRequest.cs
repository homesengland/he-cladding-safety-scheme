using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.InviteMember;

public class SetInviteMemberRequest(Guid teamMemberId, string auth0UserId) : IRequest<SetInviteMemberResponse>
{
    public Guid TeamMemberId { get; set; } = teamMemberId;
    public string Auth0UserId { get; set; } = auth0UserId;
}
