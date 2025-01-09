using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.SetFundingStillPursuing
{
    public class SetFundingStillPursuingHandler : IRequestHandler<SetFundingStillPursuingRequest, Unit>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IDbConnectionWrapper _db;

        public SetFundingStillPursuingHandler(IApplicationDataProvider applicationDataProvider, IApplicationRepository applicationRepository, IDbConnectionWrapper db)
        {
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
            _db = db;
        }

        public async Task<Unit> Handle(SetFundingStillPursuingRequest request, CancellationToken cancellationToken)
        {
            await SetFundingStillPursuing(request);
            return Unit.Value;
        }

        private async Task SetFundingStillPursuing(SetFundingStillPursuingRequest request)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var hasDeveloperPledgeAnswer =
                request.FundingStillPursuing != null && request.FundingStillPursuing.Any(x => x == EFundingStillPursuing.SignedUpDevelopersPledge);

            await _db.ExecuteAsync("UpsertFundingStillPursuing", new { applicationId, FundingStillPursuing = string.Join(",", request.FundingStillPursuing.Select(x => (int)x)) });

            if (hasDeveloperPledgeAnswer)
            {
                await _applicationRepository.UpdateApplicationStage(_applicationDataProvider.GetApplicationId(),
                    EApplicationStage.Closed);

                await _applicationRepository.UpdateStatus(_applicationDataProvider.GetApplicationId(),
                    EApplicationStatus.ApplicationNotEligible,
                    "Developer that has signed up to the Developer's pledge.");
            }
            else
            {
                await _applicationRepository.UpdateStatus(_applicationDataProvider.GetApplicationId(),
                    EApplicationStatus.ApplicationInProgress);
            }
        }
    }
}
