
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.CheckYourAnswers.Set;

public class SetCheckYourAnswersRequest : IRequest<Unit>
{
    private SetCheckYourAnswersRequest()
    {
    }

    public static readonly SetCheckYourAnswersRequest Request = new();
}
