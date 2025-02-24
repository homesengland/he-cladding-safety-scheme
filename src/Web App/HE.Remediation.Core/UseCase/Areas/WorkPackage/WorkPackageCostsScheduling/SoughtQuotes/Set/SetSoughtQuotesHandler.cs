using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.SoughtQuotes.Set;

public class SetSoughtQuotesHandler : IRequestHandler<SetSoughtQuotesRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetSoughtQuotesHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Unit> Handle(SetSoughtQuotesRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
        var costScheduleStatus = await _workPackageRepository.GetCostsScheduleStatus();
        if (costScheduleStatus is null)
        {
            await _workPackageRepository.InsertCostsSchedule();
        }

        await _workPackageRepository.UpdateCostsScheduleSoughtQuotes(request.SoughtQuotes);

        if (request.SoughtQuotes == ENoYes.Yes)
        {
            await _workPackageRepository.UpdateCostsScheduleNoQuotesReason(null);
        }

        await _workPackageRepository.UpdateCostsScheduleStatus(ETaskStatus.InProgress);

        scope.Complete();
        
        return Unit.Value;
    }
}
