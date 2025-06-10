using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.ViewModels.OrganisationManagement
{
    public class InvitationDeclarationViewModel
    {
        [FromRoute]
        public Guid CollaborationUserId { get; set; }
        public bool IsAdminResponsibilityConfirmed { get; set; }
    }
}
