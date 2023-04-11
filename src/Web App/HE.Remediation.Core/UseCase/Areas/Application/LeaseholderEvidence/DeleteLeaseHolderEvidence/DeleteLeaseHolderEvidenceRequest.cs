using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.LeaseHolderEvidence.DeleteLeaseHolderEvidence
{
    public class DeleteLeaseHolderEvidenceRequest: IRequest
    {
        public Guid FileId { get; set; }
    }
}
