using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.GetFinalCheckYourAnswers
{
    public class GetFinalCheckYourAnswersRequest : IRequest<GetFinalCheckYourAnswersResponse>
    {
        private GetFinalCheckYourAnswersRequest()
        {
        }

        public static readonly GetFinalCheckYourAnswersRequest Request = new();
    }
}
