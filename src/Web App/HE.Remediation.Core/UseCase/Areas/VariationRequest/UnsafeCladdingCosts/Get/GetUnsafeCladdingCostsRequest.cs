using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.UnsafeCladdingCosts.Get;

public class GetUnsafeCladdingCostsRequest : IRequest<GetUnsafeCladdingCostsResponse>
{
    private GetUnsafeCladdingCostsRequest()
    {
    }

    public static GetUnsafeCladdingCostsRequest Request => new();
}