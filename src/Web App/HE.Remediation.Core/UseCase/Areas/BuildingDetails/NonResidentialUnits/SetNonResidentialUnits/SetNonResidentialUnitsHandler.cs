using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.NonResidentialUnits.SetNonResidentialUnits
{
    public class SetNonResidentialUnitsHandler : IRequestHandler<SetNonResidentialUnitsRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetNonResidentialUnitsHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<Unit> Handle(SetNonResidentialUnitsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await UpdateResidentialUnits(request, applicationId);

            return Unit.Value;
        }

        private async Task UpdateResidentialUnits(SetNonResidentialUnitsRequest request, Guid applicationId)
        {
            await _dbConnectionWrapper.ExecuteAsync("UpdateNonResidentialUnits", new { applicationId, request.NonResidentialUnitsCount });
        }
    }
}
