using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.PreliminaryCosts.Get;

public class GetPreliminaryCostsRequest : IRequest<GetPreliminaryCostsResponse>
{
    private GetPreliminaryCostsRequest()
    {
    }

    public static GetPreliminaryCostsRequest Request => new();
}

