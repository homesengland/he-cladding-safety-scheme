
namespace HE.Remediation.Core.Services.UserService.Model
{
    public class UserInvitesPendingModel
    {
        public bool IsOrganisationInvitePending { get; set; }
        public bool IsOrganisationInviteRevoked { get; set; }

        public bool IsApplicationInvitePending { get; set; }

        public bool Any()
        {
            return IsOrganisationInvitePending || IsApplicationInvitePending;
        }
    }
}
