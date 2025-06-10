namespace HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Invite;

public class GetInviteResponse
{
    public Guid? TeamMemberId { get; set; }
    public string InvitedName { get; set; }
    public string InvitedEmailAddress { get; set; }
}
