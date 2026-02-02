using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetPracticalCompletionDate;

public class GetPracticalCompletionDateRequest : IRequest<GetPracticalCompletionDateResponse>
{
    private GetPracticalCompletionDateRequest()
    {
    }

    public static readonly GetPracticalCompletionDateRequest Request = new();
}

