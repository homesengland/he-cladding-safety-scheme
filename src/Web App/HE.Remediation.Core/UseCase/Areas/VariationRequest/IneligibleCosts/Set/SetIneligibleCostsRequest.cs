using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.IneligibleCosts.Set
{
    public class SetIneligibleCostsRequest : IRequest
    {
        public ENoYes? HasVariationIneligibleCosts { get; set; }
    }
}
