using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress
{
    public class GetBuildingAddressHandler : IRequestHandler<GetBuildingAddressRequest, GetBuildingAddressResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetBuildingAddressHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _applicationDataProvider = applicationDataProvider;
            _dbConnectionWrapper = dbConnectionWrapper;
        }

        public async ValueTask<GetBuildingAddressResponse> Handle(GetBuildingAddressRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await GetBuildingAddress(applicationId);

            return response;
        }

        private async ValueTask<GetBuildingAddressResponse> GetBuildingAddress(Guid applicationId)
        {
            var response = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetBuildingAddressResponse>("GetBuildingAddress", new { applicationId });

            return response ?? new GetBuildingAddressResponse();
        }
    }
}
