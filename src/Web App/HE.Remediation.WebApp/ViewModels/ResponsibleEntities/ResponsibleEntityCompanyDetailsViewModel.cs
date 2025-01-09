namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class ResponsibleEntityCompanyDetailsViewModel
    {
        public string CompanyName { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public bool IsUkBased { get; set; }

        public string ReturnUrl { get; set; }
    }
}
