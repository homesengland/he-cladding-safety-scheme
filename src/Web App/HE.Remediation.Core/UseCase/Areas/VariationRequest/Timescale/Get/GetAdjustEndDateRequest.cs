using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Timescale.Get;

public class GetAdjustEndDateRequest : IRequest<GetAdjustEndDateResponse>
{
    private GetAdjustEndDateRequest()
    {
    }

    public static GetAdjustEndDateRequest Request => new();
}
