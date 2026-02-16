using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetCladdingRemoved;

public class SetCladdingRemovedHandler : IRequestHandler<SetCladdingRemovedRequest>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public SetCladdingRemovedHandler(IApplicationDataProvider adp, 
                                     IPaymentRequestRepository paymentRequestRepository)
    {
        _adp = adp;
        _paymentRequestRepository = paymentRequestRepository;
    }
    
    public async ValueTask<Unit> Handle(SetCladdingRemovedRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _adp.GetApplicationId();
        var paymentRequestId = _adp.GetPaymentRequestId();

        await _paymentRequestRepository.UpdatePaymentRequestUnsafeCladdingRemoved(paymentRequestId, 
                                                                                  request?.UnsafeCladdingRemoved);
        
        return Unit.Value;
    }    
}
