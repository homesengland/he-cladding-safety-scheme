 using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.Administration
{
    public class AdminSecondaryContactDetailsViewModel
    {
        public Guid? Id { get; set; }

        public string Name { get;set; }

        public string ContactNumber { get;set; }

        public string EmailAddress { get;set; }

        public ESubmitAction SubmitAction { get; set; }
    }
}
