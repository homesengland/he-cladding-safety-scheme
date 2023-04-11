using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.GetLeaseHolderEvidence
{
    public class GetLeaseHolderEvidenceRequest : IRequest<IReadOnlyCollection<GetLeaseHolderEvidenceResponse>>
    {
        private GetLeaseHolderEvidenceRequest() { }

        public static readonly GetLeaseHolderEvidenceRequest Request = new();
    }
}
