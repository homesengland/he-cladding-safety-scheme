﻿using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.Communication;
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

    public GetSubmittedHandler(IApplicationDataProvider applicationDataProvider,
                               IApplicationRepository applicationRepository,
                               IPaymentRequestRepository paymentRequestRepository,
                               ITaskRepository taskRepository,
                               IDateRepository dateRepository,
                               ICommunicationService communicationService)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _paymentRequestRepository = paymentRequestRepository;
        _taskRepository = taskRepository;
        _dateRepository = dateRepository;
        _communicationService = communicationService;
    }

    public async Task<GetSubmittedResponse> Handle(GetSubmittedRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        await _paymentRequestRepository.SubmitPaymentRequest(paymentRequestId);

        await _applicationRepository.UpdateInternalStatus(applicationId, EApplicationInternalStatus.PaymentRequestSubmitted);

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

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);

        return new GetSubmittedResponse
        {
            ApplicationReferenceNumber = applicationReferenceNumber
        };
    }
}
