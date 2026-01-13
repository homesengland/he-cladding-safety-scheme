using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.Communication;
using HE.Remediation.Core.Services.StatusTransition;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubmitted;

public class GetSubmittedHandler : IRequestHandler<GetSubmittedRequest, GetSubmittedResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IDateRepository _dateRepository;
    private readonly ICommunicationService _communicationService;
    private readonly IStatusTransitionService _statusTransitionService;

    public GetSubmittedHandler(
        IApplicationDataProvider applicationDataProvider,
        IApplicationRepository applicationRepository,
        IPaymentRequestRepository paymentRequestRepository,
        ITaskRepository taskRepository,
        IDateRepository dateRepository,
        ICommunicationService communicationService, 
        IStatusTransitionService statusTransitionService)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _paymentRequestRepository = paymentRequestRepository;
        _taskRepository = taskRepository;
        _dateRepository = dateRepository;
        _communicationService = communicationService;
        _statusTransitionService = statusTransitionService;
    }

    public async Task<GetSubmittedResponse> Handle(GetSubmittedRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();
        var userId = _applicationDataProvider.GetUserId();

        var paymentRequest = await _paymentRequestRepository.GetPaymentRequestDetails(applicationId, paymentRequestId);

        if (!paymentRequest.IsSubmitted)
        {
            await _paymentRequestRepository.SubmitPaymentRequest(paymentRequestId, userId);

            await _statusTransitionService.TransitionToInternalStatus(EApplicationInternalStatus.PaymentRequestSubmitted, applicationIds: applicationId);

            var taskType = await _taskRepository.GetTaskType(new GetTaskTypeParameters("Payment Request Checks", "Review Submitted Payment Request"));
            var dueDate = await _dateRepository.AddWorkingDays(new AddWorkingDaysParameters
            {
                Date = DateTime.UtcNow.Date,
                WorkingDays = 1
            });

            await _taskRepository.InsertTask(new InsertTaskParameters
            {
                ReferenceId = applicationId,
                AssignedToTeamId = (int)ETeam.DaviesOps,
                AssignedToUserId = null,
                CreatedByUserId = null,
                Description = $"Please review the payment request and provide a recommendation to HE whether to:{Environment.NewLine}•Approve the request{Environment.NewLine}•Reject the request{Environment.NewLine}•Approve reduced amount",
                RequiredByDate = DateOnly.FromDateTime(dueDate),
                TaskStatus = ETaskStatus.NotStarted.ToString(),
                TopicId = taskType.TopicId,
                TaskTypeId = taskType.Id
            });

            await _paymentRequestRepository.UpdatePaymentRequestTaskStatus(paymentRequestId, EPaymentRequestTaskStatus.Submitted);

            await _communicationService.QueueEmailCommunication(new EmailCommunicationRequest
            (
                applicationId,
                EEmailType.PaymentRequestSubmitted
            ));
        }

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);

        return new GetSubmittedResponse
        {
            ApplicationReferenceNumber = applicationReferenceNumber
        };
    }
}
