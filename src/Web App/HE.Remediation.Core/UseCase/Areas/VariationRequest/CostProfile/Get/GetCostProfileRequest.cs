using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.CostProfile.Get;

public class GetCostProfileRequest : IRequest<GetCostProfileResponse>
{
    private GetCostProfileRequest()
    {
    }

    public static GetCostProfileRequest Request => new();
}
