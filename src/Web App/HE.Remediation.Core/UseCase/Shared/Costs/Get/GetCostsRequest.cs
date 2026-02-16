using Mediator;

namespace HE.Remediation.Core.UseCase.Shared.Costs.Get;

public class GetCostsRequest : IRequest<GetCostsResponse>
{
    private GetCostsRequest()
    {
    }

    public static GetCostsRequest Request => new();
}
