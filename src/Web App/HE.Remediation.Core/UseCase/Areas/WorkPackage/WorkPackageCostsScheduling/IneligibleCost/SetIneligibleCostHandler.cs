using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.CostsScheduling;
using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.IneligibleCost;

public class SetIneligibleCostHandler : IRequestHandler<SetIneligibleCostRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetIneligibleCostHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Unit> Handle(SetIneligibleCostRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _workPackageRepository.UpdateCostsScheduleHasIneligibleCosts(request.IneligibleCosts);

        if (request.IneligibleCosts == ENoYes.No)
        {
            await _workPackageRepository.UpdateIneligibleCosts(new UpdateIneligibleCostsParameters
            {
                IneligibleAmount = null,
                IneligibleDescription = null
            });

            await _workPackageRepository.UpdateCostsScheduleStatus(ETaskStatus.Completed);
        }
        else
        {
            await _workPackageRepository.UpdateCostsScheduleStatus(ETaskStatus.InProgress);
        }

        scope.Complete();

        return Unit.Value;
    }
}