using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.SurveyInstructionDetails.GetSurveyInstructionDetails
{
    public class GetSurveyInstructionDetailsRequest : IRequest<GetSurveyInstructionDetailsResponse>
    {
        private GetSurveyInstructionDetailsRequest()
        {

        }

        public static readonly GetSurveyInstructionDetailsRequest Request = new();
    }
}
