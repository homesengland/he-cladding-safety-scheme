using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetReviewPayment;

public class GetReviewPaymentRequest : IRequest<GetReviewPaymentResponse>
{
    private GetReviewPaymentRequest()
    {
    }

    public static readonly GetReviewPaymentRequest Request = new();

}
