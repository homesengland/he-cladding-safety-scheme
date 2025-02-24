using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.CheckYourAnswers.SetCheckYourAnswers
{
    public class SetCheckYourAnswersRequest : IRequest<Unit>
    {
        private SetCheckYourAnswersRequest()
        {
        }

        public static readonly SetCheckYourAnswersRequest Request = new();
    }
}
