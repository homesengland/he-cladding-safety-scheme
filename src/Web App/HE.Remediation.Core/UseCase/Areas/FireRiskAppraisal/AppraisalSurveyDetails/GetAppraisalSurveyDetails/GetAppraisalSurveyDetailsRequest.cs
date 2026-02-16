using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AppraisalSurveyDetails.GetAppraisalSurveyDetails
{
    public class GetAppraisalSurveyDetailsRequest : IRequest<GetAppraisalSurveyDetailsResponse>
    {
        private GetAppraisalSurveyDetailsRequest()
        {

        }

        public static readonly GetAppraisalSurveyDetailsRequest Request = new();
    }
}
