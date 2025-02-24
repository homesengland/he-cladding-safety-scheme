using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Costs.Get;

public class GetCostsRequest : IRequest<GetCostsResponse>
{
    private GetCostsRequest()
    {
    }

    public static GetCostsRequest Request => new();
}
