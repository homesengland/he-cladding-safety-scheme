using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.DeleteLeaseholderResidentUploadEvidence;

public class DeleteLeaseholderResidentUploadEvidenceRequest : IRequest
{
    public Guid FileId { get; set; }

    public string ReturnUrl { get; set; }
}
