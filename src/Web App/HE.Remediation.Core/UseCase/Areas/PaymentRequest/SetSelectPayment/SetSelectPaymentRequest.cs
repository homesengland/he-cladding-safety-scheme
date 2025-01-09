using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetSelectPayment;

public class SetSelectPaymentRequest : IRequest
{
    public Guid? Id { get; set; }
}
