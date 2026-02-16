
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.FinalCheckYourAnswers.SetFinalCheckYourAnswers;

public class SetFinalCheckYourAnswersRequest : IRequest<Unit>
{
    private SetFinalCheckYourAnswersRequest()
    {
    }

    public static readonly SetFinalCheckYourAnswersRequest Request = new();
}
