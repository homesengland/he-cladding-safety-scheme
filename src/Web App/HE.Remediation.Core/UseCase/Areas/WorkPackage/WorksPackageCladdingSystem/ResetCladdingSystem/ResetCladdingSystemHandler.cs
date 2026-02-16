using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.ResetCladdingSystem;

public class ResetCladdingSystemHandler : IRequestHandler<ResetCladdingSystemRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public ResetCladdingSystemHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(ResetCladdingSystemRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _workPackageRepository.ResetCladdingSystem(request.FireRiskCladdingSystemsId);

        await _workPackageRepository.UpdateCostsScheduleCladdingSystemStatus(request.FireRiskCladdingSystemsId, ETaskStatus.NotStarted);

        await _workPackageRepository.UpdateWorkPackageCladdingSystemStatus(ETaskStatus.NotStarted);

        scope.Complete();

        return Unit.Value;
    }
}
