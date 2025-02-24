namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class ResponsibleEntityPrimaryContactDetailsViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public bool IsIndividuelOrganisationSubType { get; set; }
        public bool IsUkBased { get; set; }

        public string ReturnUrl { get; set; }
    }
}
