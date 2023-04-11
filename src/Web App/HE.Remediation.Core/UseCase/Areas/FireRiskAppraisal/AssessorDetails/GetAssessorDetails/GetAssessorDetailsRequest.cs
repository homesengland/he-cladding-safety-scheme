using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AssessorDetails.GetAssessorDetails
{
    public class GetAssessorDetailsRequest : IRequest<GetAssessorDetailsResponse>
    {
        private GetAssessorDetailsRequest()
        {

        }

        public static readonly GetAssessorDetailsRequest Request = new();
    }
}
