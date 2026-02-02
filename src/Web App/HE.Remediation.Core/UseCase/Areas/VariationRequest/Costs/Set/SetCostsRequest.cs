using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Costs.Set
{
    public class SetCostsRequest : IRequest<SetCostsResponse>
    {
        private SetCostsRequest()
        {
        }

        public static SetCostsRequest Request => new();
    }
}