using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.ScheduleOfWorks;
using HE.Remediation.Core.UseCase.Shared.Costs.Set;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Costs.Set;

public class SetCostsHandler : IRequestHandler<SetCostsRequest>
{
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public SetCostsHandler(IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
    }

    public async ValueTask<Unit> Handle(SetCostsRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var costs = request.MonthlyCosts.Select(c =>
            new UpdateCostParameters
            {
                Id = c.Id,
                Date = c.MonthDate,
                Amount = c.Amount
            })
            .ToList();

        await _scheduleOfWorksRepository.UpdateCosts(costs);

        scope.Complete();

        return Unit.Value;
    }
}