using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.Administration;

public class AdditionalContactsViewModel
{
    public List<SecondaryContactDetails> ContactDetails { get; set; }

    public class SecondaryContactDetails
    {
        public string Id { get; set; }  

        public string Name { get;set; }

        public string ContactNumber { get;set; }

        public string EmailAddress { get;set; }

        public ESubmitAction SubmitAction { get; set; }
    }
}
