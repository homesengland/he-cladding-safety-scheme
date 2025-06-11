namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class JoinViewModel
    {
        public string RequestorFullName { get; set; }
        public Guid CollaborationUserId { get; set; }
        public Guid ApplicationDetailsId { get; set; }
        public bool NewSignUp { get; set; }
    }
}
