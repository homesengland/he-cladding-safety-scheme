using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.Evidence.GetEvidence;

public class GetEvidenceRequest : IRequest<GetEvidenceResponse>
{
    private GetEvidenceRequest()
    {
    }

    public static readonly GetEvidenceRequest Request = new();
}
