using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.Communication;
using MediatR;
using System.Transactions;
using HE.Remediation.Core.Services.StatusTransition;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageSubmit.Submit.Set;

public class SetSubmitHandler : IRequestHandler<SetSubmitRequest, Unit>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository; 
    private readonly IWorkPackageRepository _workPackageRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly ICommunicationService _communicationService;
    private readonly IStatusTransitionService _statusTransitionService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IDateRepository _dateRepository;

    public SetSubmitHandler(
        IApplicationDataProvider applicationDataProvider,
        IApplicationRepository applicationRepository, 
        IWorkPackageRepository workPackageRepository,
        ITaskRepository taskRepository,
        ICommunicationService communicationService, 
        IStatusTransitionService statusTransitionService, 
        IDateTimeProvider dateTimeProvider, 
        IDateRepository dateRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
        _taskRepository = taskRepository;
        _communicationService = communicationService;
        _statusTransitionService = statusTransitionService;
        _dateTimeProvider = dateTimeProvider;
        _dateRepository = dateRepository;
    }

    public async Task<Unit> Handle(SetSubmitRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var userId = _applicationDataProvider.GetUserId();

        var taskType = await _taskRepository.GetTaskType(new GetTaskTypeParameters("Works Package Checks", "Review works package for recommendation"));
        
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _workPackageRepository.SubmitWorkPackage(userId);
        
        await _statusTransitionService.TransitionToStatus(EApplicationStatus.WorksPackageSubmitted, applicationIds: applicationId);

        await _taskRepository.InsertTask(new InsertTaskParameters
        {
            ReferenceId = applicationId,
            AssignedToTeamId = (int)ETeam.DaviesOps,
            AssignedToUserId = null,
            CreatedByUserId = null,
            Description = "Please undertake the following checks to allow you to recommend for approval",
            RequiredByDate = DateOnly.FromDateTime(_dateTimeProvider.UtcNow),
            TaskStatus = ETaskStatus.NotStarted.ToString(),
            TaskTypeId = taskType.Id,
            TopicId = taskType.TopicId
        });

        await TryCreateConsiderateConstructorsSchemeTask(applicationId);

        await TryCreateProjectPlanTask(applicationId);

        await TryCreateTeamMemberCladdingSystemInstallationTask(applicationId);

        await _communicationService.QueueEmailCommunication(new EmailCommunicationRequest
        (
            applicationId,
            EEmailType.WorksPackageSubmitted
        ));

        scope.Complete();

        return Unit.Value;
    }

    private async Task TryCreateConsiderateConstructorsSchemeTask(Guid applicationId)
    {
        if (await _workPackageRepository.IsSignedUpToConsiderateConstructorsScheme(applicationId))
        {
            return;
        }

        var taskType = await _taskRepository.GetTaskType(new GetTaskTypeParameters("Works Package submission", "Additional information required"));

        var dueDate = await _dateRepository.AddWorkingDays(new AddWorkingDaysParameters
        {
            Date = _dateTimeProvider.UtcNow,
            WorkingDays = 1
        });

        await _taskRepository.InsertTask(new InsertTaskParameters
        {
            Description = "Please contact the applicant to discuss the lead contractor not being signed up to the considerate constructors scheme",
            TaskTypeId = taskType.Id,
            AssignedToTeamId = (int)ETeam.DaviesOps,
            AssignedToUserId = null,
            RequiredByDate = DateOnly.FromDateTime(dueDate),
            CreatedByUserId = null,
            Notes = null,
            ReferenceId = applicationId,
            TaskStatus = ETaskStatus.NotStarted.ToString(),
            TopicId = null
        });
    }
    private async Task TryCreateProjectPlanTask(Guid applicationId)
    {
        var hasAgreedProjectPlan = await _workPackageRepository.GetHasProgrammePlan(applicationId);

        if (hasAgreedProjectPlan != null && hasAgreedProjectPlan.Value)
        {
            return;
        }

        var taskType = await _taskRepository.GetTaskType(new GetTaskTypeParameters("Works Package submission", "Additional information required"));

        var dueDate = await _dateRepository.AddWorkingDays(new AddWorkingDaysParameters
        {
            Date = _dateTimeProvider.UtcNow,
            WorkingDays = 1
        });

        await _taskRepository.InsertTask(new InsertTaskParameters
        {
            Description = "Contact the applicant to request further information about Project plan",
            TaskTypeId = taskType.Id,
            AssignedToTeamId = (int)ETeam.DaviesOps,
            AssignedToUserId = null,
            RequiredByDate = DateOnly.FromDateTime(dueDate),
            CreatedByUserId = null,
            Notes = null,
            ReferenceId = applicationId,
            TaskStatus = ETaskStatus.NotStarted.ToString(),
            TopicId = null
        });
    }

    private async Task TryCreateTeamMemberCladdingSystemInstallationTask(Guid applicationId)
    {
        var teamMembers = await _workPackageRepository.GetTeamMembers();

        if (!teamMembers.Any(t => t.InvolvedInOriginalInstallation == true))
        {
            return;
        }

        var taskType = await _taskRepository.GetTaskType(new GetTaskTypeParameters("Works Package submission", "Additional information required"));

        var dueDate = await _dateRepository.AddWorkingDays(new AddWorkingDaysParameters
        {
            Date = _dateTimeProvider.UtcNow,
            WorkingDays = 1
        });

        await _taskRepository.InsertTask(new InsertTaskParameters
        {
            Description = "Please Contact the applicant to request further information about the project team member's involvement in the original cladding installation.",
            TaskTypeId = taskType.Id,
            AssignedToTeamId = (int)ETeam.DaviesOps,
            AssignedToUserId = null,
            RequiredByDate = DateOnly.FromDateTime(dueDate),
            CreatedByUserId = null,
            Notes = null,
            ReferenceId = applicationId,
            TaskStatus = ETaskStatus.NotStarted.ToString(),
            TopicId = null
        });
    }
}
