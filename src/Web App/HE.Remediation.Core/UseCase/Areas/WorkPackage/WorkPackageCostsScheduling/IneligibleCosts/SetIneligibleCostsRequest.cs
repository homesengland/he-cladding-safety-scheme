using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.IneligibleCosts;

public class SetIneligibleCostsRequest : IRequest
{
    public decimal? IneligibleAmount { get; set; }
    public string IneligibleDescription { get; set; }
}