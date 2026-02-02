using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.CheckYourAnswers.Get;

public class GetCheckYourAnswersRequest : IRequest<GetCheckYourAnswersResponse>
{
    private GetCheckYourAnswersRequest()
    {
    }

    public static GetCheckYourAnswersRequest Request => new();
}
