using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class RemoveAccessViewModel
    {
        public Guid? TeamMemberId { get; set; }
        public string InvitedName { get; set; }
        public ETeamMemberSource Source { get; set; }
    }
}
