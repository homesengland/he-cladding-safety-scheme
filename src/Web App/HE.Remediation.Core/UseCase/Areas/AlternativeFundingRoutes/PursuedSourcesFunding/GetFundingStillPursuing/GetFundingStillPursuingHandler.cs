using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.GetFundingStillPursuing
{
    public class GetFundingStillPursuingHandler : IRequestHandler<GetFundingStillPursuingRequest, GetFundingStillPursuingResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;
        private readonly IAlternateFundingRepository _alternateFundingRepository;

        public GetFundingStillPursuingHandler(
            IApplicationDataProvider applicationDataProvider, 
            IDbConnectionWrapper db, 
            IAlternateFundingRepository alternateFundingRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
            _alternateFundingRepository = alternateFundingRepository;
        }

        public async Task<GetFundingStillPursuingResponse> Handle(GetFundingStillPursuingRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var dbResponse = await _db.QueryAsync<EFundingStillPursuing>("GetFundingStillPursuing", new
            {
                ApplicationId = applicationId
            });

            var visitedCheckYourAnswers = await _alternateFundingRepository.GetAlternateFundingVisitedCheckYourAnswers(applicationId);

            return new GetFundingStillPursuingResponse
            {
                FundingStillPursuing = dbResponse,
                VisitedCheckYourAnswers = visitedCheckYourAnswers
            };
        }
    }
}
