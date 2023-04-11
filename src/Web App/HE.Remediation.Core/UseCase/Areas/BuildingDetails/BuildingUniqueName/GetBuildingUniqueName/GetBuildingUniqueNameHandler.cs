using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingUniqueName.GetBuildingUniqueName
{
    public class GetBuildingUniqueNameHandler : IRequestHandler<GetBuildingUniqueNameRequest, GetBuildingUniqueNameResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetBuildingUniqueNameHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetBuildingUniqueNameResponse> Handle(GetBuildingUniqueNameRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();
            var response = await GetBuildingUniqueName(applicationId);
            return response;
        }

        private async Task<GetBuildingUniqueNameResponse> GetBuildingUniqueName(Guid applicationId)
        {
            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetBuildingUniqueNameResponse>("GetBuildingUniqueName", new { applicationId });

            return result ?? new GetBuildingUniqueNameResponse();
        }
    }
}
