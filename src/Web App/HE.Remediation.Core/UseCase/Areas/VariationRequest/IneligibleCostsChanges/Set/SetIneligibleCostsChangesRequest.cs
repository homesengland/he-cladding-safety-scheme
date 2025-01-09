using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.IneligibleCostsChanges.Set;

public class SetIneligibleCostsChangesRequest : IRequest
{
    public decimal? VariationIneligibleAmount { get; set; }
    public string VariationIneligibleDescription { get; set; }
}
