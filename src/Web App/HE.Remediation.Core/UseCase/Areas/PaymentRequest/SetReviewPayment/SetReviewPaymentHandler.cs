using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetReviewPayment;

public class SetReviewPaymentHandler : IRequestHandler<SetReviewPaymentRequest>
{ 
    private readonly IApplicationDataProvider _adp;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public SetReviewPaymentHandler(IApplicationDataProvider adp, 
                                  IPaymentRequestRepository paymentRequestRepository)
    {
        _adp = adp;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async ValueTask<Unit> Handle(SetReviewPaymentRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _adp.GetApplicationId();
        var paymentRequestId = _adp.GetPaymentRequestId();

        if (request.ChangeToMonthlyCost)
        {
            if (request?.ReasonForChange != null)
            {
                await _paymentRequestRepository.UpdatePaymentRequestReasonForChange(paymentRequestId,
                                                                                    request?.ReasonForChange);
            }
        }
                
        return Unit.Value;
    }
}
