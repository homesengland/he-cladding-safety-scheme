using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetChangeProjectDates;

public class SetChangeProjectDatesHandler : IRequestHandler<SetChangeProjectDatesRequest, SetChangeProjectDatesResponse>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public SetChangeProjectDatesHandler(IApplicationDataProvider adp, 
                                        IPaymentRequestRepository paymentRequestRepository)
    {
        _adp = adp;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async ValueTask<SetChangeProjectDatesResponse> Handle(SetChangeProjectDatesRequest request, CancellationToken cancellationToken)
    {           
        var applicationId = _adp.GetApplicationId();
        var paymentRequestId = _adp.GetPaymentRequestId();

        KeyDatesParameters keyDatesParams = new KeyDatesParameters
        {            
            ExpectedDateForCompletion = GetDate(request?.ProjectDateEndMonth, request?.ProjectDateEndYear),
        };

        keyDatesParams.StartDate = request?.ExpectedStartDate;

        await _paymentRequestRepository.InsertWorkPackageKeyDetailsVersion(paymentRequestId, keyDatesParams);

        var claddingAlreadyRemoved = await _paymentRequestRepository.GetUnsafeCladdingAlreadyRemoved(applicationId, 
                                                                                                     paymentRequestId);                                
        return new SetChangeProjectDatesResponse
        {
            UnsafeCladdingAlreadyRemoved = claddingAlreadyRemoved
        };
    }

    private DateTime? GetDate(int? month, int? year)
    {
        return month is not null && year is not null
            ? new DateTime(year.Value, month.Value, 1)
            : null;
    }
}
