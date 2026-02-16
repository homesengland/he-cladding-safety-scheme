using Mediator;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.RemoveAccess;

public class SetRemoveAccessRequest(Guid teamMemberId, string auth0UserId, ETeamMemberSource source) : IRequest<SetRemoveAccessResponse>
{
    public Guid TeamMemberId { get; set; } = teamMemberId;
    public string Auth0UserId { get; set; } = auth0UserId;
    public ETeamMemberSource Source { get; set; } = source;
}
