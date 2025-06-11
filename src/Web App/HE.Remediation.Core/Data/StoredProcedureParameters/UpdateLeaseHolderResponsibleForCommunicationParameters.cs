using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureParameters
{
    public class UpdateLeaseHolderResponsibleForCommunicationParameters
    {
        public Guid LeaseHolderEngagementId { get; set; }
        public ENoYes? ResponsibleForCommunication { get; set; }
    }
}
