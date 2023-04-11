using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.SetCheckYourAnswers
{
    public class SetCheckYourAnswersRequest : IRequest<Unit>
    {
        private SetCheckYourAnswersRequest()
        {
        }

        public static readonly SetCheckYourAnswersRequest Request = new();
    }
}
