using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.SetPursuedSourcesFunding
{
    public class SetPursuedSourcesFundingHandler : IRequestHandler<SetPursuedSourcesFundingRequest, Unit>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;
        private readonly IApplicationRepository _applicationRepository;

        public SetPursuedSourcesFundingHandler(IApplicationDataProvider applicationDataProvider, 
                                               IDbConnectionWrapper db,
                                               IApplicationRepository applicationRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
            _applicationRepository = applicationRepository;     
        }

        public async Task<Unit> Handle(SetPursuedSourcesFundingRequest request, CancellationToken cancellationToken)
        {
            await SetPursuedSourcesFunding(request);
            return Unit.Value;
        }

        private async Task SetPursuedSourcesFunding(SetPursuedSourcesFundingRequest request)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _db.ExecuteAsync("UpsertPursuedSourcesFunding", new
            {
                ApplicationId = applicationId, 
                request.PursuedSourcesFunding
            });

            await _applicationRepository.UpdateStatus(_applicationDataProvider.GetApplicationId(), EApplicationStatus.ApplicationInProgress);

            scope.Complete();
        }
    }
}
