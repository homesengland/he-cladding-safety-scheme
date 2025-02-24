using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.Start;

public class CheckStatusHandler : IRequestHandler<CheckStatusRequest, CheckStatusResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public CheckStatusHandler(IApplicationDataProvider applicationDataProvider, 
                                       IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async Task<CheckStatusResponse> Handle(CheckStatusRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var isSubmitted = await _paymentRequestRepository.IsPaymentRequestSubmitted(request.PaymentRequestId);
        var isExpired = await _paymentRequestRepository.IsPaymentRequestExpired(request.PaymentRequestId);

        return new CheckStatusResponse
        {
            IsSubmitted = isSubmitted,
            IsExpired = isExpired
        };
    }
}
