using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.GetFundingStillPursuing
{
    public class GetFundingStillPursuingHandler : IRequestHandler<GetFundingStillPursuingRequest, GetFundingStillPursuingResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;

        public GetFundingStillPursuingHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
        }

        public async Task<GetFundingStillPursuingResponse> Handle(GetFundingStillPursuingRequest request, CancellationToken cancellationToken)
        {
            return await GetFundingStillPursuing();
        }

        private async Task<GetFundingStillPursuingResponse> GetFundingStillPursuing()
        {
            var dbResponse = await _db.QueryAsync<EFundingStillPursuing>("GetFundingStillPursuing", new
            {
                ApplicationId = _applicationDataProvider.GetApplicationId()
            });

            return new GetFundingStillPursuingResponse
            {
                FundingStillPursuing = dbResponse ?? new List<EFundingStillPursuing>()
            };
        }
    }
}
