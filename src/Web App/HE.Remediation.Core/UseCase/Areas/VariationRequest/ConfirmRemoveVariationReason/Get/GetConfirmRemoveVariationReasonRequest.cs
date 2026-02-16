using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.ConfirmRemoveVariationReason.Get;

public class GetConfirmRemoveVariationReasonRequest : IRequest<GetConfirmRemoveVariationReasonResponse>
{
    private GetConfirmRemoveVariationReasonRequest()
    {
    }

    public static GetConfirmRemoveVariationReasonRequest Request => new();
}
