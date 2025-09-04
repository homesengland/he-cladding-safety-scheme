using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.StatusTransition;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.SetPursuedSourcesFunding
{
    public class SetPursuedSourcesFundingHandler : IRequestHandler<SetPursuedSourcesFundingRequest, SetPursuedSourcesFundingResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;
        private readonly IStatusTransitionService _statusTransitionService;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IAlternateFundingRepository _alternateFundingRepository;

        public SetPursuedSourcesFundingHandler(
            IApplicationDataProvider applicationDataProvider, 
            IDbConnectionWrapper db, 
            IStatusTransitionService statusTransitionService, 
            IApplicationRepository applicationRepository, 
            IAlternateFundingRepository alternateFundingRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
            _statusTransitionService = statusTransitionService;
            _applicationRepository = applicationRepository;
            _alternateFundingRepository = alternateFundingRepository;
        }

        public async Task<SetPursuedSourcesFundingResponse> Handle(SetPursuedSourcesFundingRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var pursuedSourcesFunding = await _alternateFundingRepository.GetPursuedSourcesFunding(applicationId);
            var visitedCheckYourAnswers = await _alternateFundingRepository.GetAlternateFundingVisitedCheckYourAnswers(applicationId);

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await SetPursuedSourcesFunding(request, applicationId);
            var isSocialSector = await _applicationRepository.IsSocialSector(applicationId);

            if (pursuedSourcesFunding == EPursuedSourcesFundingType.ExhaustedAllRoutes &&
                request.PursuedSourcesFunding != EPursuedSourcesFundingType.ExhaustedAllRoutes)
            {
                await _alternateFundingRepository.SetAlternateFundingVisitedCheckYourAnswers(
                    new SetAlternateFundingVisitedCheckYourAnswersParameters
                    {
                        ApplicationId = applicationId,
                        VisitedCheckYourAnswers = false
                    });

                visitedCheckYourAnswers = false;
            }

            scope.Complete();
            return new SetPursuedSourcesFundingResponse
            {
                IsSocialSector = isSocialSector,
                VisitedCheckYourAnswers = visitedCheckYourAnswers
            };
        }

        private async Task SetPursuedSourcesFunding(SetPursuedSourcesFundingRequest request, Guid applicationId)
        {
            await _db.ExecuteAsync("UpsertPursuedSourcesFunding", new
            {
                ApplicationId = applicationId, 
                request.PursuedSourcesFunding
            });

            await _statusTransitionService.TransitionToStatus(EApplicationStatus.ApplicationInProgress, applicationIds: applicationId);
        }
    }
}
