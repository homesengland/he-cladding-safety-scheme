using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.CompletedAppraisal.GetCompletedAppraisal
{
    public class GetCompletedAppraisalRequest : IRequest<GetCompletedAppraisalResponse>
    {
        private GetCompletedAppraisalRequest()
        {

        }

        public static readonly GetCompletedAppraisalRequest Request = new();
    }
}
