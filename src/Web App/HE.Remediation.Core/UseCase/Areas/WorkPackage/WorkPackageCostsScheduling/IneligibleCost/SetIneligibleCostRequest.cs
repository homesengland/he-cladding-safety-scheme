using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.IneligibleCost;

public class SetIneligibleCostRequest : IRequest
{
    public ENoYes? IneligibleCosts { get; set; }
}
