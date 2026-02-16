using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetSelectPayment;

public class SetSelectPaymentHandler : IRequestHandler<SetSelectPaymentRequest>
{
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public SetSelectPaymentHandler(IPaymentRequestRepository paymentRequestRepository)
    {
        _paymentRequestRepository = paymentRequestRepository;
    }

    public ValueTask<Unit> Handle(SetSelectPaymentRequest request, CancellationToken cancellationToken)
    {
        _paymentRequestRepository.SetPaymentRequestId(request.Id);        
        return ValueTask.FromResult(Unit.Value);
    }
}
