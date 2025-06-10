using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureParameters
{
    public class UpdateLeaseHolderResponsibleForCommunicationTypeParameters
    {
        public Guid LeaseHolderEngagementId { get; set; }
        public EResponsibleForCommunicationType ResponsibleForCommunicationTypeId { get; set; }
        public string? RepresentationOtherText { get; set; }
    }
}
