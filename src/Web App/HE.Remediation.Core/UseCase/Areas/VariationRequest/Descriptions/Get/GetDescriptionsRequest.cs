using HE.Remediation.Core.UseCase.Areas.VariationRequest.Descriptions.Get;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Descriptions.Get
{
    public class GetDescriptionsRequest : IRequest<GetDescriptionsResponse>
    {
        private GetDescriptionsRequest()
        {
        }

        public static GetDescriptionsRequest Request => new();
    }
}