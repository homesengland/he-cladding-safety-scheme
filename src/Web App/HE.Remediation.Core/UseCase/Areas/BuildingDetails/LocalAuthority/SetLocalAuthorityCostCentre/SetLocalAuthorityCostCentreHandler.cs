using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.LocalAuthority.SetLocalAuthorityCostCentre
{
    public class SetLocalAuthorityCostCentreHandler : IRequestHandler<SetLocalAuthorityCostCentreRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetLocalAuthorityCostCentreHandler(
            IApplicationDataProvider applicationDataProvider,
            ILocalAuthorityCostCentreRepository costcentreRepository,
            IDbConnectionWrapper dbConnectionWrapper)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(SetLocalAuthorityCostCentreRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();
            await UpdateLocalAuthority(request, applicationId);
            return Unit.Value;
        }

        private async Task UpdateLocalAuthority(SetLocalAuthorityCostCentreRequest request, Guid applicationId)
        {
            await _dbConnectionWrapper.ExecuteAsync("UpdateLocalAuthority",
                new
                {
                    applicationId,
                    request.LocalAuthorityCostCentreId
                }
            );
        }
    }
}
