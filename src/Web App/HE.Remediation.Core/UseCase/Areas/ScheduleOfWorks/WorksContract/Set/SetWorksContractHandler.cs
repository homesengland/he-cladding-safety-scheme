using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.StatusTransition;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Set;

public class SetWorksContractHandler : IRequestHandler<SetWorksContractRequest, Unit>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;
    private readonly IStatusTransitionService _statusTransitionService;

    public SetWorksContractHandler(
        IApplicationDataProvider applicationDataProvider,
        IScheduleOfWorksRepository scheduleOfWorksRepository, 
        IStatusTransitionService statusTransitionService)
    {
        _applicationDataProvider = applicationDataProvider;
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
        _statusTransitionService = statusTransitionService;
    }

    public async Task<Unit> Handle(SetWorksContractRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var hasScheduleOfWorks = await _scheduleOfWorksRepository.HasScheduleOfWorks();
        if (!hasScheduleOfWorks)
        {
            await _scheduleOfWorksRepository.InsertScheduleOfWorks();
        }

        var applicationId = _applicationDataProvider.GetApplicationId();
        await _statusTransitionService.TransitionToStatus(EApplicationStatus.ScheduleOfWorksInProgress, applicationIds: applicationId);

        scope.Complete();

        return Unit.Value;
    }
}
