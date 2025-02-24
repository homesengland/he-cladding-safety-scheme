namespace HE.Remediation.Core.Data.StoredProcedureParameters
{
    public class InsertCommunicationParameters
    {
        public Guid ApplicationId { get; set; }
        public Guid? CommunicationFileId { get; set; }
        public int CommunicationDirectionId { get; set; }
        public int CommunicationTriggerId { get; set; }
        public int CommunicationCategoryId { get; set; }
        public int CommunicationTypeId { get; set; }
        public Guid? AddedByUserId { get; set; }
        public DateTime DateSent { get; set; }
        public DateTime DateAdded { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Notes { get; set; }
        public Guid? GovNotifyNotificationId { get; set; }
        public int? GovNotifyNotificationStatusId { get; set; }
        public int? GovNotifyNotificationTypeId { get; set; }

    }
}
