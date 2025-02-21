using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.StatusTransition;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.SetFundingStillPursuing
{
    public class SetFundingStillPursuingHandler : IRequestHandler<SetFundingStillPursuingRequest, Unit>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IDbConnectionWrapper _db;
        private readonly IStatusTransitionService _statusTransitionService;

        public SetFundingStillPursuingHandler(
            IApplicationDataProvider applicationDataProvider, 
            IApplicationRepository applicationRepository, 
            IDbConnectionWrapper db, 
            IStatusTransitionService statusTransitionService)
        {
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
            _db = db;
            _statusTransitionService = statusTransitionService;
        }

        public async Task<Unit> Handle(SetFundingStillPursuingRequest request, CancellationToken cancellationToken)
        {
            await SetFundingStillPursuing(request);
            return Unit.Value;
        }

        private async Task SetFundingStillPursuing(SetFundingStillPursuingRequest request)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            
            await _db.ExecuteAsync("UpsertFundingStillPursuing", new { applicationId, FundingStillPursuing = string.Join(",", request.FundingStillPursuing.Select(x => (int)x)) });
            
            await _statusTransitionService.TransitionToStatus(EApplicationStatus.ApplicationInProgress, applicationIds: applicationId);

            scope.Complete();
        }
    }
}
