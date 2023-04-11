using MediatR;
using Microsoft.AspNetCore.Http;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.SetLeaseholderEvidence
{
    public class SetLeaseHolderEvidenceRequest : IRequest<Unit>
    {
        public IFormFile File { get; set; }
        public bool Completed { get; set; }
    }
}
