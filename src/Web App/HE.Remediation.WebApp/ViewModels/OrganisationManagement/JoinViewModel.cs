namespace HE.Remediation.WebApp.ViewModels.OrganisationManagement
{
    public class JoinViewModel
    {
        public string RequestorFullName { get; set; }
        public string OrganisationName { get; set; }
        public Guid CollaborationUserId { get; set; }
        public bool NewSignUp { get; set; }
    }
}
