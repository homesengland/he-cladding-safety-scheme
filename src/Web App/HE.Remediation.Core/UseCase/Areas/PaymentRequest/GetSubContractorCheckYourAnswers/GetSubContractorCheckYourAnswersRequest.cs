using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubContractorCheckYourAnswers;

public class GetSubContractorCheckYourAnswersRequest : IRequest<GetSubContractorCheckYourAnswersResponse>
{
    
    private GetSubContractorCheckYourAnswersRequest()
    {
    }

    public static readonly GetSubContractorCheckYourAnswersRequest Request = new();
}
