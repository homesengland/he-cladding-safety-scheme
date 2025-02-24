using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.IneligibleCosts.Get;

public class GetIneligibleCostsRequest : IRequest<GetIneligibleCostsResponse>
{
    private GetIneligibleCostsRequest()
    {
    }

    public static GetIneligibleCostsRequest Request => new();
}
