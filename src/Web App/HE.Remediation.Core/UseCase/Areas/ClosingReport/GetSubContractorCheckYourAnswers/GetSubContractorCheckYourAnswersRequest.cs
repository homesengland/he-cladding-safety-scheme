using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubContractorCheckYourAnswers;

public class GetSubContractorCheckYourAnswersRequest : IRequest<GetSubContractorCheckYourAnswersResponse>
{
    
    private GetSubContractorCheckYourAnswersRequest()
    {
    }

    public static readonly GetSubContractorCheckYourAnswersRequest Request = new();
}
