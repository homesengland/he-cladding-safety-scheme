namespace HE.Remediation.Core.Services.UserService.Model
{
    public class UserInvitesModel
    {
        public Guid UserId { get; set; }
        public Guid CollaborationUserId { get; set; }
        public Guid? CollaborationOrganisationId { get; set; }
        public Guid? ApplicationDetailsId { get; set; }
    }
}
