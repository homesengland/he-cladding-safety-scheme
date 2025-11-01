using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;

using MediatR;

using Microsoft.Extensions.Options;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetForecastGateway3Submission;

public class SetForecastGateway3SubmissionHandler : IRequestHandler<SetForecastGateway3SubmissionRequest>
{
    private readonly IPaymentRequestRepository _paymentRequestRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetForecastGateway3SubmissionHandler(IApplicationDataProvider applicationDataProvider,
                          IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async Task<Unit> Handle(SetForecastGateway3SubmissionRequest request, CancellationToken cancellationToken)
    {
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        DateTime? expectedSubmissionDate = null;

        if (request.ExpectedSubmissionDateMonth.HasValue || request.ExpectedSubmissionDateYear.HasValue)
        {
            expectedSubmissionDate = GetDate(request.ExpectedSubmissionDateMonth, request.ExpectedSubmissionDateYear);
        }

        await _paymentRequestRepository.UpdateExpectedSubmissionDateForGateway3Application(paymentRequestId, expectedSubmissionDate);

        return Unit.Value;
    }

    private DateTime? GetDate(int? month, int? year)
    {
        return month is not null && year is not null
            ? new DateTime(year.Value, month.Value, 1)
            : null;
    }
}