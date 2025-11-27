using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class ResponsibleEntityCompanyDetailsViewModel
    {
        public EApplicationScheme ApplicationScheme { get; set; }

        public string CompanyName { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public bool IsUkBased { get; set; }

        public string ReturnUrl { get; set; }
    }
}
