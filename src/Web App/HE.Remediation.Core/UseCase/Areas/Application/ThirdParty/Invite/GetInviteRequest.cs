using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Invite;

public class GetInviteRequest(Guid teamMemberId) : IRequest<GetInviteResponse>
{
    public Guid TeamMemberId { get; set; } = teamMemberId;
}
