using Mediator;
using Microsoft.AspNetCore.Http;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetLeaseholderResidentUploadEvidence;

public class SetLeaseholderResidentUploadEvidenceRequest : IRequest
{
    public IFormFile File { get; set; }
    public int? LastCommunicationDateMonth { get; set; }
    public int? LastCommunicationDateYear { get; set; }
}
