using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Evidence.Get;

public class GetEvidenceRequest : IRequest<GetEvidenceResponse>
{
    private GetEvidenceRequest()
    {
    }

    public static GetEvidenceRequest Request => new();
}
