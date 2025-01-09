using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Set;

public class SetWorksContractHandler : IRequestHandler<SetWorksContractRequest, Unit>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public SetWorksContractHandler(IApplicationDataProvider applicationDataProvider,
                                   IApplicationRepository applicationRepository,
                                   IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
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
        await _applicationRepository.UpdateStatus(applicationId, EApplicationStatus.ScheduleOfWorksInProgress);

        scope.Complete();

        return Unit.Value;
    }
}
