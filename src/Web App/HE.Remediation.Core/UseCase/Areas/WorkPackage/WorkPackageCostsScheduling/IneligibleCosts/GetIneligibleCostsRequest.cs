using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.IneligibleCosts;

public class GetIneligibleCostsRequest : IRequest<GetIneligibleCostsResponse>
{
    private GetIneligibleCostsRequest()
    {
    }

    public static readonly GetIneligibleCostsRequest Request = new();
}