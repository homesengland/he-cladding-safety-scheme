using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.TotalCosts;

public class SetTotalCostRequest : IRequest
{
    public static readonly SetTotalCostRequest Request = new();
}
