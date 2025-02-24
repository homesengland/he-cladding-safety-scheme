using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.OtherCosts.Get;

public class GetOtherCostsRequest : IRequest<GetOtherCostsResponse>
{
    private GetOtherCostsRequest()
    {
    }

    public static GetOtherCostsRequest Request => new();
}
