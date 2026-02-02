using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.UnsafeCladding;

public class SetUnsafeCladdingCostsRequest : IRequest
{
    public decimal? UnsafeCladdingRemovalAmount { get; set; }
    public string UnsafeCladdingRemovalDescription { get; set; }
}