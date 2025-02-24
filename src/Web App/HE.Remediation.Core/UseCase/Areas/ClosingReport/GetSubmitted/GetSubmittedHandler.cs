using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubmitted;

public class GetSubmittedHandler : IRequestHandler<GetSubmittedRequest, GetSubmittedResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IDateRepository _dateRepository;

    public GetSubmittedHandler(IApplicationDataProvider applicationDataProvider,
                               IApplicationRepository applicationRepository,
                               IPaymentRequestRepository paymentRequestRepository,
                               ITaskRepository taskRepository,
                               IDateRepository dateRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _paymentRequestRepository = paymentRequestRepository;
        _taskRepository = taskRepository;
        _dateRepository = dateRepository;
    }

    public async Task<GetSubmittedResponse> Handle(GetSubmittedRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);

        return new GetSubmittedResponse
        {
            ApplicationReferenceNumber = applicationReferenceNumber
        };
    }
}
