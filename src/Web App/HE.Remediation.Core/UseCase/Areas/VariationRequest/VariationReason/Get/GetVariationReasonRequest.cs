using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.VariationReason.Get;

public class GetVariationReasonRequest : IRequest<GetVariationReasonResponse>
{
    private GetVariationReasonRequest()
    {
    }

    public static GetVariationReasonRequest Request => new();
}
