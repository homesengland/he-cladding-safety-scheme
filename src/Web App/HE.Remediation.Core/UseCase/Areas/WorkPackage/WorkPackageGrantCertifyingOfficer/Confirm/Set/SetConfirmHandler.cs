using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using Mediator;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Confirm.Set;

public class SetConfirmHandler : IRequestHandler<SetConfirmRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetConfirmHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(SetConfirmRequest request, CancellationToken cancellationToken)
    {
        await UpdateGrantCertifyingOfficerConfirmation(request);

        return Unit.Value;
    }

    private async Task UpdateGrantCertifyingOfficerConfirmation(SetConfirmRequest request)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _workPackageRepository.UpdateGrantCertifyingOfficerConfirmation(
            request.CertifyingOfficerResponse);

        await _workPackageRepository.UpdateGrantCertifyingOfficerStatus(ETaskStatus.InProgress);

        scope.Complete();
    }
}