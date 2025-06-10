namespace HE.Remediation.Core.Services.GovNotify.Models
{
    public class GovNotifySettings
    {
        public Guid ApplicationSubmittedEmailTemplateId { get; set; }

        public Guid WorksPackageSubmittedEmailTemplateId { get; set; }

        public Guid ScheduleOfWorksSubmittedEmailTemplateId { get; set; }

        public Guid PaymentRequestSubmittedEmailTemplateId { get; set; }

        public Guid VariationSubmittedEmailTemplateId { get; set; }

        public Guid ClosingReportSubmittedEmailTemplateId { get; set; }

        public Guid CollaborationOrganisationInviteEmailTemplateId { get; set; }

        public Guid CollaborationOrganisationInviteRemovalEmailTemplateId { get; set; }
    }
}
