
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageKeyDates.CheckYourAnswers.Get;

public class GetCheckYourAnswersRequest : IRequest<GetCheckYourAnswersResponse>
{
    private GetCheckYourAnswersRequest()
    {
    }

    public static readonly GetCheckYourAnswersRequest Request = new();
}
