using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Invite;

public class GetInviteResponse
{
    public Guid? TeamMemberId { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public ETeamMemberSource Source { get; set; }
}
