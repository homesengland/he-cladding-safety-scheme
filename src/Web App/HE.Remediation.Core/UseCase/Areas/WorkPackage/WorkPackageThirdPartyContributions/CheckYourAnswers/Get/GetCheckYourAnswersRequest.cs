using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.CheckYourAnswers.Get
{
    public class GetCheckYourAnswersRequest : IRequest<GetCheckYourAnswersResponse>
    {
        private GetCheckYourAnswersRequest()
        {
        }

        public static readonly GetCheckYourAnswersRequest Request = new();
    }
}
