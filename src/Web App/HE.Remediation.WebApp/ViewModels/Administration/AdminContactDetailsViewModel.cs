using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.Administration
{
    public class AdminContactDetailsViewModel
    {
        public string FirstName { get;set; }

        public string LastName { get;set; }

        public string ContactNumber { get;set; }

        public ESubmitAction SubmitAction { get; set; }
    }
}
