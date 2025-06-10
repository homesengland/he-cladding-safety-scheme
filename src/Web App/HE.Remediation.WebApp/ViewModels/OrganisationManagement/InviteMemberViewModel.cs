using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.OrganisationManagement
{
    public class InviteMemberViewModel
    {
        public Guid? CollaborationUserId { get; set; }
        public Guid OrganisationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public EApplicationRole ApplicationRole { get; set; }
        public ECollaborationUserStatus? UserStatus { get; set; }
    }
}
