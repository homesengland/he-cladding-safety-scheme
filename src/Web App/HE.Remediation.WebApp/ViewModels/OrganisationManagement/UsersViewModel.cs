using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.OrganisationManagement
{
    public class UsersViewModel
    {
        public Guid OrganisationId { get; set; }
        public string OrganisationName { get; set; }
        public EApplicationRole ApplicationRoleId { get; set; }

        public List<CollaborationUser> Users { get; set; }

        public record CollaborationUser(Guid Id, string Name, string Email, string Role, string UserStatus, string Auth0UserId);
    }
}
