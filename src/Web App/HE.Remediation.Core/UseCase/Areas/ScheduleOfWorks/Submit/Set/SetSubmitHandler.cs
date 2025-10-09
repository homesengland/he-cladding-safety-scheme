using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.Communication;
using HE.Remediation.Core.Services.StatusTransition;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Submit.Set;

public class SetSubmitHandler : IRequestHandler<SetSubmitRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly ICommunicationService _communicationService;
    private readonly IStatusTransitionService _statusTransitionService;

    public SetSubmitHandler(
        IApplicationDataProvider applicationDataProvider,
        IApplicationRepository applicationRepository,
        IScheduleOfWorksRepository scheduleOfWorksRepository,
        ITaskRepository taskRepository,
        ICommunicationService communicationService, 
        IStatusTransitionService statusTransitionService)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
        _taskRepository = taskRepository;
        _communicationService = communicationService;
        _statusTransitionService = statusTransitionService;
    }

    public async Task<Unit> Handle(SetSubmitRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var userId = _applicationDataProvider.GetUserId();
        await _scheduleOfWorksRepository.SubmitScheduleOfWorks(userId);

        var applicationId = _applicationDataProvider.GetApplicationId();
        await _statusTransitionService.TransitionToStatus(EApplicationStatus.ScheduleOfWorksSubmitted, applicationIds: applicationId);

        var taskType = await _taskRepository.GetTaskType(new GetTaskTypeParameters("Schedule of works checks", "Review schedule of works submission"));

        var referenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);

        await _taskRepository.InsertTask(new InsertTaskParameters
        {
            ReferenceId = applicationId,
            AssignedToTeamId = (int)ETeam.DaviesOps,
            AssignedToUserId = null,
            CreatedByUserId = null,
            Description = "Please review the schedule of works submitted and complete the required checks",
            RequiredByDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)),
            TaskStatus = ETaskStatus.NotStarted.ToString(),
            TaskTypeId = taskType.Id
        });

        await _communicationService.QueueEmailCommunication(new EmailCommunicationRequest
        (
            applicationId,
            EEmailType.ScheduleOfWorksSubmitted
        ));

        scope.Complete();

        return Unit.Value;
    }
}