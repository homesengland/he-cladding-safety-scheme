using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.CheckYourAnswers.GetCheckYourAnswers;

public class GetCheckYourAnswersRequest : IRequest<GetCheckYourAnswersResponse>
{
    private GetCheckYourAnswersRequest()
    {
    }

    public static readonly GetCheckYourAnswersRequest Request = new();
}