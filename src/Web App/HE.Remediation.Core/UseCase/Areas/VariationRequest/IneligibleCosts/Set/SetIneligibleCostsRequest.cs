using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.IneligibleCosts.Set
{
    public class SetIneligibleCostsRequest : IRequest
    {
        public ENoYes? HasVariationIneligibleCosts { get; set; }
    }
}
