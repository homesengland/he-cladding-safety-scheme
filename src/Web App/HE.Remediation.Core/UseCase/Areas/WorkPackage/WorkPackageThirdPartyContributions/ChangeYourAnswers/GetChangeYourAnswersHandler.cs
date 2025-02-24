using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.ChangeYourAnswers
{
    public class GetChangeYourAnswersHandler : IRequestHandler<GetChangeYourAnswersRequest>
    {
        private readonly IWorkPackageRepository _workPackageRepository;

        public GetChangeYourAnswersHandler(IWorkPackageRepository workPackageRepository)
        {
            _workPackageRepository = workPackageRepository;
        }

        public async Task<Unit> Handle(GetChangeYourAnswersRequest request, CancellationToken cancellationToken)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _workPackageRepository.ResetThirdPartyContributions();

            var taskStatus = await _workPackageRepository.GetThirdPartyContributionsStatus();

            if (taskStatus == ETaskStatus.Completed)
            {
                await _workPackageRepository.UpdateThirdPartyContributionsStatus(ETaskStatus.InProgress);
            }

            scope.Complete();

            return Unit.Value;
        }
    }
}
