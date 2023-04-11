using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.DeveloperPledgeStop.SetDeveloperPledgeStop
{
    public class SetDeveloperPledgeStopHandler : IRequestHandler<SetDeveloperPledgeStopRequest, Unit>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;

        public SetDeveloperPledgeStopHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
        }

        public async Task<Unit> Handle(SetDeveloperPledgeStopRequest request, CancellationToken cancellationToken)
        {
            await SetDeveloperPledgeStop();
            return Unit.Value;
        }

        private async Task SetDeveloperPledgeStop()
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await _db.ExecuteAsync("UpdateDeveloperPledgeStop", new { applicationId});
        }
    }
}
