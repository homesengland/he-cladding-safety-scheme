using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Set;

public class SetWorksContractHandler : IRequestHandler<SetWorksContractRequest, Unit>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public SetWorksContractHandler(
        IApplicationDataProvider applicationDataProvider,
        IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
    }

    public async Task<Unit> Handle(SetWorksContractRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

       
        var applicationId = _applicationDataProvider.GetApplicationId();

        var taskStatusesResult = await _scheduleOfWorksRepository.GetScheduleOfWorksTaskStatuses();

        if (taskStatusesResult?.WorksContractStatusId != ETaskStatus.Completed)
        {
            await _scheduleOfWorksRepository.UpdateScheduleOfWorksWorksContractStatus(ETaskStatus.Completed);
        }

        scope.Complete();

        return Unit.Value;
    }
}
