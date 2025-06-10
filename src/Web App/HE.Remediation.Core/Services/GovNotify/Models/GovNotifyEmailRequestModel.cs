namespace HE.Remediation.Core.Services.GovNotify.Models
{
    public class GovNotifyEmailRequestModel<TPersonalisationParameters>
        where TPersonalisationParameters : GlobalPersonalisationParameters
    {
        public Guid TemplateId { get; set; }
        public string EmailAddress { get; set; }
        public TPersonalisationParameters Personalisation { get; set; }
    }

    public class ApplicationUpdateParameters : GlobalPersonalisationParameters
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string BuildingName { get; set; }
        public string ReferenceNumber { get; set; }
    }

    public class OrganisationInviteParameters : GlobalPersonalisationParameters
    {
        public string FirstName { get; set; }
        public string RequestorFullName { get; set; }
    }

    public class OrganisationRemovalParameters : GlobalPersonalisationParameters
    {
        public string FirstName { get; set; }
        public string OrganisationName { get; set; }
        public string AdminUserEmailAddress { get; set; }
    }

    /// <summary>
    /// These are required fields for all templates
    /// </summary>
    public abstract class GlobalPersonalisationParameters
    {
        public string RecipientEmail { get; set; }
    }
}
