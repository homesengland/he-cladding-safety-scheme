using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Costs.Create;

public class CreateCostsHandler : IRequestHandler<CreateCostsRequest>
{
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public CreateCostsHandler(IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
    }

    public async ValueTask<Unit> Handle(CreateCostsRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _scheduleOfWorksRepository.CreateCosts();

        scope.Complete();

        return Unit.Value;
    }
}