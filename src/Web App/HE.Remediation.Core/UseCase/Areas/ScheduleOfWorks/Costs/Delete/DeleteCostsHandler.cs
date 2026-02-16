using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Costs.Delete;

public class DeleteCostsHandler : IRequestHandler<DeleteCostsRequest>
{
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public DeleteCostsHandler(IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
    }

    public async ValueTask<Unit> Handle(DeleteCostsRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _scheduleOfWorksRepository.DeleteCosts();

        scope.Complete();

        return Unit.Value;
    }
}