using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Overview;

public class GetCostsRequest : IRequest<GetCostsResponse>
{
    private GetCostsRequest()
    {
    }

    public static readonly GetCostsRequest Request = new();
}