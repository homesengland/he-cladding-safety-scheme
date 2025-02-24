using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.CladdingArea
{
    public class GetTotalCladdingAreaRequest : IRequest<GetTotalCladdingAreaResponse>
    {
        public GetTotalCladdingAreaRequest()
        { }
    }
}
