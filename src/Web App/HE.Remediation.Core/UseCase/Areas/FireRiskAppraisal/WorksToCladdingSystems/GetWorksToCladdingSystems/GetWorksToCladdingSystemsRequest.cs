using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.GetWorksToCladdingSystems
{
    public class GetWorksToCladdingSystemsRequest : IRequest<IEnumerable<GetWorksToCladdingSystemsResponse>>
    {
        private GetWorksToCladdingSystemsRequest()
        {

        }

        public static readonly GetWorksToCladdingSystemsRequest Request = new();
    }
}
