using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.WorkPackageSignatories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageSignatories.ConfirmSignatories.Set;

public class SetConfirmSignatoriesHandler : IRequestHandler<SetConfirmSignatoriesRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly ITaskRepository _taskRepository;

    public SetConfirmSignatoriesHandler(
        IWorkPackageRepository workPackageRepository,
        ITaskRepository taskRepository,
        IApplicationDataProvider applicationDataProvider)
    {
        _workPackageRepository = workPackageRepository;
        _taskRepository = taskRepository;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<Unit> Handle(SetConfirmSignatoriesRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var requireSignatories = await EnsureSignatories(request);

        if (request.AreSignatoriesCorrect is false && (requireSignatories?.ContactTaskRaised == null || requireSignatories?.ContactTaskRaised is false))
        {
            await AddTask();
        }

        scope.Complete();

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
            Description = "Please contact the applicant to confirm signatory details",
            RequiredByDate = DateOnly.FromDateTime(DateTime.Today),
            TaskStatus = ETaskStatus.NotStarted.ToString(),
            TaskTypeId = taskType.Id,
            Notes = null
        });

        await _workPackageRepository.UpdateSignatoriesContactTask(true);
    }

    private async Task<SignatoriesResult> EnsureSignatories(SetConfirmSignatoriesRequest request)
    {
        var existingSignatories = await _workPackageRepository.GetSignatories();
        var signatoriesStatus = await _workPackageRepository.GetSignatoriesStatus();

        if (signatoriesStatus is null)
        {
            await _workPackageRepository.InsertSignatories(request.AreSignatoriesCorrect);
        }
        else
        {
            await _workPackageRepository.UpdateSignatories(request.AreSignatoriesCorrect);
        }

        await _workPackageRepository.UpdateSignatoriesStatus(ETaskStatus.Completed);

        return existingSignatories;
    }
}