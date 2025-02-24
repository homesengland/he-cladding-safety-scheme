using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.StatusTransition;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.DeveloperPledgeStop.SetDeveloperPledgeStop
{
    public class SetDeveloperPledgeStopHandler : IRequestHandler<SetDeveloperPledgeStopRequest, Unit>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;
        private readonly IStatusTransitionService _statusTransitionService;
        private readonly IApplicationRepository _applicationRepository;

        public SetDeveloperPledgeStopHandler(
            IApplicationDataProvider applicationDataProvider, 
            IDbConnectionWrapper db, 
            IStatusTransitionService statusTransitionService, 
            IApplicationRepository applicationRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
            _statusTransitionService = statusTransitionService;
            _applicationRepository = applicationRepository;
        }

        public async Task<Unit> Handle(SetDeveloperPledgeStopRequest request, CancellationToken cancellationToken)
        {
            await SetDeveloperPledgeStop();
            return Unit.Value;
        }

        private async Task SetDeveloperPledgeStop()
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _db.ExecuteAsync("UpdateDeveloperPledgeStop", new { applicationId});

            await _applicationRepository.UpdateApplicationStage(applicationId, EApplicationStage.Closed);

            await _statusTransitionService.TransitionToStatus(EApplicationStatus.ApplicationNotEligible,
                "Developer that has signed up to the Developer's pledge.", applicationId);

            scope.Complete();
        }
    }
}
