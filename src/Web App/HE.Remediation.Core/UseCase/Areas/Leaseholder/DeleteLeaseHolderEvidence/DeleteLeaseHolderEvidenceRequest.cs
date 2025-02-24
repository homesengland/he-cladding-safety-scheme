using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.DeleteLeaseHolderEvidence
{
    public class DeleteLeaseHolderEvidenceRequest : IRequest
    {
        public Guid FileId { get; set; }
    }
}
