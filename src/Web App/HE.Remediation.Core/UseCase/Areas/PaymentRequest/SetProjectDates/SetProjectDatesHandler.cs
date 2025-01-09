using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetProjectDates;

public class SetProjectDatesHandler : IRequestHandler<SetProjectDatesRequest, SetProjectDatesResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public SetProjectDatesHandler(IApplicationDataProvider applicationDataProvider, 
                                  IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async Task<SetProjectDatesResponse> Handle(SetProjectDatesRequest request, CancellationToken cancellationToken)
    {   
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        await _paymentRequestRepository.UpdatePaymentRequestProjectDateChanged(paymentRequestId, 
                                                                               request?.ProjectDatesChanged);

        var claddingAlreadyRemoved = await _paymentRequestRepository.GetUnsafeCladdingAlreadyRemoved(applicationId, 
                                                                                                     paymentRequestId);                
        return new SetProjectDatesResponse
        {
            UnsafeCladdingAlreadyRemoved = claddingAlreadyRemoved
        };
    }    
}
