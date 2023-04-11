using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.GetCheckYourAnswers;

public class GetCheckYourAnswersRequest : IRequest<GetCheckYourAnswersResponse>
{
    private GetCheckYourAnswersRequest()
    {
    }

    public static readonly GetCheckYourAnswersRequest Request = new();
}