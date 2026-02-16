using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingPartOfDevelopment.GetBuildingPartOfDevelopment
{
    public class GetBuildingPartOfDevelopmentHandler : IRequestHandler<GetBuildingPartOfDevelopmentRequest, GetBuildingPartOfDevelopmentResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetBuildingPartOfDevelopmentHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<GetBuildingPartOfDevelopmentResponse> Handle(GetBuildingPartOfDevelopmentRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await GetBuildingPartOfDevelopment(applicationId);

            return response;
        }

        private async ValueTask<GetBuildingPartOfDevelopmentResponse> GetBuildingPartOfDevelopment(Guid applicationId)
        {
            var response = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetBuildingPartOfDevelopmentResponse>("GetBuildingPartOfDevelopment", new { applicationId });

            return response ?? new GetBuildingPartOfDevelopmentResponse();
        }
    }
}
