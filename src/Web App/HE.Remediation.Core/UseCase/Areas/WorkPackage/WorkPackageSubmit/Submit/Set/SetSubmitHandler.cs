using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.Communication;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageSubmit.Submit.Set;

public class SetSubmitHandler : IRequestHandler<SetSubmitRequest, Unit>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository; 
    private readonly IWorkPackageRepository _workPackageRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly ICommunicationService _communicationService;

    public SetSubmitHandler(IApplicationDataProvider applicationDataProvider,
                            IApplicationRepository applicationRepository, 
                            IWorkPackageRepository workPackageRepository,
                            ITaskRepository taskRepository,
                            ICommunicationService communicationService)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
        _taskRepository = taskRepository;
        _communicationService = communicationService;
    }

    public async Task<Unit> Handle(SetSubmitRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _workPackageRepository.SubmitWorkPackage();

        var applicationId = _applicationDataProvider.GetApplicationId();

        await _applicationRepository.UpdateStatus(applicationId, EApplicationStatus.WorksPackageSubmitted);

        var taskType = await _taskRepository.GetTaskType(new GetTaskTypeParameters("Works Package Checks", "Review works package for recommendation"));

        var referenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);

        await _taskRepository.InsertTask(new InsertTaskParameters
        {
            ReferenceId = applicationId,
            AssignedToTeamId = (int)ETeam.DaviesOps,
            AssignedToUserId = null,
            CreatedByUserId = null,
            Description = $"Please undertake the following checks to allow you to recommend for approval",
            RequiredByDate = DateOnly.FromDateTime(DateTime.UtcNow),
            TaskStatus = ETaskStatus.NotStarted.ToString(),
            TaskTypeId = taskType.Id,
            TopicId = taskType.TopicId
        });

        await _communicationService.QueueEmailCommunication(new EmailCommunicationRequest
        (
            applicationId,
            EEmailType.WorksPackageSubmitted
        ));

        scope.Complete();

        return Unit.Value;
    }
}
