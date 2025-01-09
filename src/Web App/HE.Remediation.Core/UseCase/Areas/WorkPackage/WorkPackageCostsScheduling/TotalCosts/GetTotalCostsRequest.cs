using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.TotalCosts;

public class GetTotalCostsRequest : IRequest<GetTotalCostsResponse>
{
    private GetTotalCostsRequest()
    {
    }

    public static readonly GetTotalCostsRequest Request = new();
}