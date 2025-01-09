namespace HE.Remediation.Core.Services.GovNotify.Models
{
    public class GovNotifyEmailRequestModel
    {
        public Guid TemplateId { get; set; }
        public string EmailAddress { get; set; }
        public PersonalisationModel Personalisation { get; set; }

        public class PersonalisationModel
        {
            public string FirstName { get; set; }
            public string Surname { get; set; }
            public string RecipientEmail { get; set; }
            public string BuildingName { get; set; }
            public string ReferenceNumber { get; set; }
        }
    }
}
