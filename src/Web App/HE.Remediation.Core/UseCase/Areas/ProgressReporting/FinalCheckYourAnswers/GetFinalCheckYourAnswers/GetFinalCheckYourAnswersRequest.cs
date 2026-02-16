
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.FinalCheckYourAnswers.GetFinalCheckYourAnswers;

public class GetFinalCheckYourAnswersRequest : IRequest<GetFinalCheckYourAnswersResponse>
{
    private GetFinalCheckYourAnswersRequest()
    {
    }

    public static readonly GetFinalCheckYourAnswersRequest Request = new();
}
