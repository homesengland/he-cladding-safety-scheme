
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageKeyDates.CheckYourAnswers.Set;

public class SetCheckYourAnswersRequest : IRequest<Unit>
{
    private SetCheckYourAnswersRequest()
    {
    }

    public static readonly SetCheckYourAnswersRequest Request = new();
}
