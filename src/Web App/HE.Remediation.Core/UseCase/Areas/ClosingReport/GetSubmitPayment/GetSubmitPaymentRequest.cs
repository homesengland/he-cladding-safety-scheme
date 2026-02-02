using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubmitPayment;

public class GetSubmitPaymentRequest : IRequest<GetSubmitPaymentResponse>
{
    private GetSubmitPaymentRequest()
    {
    }

    public static readonly GetSubmitPaymentRequest Request = new();
}
