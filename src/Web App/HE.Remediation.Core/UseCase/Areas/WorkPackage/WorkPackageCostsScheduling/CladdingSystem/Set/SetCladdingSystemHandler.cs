using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.CostsScheduling;
using HE.Remediation.Core.Enums;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CladdingSystem.Set;

public class SetCladdingSystemHandler : IRequestHandler<SetCladdingSystemRequest, Unit>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetCladdingSystemHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Unit> Handle(SetCladdingSystemRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var claddingSystem = await _workPackageRepository.GetCostsScheduleCladdingSystemIsBeingRemoved(request.FireRiskCladdingSystemsId);

        if (claddingSystem is null || claddingSystem.CostsScheduleCladdingSystemId is null)
        {
            await InsertCladdingSystem(request);
        }
        else
        {
            await UpdateCladdingSystem(request);
        }

        await UpdateStatuses(request);

        scope.Complete();

        return Unit.Value;
    }

    private async Task InsertCladdingSystem(SetCladdingSystemRequest request)
    {
        await _workPackageRepository.InsertCostsScheduleCladdingSystem(new InsertCladdingSystemParameters 
        {
            FireRiskCladdingSystemsId = request.FireRiskCladdingSystemsId, 
            IsBeingRemoved = request.IsBeingRemoved 
        });
    }

    private async Task UpdateCladdingSystem(SetCladdingSystemRequest request)
    {
        await _workPackageRepository.UpdateCostsScheduleCladdingSystemIsBeingRemoved(new UpdateCladdingSystemParameters
        {
            FireRiskCladdingSystemsId = request.FireRiskCladdingSystemsId,
            IsBeingRemoved = request.IsBeingRemoved
        });
    }

    private async Task UpdateStatuses(SetCladdingSystemRequest request)
    {
        await _workPackageRepository.UpdateCostsScheduleCladdingSystemStatus(request.FireRiskCladdingSystemsId,
            request.IsBeingRemoved is EReplacementCladding.Full or EReplacementCladding.Partial
            ? ETaskStatus.InProgress
            : ETaskStatus.Completed);

        await _workPackageRepository.UpdateCostsScheduleStatus(ETaskStatus.InProgress);
    }
}
