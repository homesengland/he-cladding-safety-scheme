using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;

using MediatR;

using Microsoft.Extensions.Options;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetPracticalCompletionDate;

public class SetPracticalCompletionDateHandler : IRequestHandler<SetPracticalCompletionDateRequest>
{
    private readonly IPaymentRequestRepository _paymentRequestRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetPracticalCompletionDateHandler(IApplicationDataProvider applicationDataProvider,
                          IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async Task<Unit> Handle(SetPracticalCompletionDateRequest request, CancellationToken cancellationToken)
    {
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        DateTime? expectedSubmissionDate = null;

        if (request.ExpectedPracticalDateMonth.HasValue || request.ExpectedPracticalDateYear.HasValue)
        {
            expectedSubmissionDate = GetDate(request.ExpectedPracticalDateMonth, request.ExpectedPracticalDateYear);
        }

        await _paymentRequestRepository.UpdatePracticalCompletionDate(paymentRequestId, expectedSubmissionDate);

        return Unit.Value;
    }

    private DateTime? GetDate(int? month, int? year)
    {
        return month is not null && year is not null
            ? new DateTime(year.Value, month.Value, 1)
            : null;
    }
}