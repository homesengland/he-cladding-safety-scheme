using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetLeaseholderResidentUploadEvidence;

public class GetLeaseholderResidentUploadEvidenceRequest : IRequest<GetLeaseholderResidentUploadEvidenceResponse>
{
    private GetLeaseholderResidentUploadEvidenceRequest()
    {
    }

    public static readonly GetLeaseholderResidentUploadEvidenceRequest Request = new();
}
