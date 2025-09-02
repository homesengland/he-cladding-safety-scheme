using HE.Remediation.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails
{
    public class EvidenceSubmissionUploadRequest : IRequest
    {
        public IFormFile EvidenceSubmissionFile { get; set; }
    }
}
