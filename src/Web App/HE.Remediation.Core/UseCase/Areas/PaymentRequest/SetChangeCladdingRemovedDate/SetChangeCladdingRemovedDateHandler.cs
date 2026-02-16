using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetChangeCladdingRemovedDate;

public class SetChangeCladdingRemovedDateHandler : IRequestHandler<SetChangeCladdingRemovedDateRequest>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public SetChangeCladdingRemovedDateHandler(IApplicationDataProvider adp, 
                                               IPaymentRequestRepository paymentRequestRepository)
    {
        _adp = adp;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async ValueTask<Unit> Handle(SetChangeCladdingRemovedDateRequest request, CancellationToken cancellationToken)
    {           
        var applicationId = _adp.GetApplicationId();
        var paymentRequestId = _adp.GetPaymentRequestId();
        
        await _paymentRequestRepository.UpdatePaymentRequestUnsafeCladdingRemovedDate(paymentRequestId, 
                                                                                      GetDate(request?.DateRemovedMonth, request?.DateRemovedYear));
        
        return Unit.Value;
    }

    private DateTime? GetDate(int? month, int? year)
    {
        return month is not null && year is not null
            ? new DateTime(year.Value, month.Value, 1)
            : null;
    }
}
