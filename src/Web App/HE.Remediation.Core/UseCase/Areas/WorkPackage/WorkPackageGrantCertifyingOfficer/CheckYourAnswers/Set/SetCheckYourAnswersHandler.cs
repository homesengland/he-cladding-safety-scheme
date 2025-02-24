using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.CheckYourAnswers.Set;

public class SetCheckYourAnswersHandler : IRequestHandler<SetCheckYourAnswersRequest, Unit>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IWorkPackageRepository _workPackageRepository;
    private readonly ITaskRepository _taskRepository;

    public SetCheckYourAnswersHandler(
        IApplicationDataProvider applicationDataProvider,
        IWorkPackageRepository workPackageRepository,
        ITaskRepository taskRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _workPackageRepository = workPackageRepository;
        _taskRepository = taskRepository;
    }

    public async Task<Unit> Handle(SetCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var dutyOfCareDeedTaskRaised = await _workPackageRepository.GetGrantCertifyingOfficerDutyOfCareDeedTaskRaised();
        if (!dutyOfCareDeedTaskRaised)
        {
            await AddTask();
        }

        await UpdateTaskStatus();
        await UpdateDutyOfCareDeedTaskStatus();

        return Unit.Value;
    }

    private async Task AddTask()
    {
        var taskType = await _taskRepository.GetTaskType(new GetTaskTypeParameters(
            "Works Package submission",
            "Additional information required"));

        await _taskRepository.InsertTask(new InsertTaskParameters
        {
            ReferenceId = _applicationDataProvider.GetApplicationId(),
            AssignedToTeamId = (int)ETeam.DaviesOps,
            Description = "Ensure Duty of Care deed has been sent to Grant Certifying Officer",
            RequiredByDate = DateOnly.FromDateTime(DateTime.Today),
            TaskStatus = ETaskStatus.NotStarted.ToString(),
            TaskTypeId = taskType.Id,
            Notes = null
        });

        await _workPackageRepository.UpdateGrantCertifyingOfficerDutyOfCareDeedTask(true);
    }

    private async Task UpdateTaskStatus()
    {
        var taskStatus = await _workPackageRepository.GetGrantCertifyingOfficerStatus();
        if (taskStatus != ETaskStatus.Completed)
        {
            await _workPackageRepository.UpdateGrantCertifyingOfficerStatus(ETaskStatus.Completed);
        }
    }

    private async Task UpdateDutyOfCareDeedTaskStatus()
    {
        var dutyOfCareDeed = await _workPackageRepository.GetDutyOfCareDeed();
        if (dutyOfCareDeed is null)
        {
            await _workPackageRepository.InsertDutyOfCareDeed();
        }

        var dutyOfCareDeedTaskStatus = await _workPackageRepository.GetDutyOfCareDeedStatus();
        if (dutyOfCareDeedTaskStatus != ETaskStatus.InProgress &&
            dutyOfCareDeedTaskStatus != ETaskStatus.Completed)
        {
            await _workPackageRepository.UpdateDutyOfCareDeedStatus(ETaskStatus.InProgress);
        }
    }
}
