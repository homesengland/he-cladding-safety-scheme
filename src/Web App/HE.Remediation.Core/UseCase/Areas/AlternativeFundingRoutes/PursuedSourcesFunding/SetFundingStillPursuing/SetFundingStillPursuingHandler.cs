using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.SetFundingStillPursuing
{
    public class SetFundingStillPursuingHandler : IRequestHandler<SetFundingStillPursuingRequest, Unit>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;

        public SetFundingStillPursuingHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db)
        {
            _applicationDataProvider = applicationDataProvider;
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

            await _db.ExecuteAsync("UpsertFundingStillPursuing", new { applicationId, FundingStillPursuing = string.Join(",", request.FundingStillPursuing.Select(x => (int)x)) });
        }
    }
}
