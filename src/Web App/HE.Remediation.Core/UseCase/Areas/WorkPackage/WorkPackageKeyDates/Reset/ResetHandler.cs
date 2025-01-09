using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageKeyDates.Reset
{
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

            await _workPackageRepository.ResetKeyDates();

            var taskStatus = await _workPackageRepository.GetKeyDatesStatus();

            if (taskStatus == ETaskStatus.Completed)
            {
                await _workPackageRepository.UpdateKeyDatesStatus(ETaskStatus.InProgress);
            }

            scope.Complete();

            return Unit.Value;
        }
    }
}
