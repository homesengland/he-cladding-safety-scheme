using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.IneligibleCost;

public class GetIneligibleCostRequest : IRequest<GetIneligibleCostResponse>
{
    private GetIneligibleCostRequest()
    {
    }

    public static GetIneligibleCostRequest Request => new();
}
