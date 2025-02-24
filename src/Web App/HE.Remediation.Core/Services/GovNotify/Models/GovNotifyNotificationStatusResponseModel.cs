namespace HE.Remediation.Core.Services.GovNotify.Models
{
    public class GovNotifyNotificationStatusResponseModel
    {
        public Guid NotificationId { get; set; }
        public string Reference { get; set; }
        public string AddressLine1 { get; set; }
        public string NotificationType { get; set; }
        public string Status { get; set; }
        public NotificationTemplateModel NotificationTemplate { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public DateTime CreatedAt { get; set; }

        public class NotificationTemplateModel
        {
            public int Version { get; set; }
            public string TemplateId { get; set; }
        }
    }
}
