using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class InvitationDeclarationViewModel
    {
        public Guid? TeamMemberId { get; set; }
        public bool IsDeclarationConfimed { get; set; }
        public ETeamMemberSource Source { get; set; }
    }
}
