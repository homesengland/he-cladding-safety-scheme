using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.CheckYourAnswers.SetCheckYourAnswers
{
    public class SetCheckYourAnswersRequest : IRequest<Unit>
    {
        private SetCheckYourAnswersRequest()
        {
        }

        public static readonly SetCheckYourAnswersRequest Request = new();
    }
}
