namespace HE.Remediation.WebApp.ViewModels.OrganisationManagement
{
    public class RemoveMemberViewModel
    {
        public Guid Id { get; set; }
        public Guid CollaborationOrganisationId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
    }
}
