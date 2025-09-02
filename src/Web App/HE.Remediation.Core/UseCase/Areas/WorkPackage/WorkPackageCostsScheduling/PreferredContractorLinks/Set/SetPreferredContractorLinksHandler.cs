using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.PreferredContractorLinks.Set;

public class SetPreferredContractorLinksHandler : IRequestHandler<SetPreferredContractorLinksRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetPreferredContractorLinksHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Unit> Handle(SetPreferredContractorLinksRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
        var costScheduleStatus = await _workPackageRepository.GetCostsScheduleStatus();
        if (costScheduleStatus is null)
        {
            await _workPackageRepository.InsertCostsSchedule();
        }

        await _workPackageRepository.UpdateCostSchedulePReferredContractorLinks(request.PreferredContractorLinks, request.PreferredContractorLinkAdditionalNotes);

        await _workPackageRepository.UpdateCostsScheduleStatus(ETaskStatus.InProgress);

        scope.Complete();
        
        return Unit.Value;
    }
}
