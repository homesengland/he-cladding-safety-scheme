using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetReviewPayment;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubmitPayment;

public class GetSubmitPaymentRequest : IRequest<GetSubmitPaymentResponse>
{
    private GetSubmitPaymentRequest()
    {
    }

    public static readonly GetSubmitPaymentRequest Request = new();
}
