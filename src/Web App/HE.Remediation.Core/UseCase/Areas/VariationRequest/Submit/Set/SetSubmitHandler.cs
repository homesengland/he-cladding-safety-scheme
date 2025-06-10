using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.Communication;
using MediatR;
using System.Transactions;
using HE.Remediation.Core.Services.StatusTransition;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Submit.Set;

public class SetSubmitHandler : IRequestHandler<SetSubmitRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IVariationRequestRepository _variationRequestRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IDateRepository _dateRepository;
    private readonly ICommunicationService _communicationService;
    private readonly IStatusTransitionService _statusTransitionService;

    public SetSubmitHandler(
        IApplicationDataProvider applicationDataProvider,
        IVariationRequestRepository variationRequestRepository,
        ITaskRepository taskRepository,
        IDateRepository dateRepository,
        ICommunicationService communicationService, 
        IStatusTransitionService statusTransitionService)
    {
        _applicationDataProvider = applicationDataProvider;
        _variationRequestRepository = variationRequestRepository;
        _taskRepository = taskRepository;
        _dateRepository = dateRepository;
        _communicationService = communicationService;
        _statusTransitionService = statusTransitionService;
    }

    public async Task<Unit> Handle(SetSubmitRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var userId = _applicationDataProvider.GetUserId();
        await _variationRequestRepository.SubmitVariationRequest(userId);

        var applicationId = _applicationDataProvider.GetApplicationId();

        var taskType = await _taskRepository.GetTaskType(new GetTaskTypeParameters("Variations", "Variation Request"));

        var nextWorkingDay = await _dateRepository.AddWorkingDays(new AddWorkingDaysParameters { Date = DateTime.UtcNow, WorkingDays = 1 });

        await _taskRepository.InsertTask(new InsertTaskParameters
        {
            ReferenceId = applicationId,
            AssignedToTeamId = (int)ETeam.DaviesOps,
            AssignedToUserId = null,
            CreatedByUserId = null,
            Description = $"Please review the variation request and confirm whether to:{Environment.NewLine}•Approve the request{Environment.NewLine}•Approve reduced amount{Environment.NewLine}•Reject the request{Environment.NewLine}•Refer the request to Homes England",
            RequiredByDate = DateOnly.FromDateTime(nextWorkingDay),
            TaskStatus = ETaskStatus.NotStarted.ToString(),
            TaskTypeId = taskType.Id,
            TopicId = taskType.TopicId
        });

        await _statusTransitionService.TransitionToInternalStatus(EApplicationInternalStatus.VariationSubmitted, applicationIds: applicationId);

        await _communicationService.QueueEmailCommunication(new EmailCommunicationRequest
        (
            applicationId,
            EEmailType.VariationSubmitted
        ));

        scope.Complete();

        return Unit.Value;
    }
}