using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetCheckYourAnswers;

public class GetCheckYourAnswersRequest : IRequest<GetCheckYourAnswersResponse>
{
    private GetCheckYourAnswersRequest()
    {
    }

    public static readonly GetCheckYourAnswersRequest Request = new();
}
