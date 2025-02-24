using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.UnsafeCladdingCosts.Set
{
    public class SetUnsafeCladdingCostsRequest : IRequest
    {
        public decimal VariationRemovalOfCladdingAmount { get; set; }
        public string VariationRemovalOfCladdingDescription { get; set; }
    }
}
