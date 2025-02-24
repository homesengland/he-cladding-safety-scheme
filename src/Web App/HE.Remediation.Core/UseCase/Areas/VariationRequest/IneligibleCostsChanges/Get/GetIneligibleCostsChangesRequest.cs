using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.IneligibleCostsChanges.Get;

public class GetIneligibleCostsChangesRequest : IRequest<GetIneligibleCostsChangesResponse>
{
    private GetIneligibleCostsChangesRequest()
    {
    }

    public static GetIneligibleCostsChangesRequest Request => new();
}
