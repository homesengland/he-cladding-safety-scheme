using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.AboutCosts.Get;

public class GetAboutCostsRequest : IRequest<GetAboutCostsResponse>
{
    private GetAboutCostsRequest()
    {
    }

    public static readonly GetAboutCostsRequest Request = new();
}
