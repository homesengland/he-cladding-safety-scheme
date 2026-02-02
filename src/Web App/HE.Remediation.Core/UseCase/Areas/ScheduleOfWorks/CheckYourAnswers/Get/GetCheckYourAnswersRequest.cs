using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.CheckYourAnswers.Get;

public class GetCheckYourAnswersRequest : IRequest<GetCheckYourAnswersResponse>
{
    private GetCheckYourAnswersRequest()
    {
    }

    public static GetCheckYourAnswersRequest Request => new();
}
