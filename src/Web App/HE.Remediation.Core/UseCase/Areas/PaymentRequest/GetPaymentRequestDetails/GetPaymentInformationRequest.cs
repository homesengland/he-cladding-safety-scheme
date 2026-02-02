
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetPaymentRequestDetails;

public class GetPaymentRequestDetailsRequest : IRequest<GetPaymentRequestDetailsResponse>
{
    private GetPaymentRequestDetailsRequest()
    {
    }

    public static readonly GetPaymentRequestDetailsRequest Request = new();
}
