
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetPaymentInformation;

public class GetPaymentInformationRequest : IRequest<GetPaymentInformationResponse>
{
    private GetPaymentInformationRequest()
    {
    }

    public static readonly GetPaymentInformationRequest Request = new();
}
