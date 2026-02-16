using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ResidentialUnits.SetResidentialUnits
{
    public class SetResidentialUnitsHandler : IRequestHandler<SetResidentialUnitsRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetResidentialUnitsHandler(
            IDbConnectionWrapper dbConnectionWrapper,
            IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<Unit> Handle(SetResidentialUnitsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await UpdateResidentialUnits(request, applicationId);

            return Unit.Value;
        }

        private async Task UpdateResidentialUnits(SetResidentialUnitsRequest request, Guid applicationId)
        {
            await _dbConnectionWrapper.ExecuteAsync("UpdateResidentialUnits",
                new
                {
                    applicationId,
                    request.ResidentialUnitsCount,
                    request.NonResidentialUnits
                });
        }
    }
}
