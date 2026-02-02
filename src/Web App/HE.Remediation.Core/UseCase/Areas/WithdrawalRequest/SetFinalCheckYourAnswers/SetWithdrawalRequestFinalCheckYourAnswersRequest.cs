using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.SetFinalCheckYourAnswers
{
    public class SetWithdrawalRequestFinalCheckYourAnswersRequest : IRequest<Unit>
    {
        private SetWithdrawalRequestFinalCheckYourAnswersRequest()
        {
        }

        public static readonly SetWithdrawalRequestFinalCheckYourAnswersRequest Request = new();
    }
}
