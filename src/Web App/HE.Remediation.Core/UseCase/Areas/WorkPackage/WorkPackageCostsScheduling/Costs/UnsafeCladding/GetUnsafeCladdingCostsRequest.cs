using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.UnsafeCladding;

public class GetUnsafeCladdingCostsRequest : IRequest<GetUnsafeCladdingCostsResponse>
{
    private GetUnsafeCladdingCostsRequest()
    {
    }

    public static readonly GetUnsafeCladdingCostsRequest Request = new();
}