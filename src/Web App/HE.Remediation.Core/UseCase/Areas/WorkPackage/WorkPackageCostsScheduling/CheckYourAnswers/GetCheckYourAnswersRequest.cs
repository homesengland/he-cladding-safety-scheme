using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CheckYourAnswers
{
    public class GetCheckYourAnswersRequest: IRequest<GetCheckYourAnswersResponse>
    {
        public static readonly GetCheckYourAnswersRequest Request = new();
    }
}
