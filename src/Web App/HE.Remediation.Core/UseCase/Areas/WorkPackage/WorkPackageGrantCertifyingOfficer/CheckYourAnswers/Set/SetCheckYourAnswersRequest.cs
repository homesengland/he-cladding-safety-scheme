using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.CheckYourAnswers.Set;

public class SetCheckYourAnswersRequest : IRequest<Unit>
{
    private SetCheckYourAnswersRequest()
    {
    }

    public static readonly SetCheckYourAnswersRequest Request = new();
}
