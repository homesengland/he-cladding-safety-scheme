using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Reset;

public class ResetHandler : IRequestHandler<ResetRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public ResetHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Unit> Handle(ResetRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _workPackageRepository.ResetGrantCertifyingOfficer();

        var taskStatus = await _workPackageRepository.GetGrantCertifyingOfficerStatus();

        if (taskStatus == ETaskStatus.Completed)
        {
            await _workPackageRepository.UpdateGrantCertifyingOfficerStatus(ETaskStatus.InProgress);
        }

        scope.Complete();

        return Unit.Value;
    }
}
