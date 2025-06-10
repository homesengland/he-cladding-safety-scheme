using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class FireRiskAssessorDetailsViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Telephone { get; set; }
        public string ReturnUrl { get; set; }
        public ESubmitAction SubmitAction { get; set; }
    }
}
