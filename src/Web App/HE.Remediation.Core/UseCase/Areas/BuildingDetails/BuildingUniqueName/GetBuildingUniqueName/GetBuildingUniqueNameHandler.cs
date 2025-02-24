using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingUniqueName.GetBuildingUniqueName
{
    public class GetBuildingUniqueNameHandler : IRequestHandler<GetBuildingUniqueNameRequest, GetBuildingUniqueNameResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;

        public GetBuildingUniqueNameHandler(
            IApplicationDataProvider applicationDataProvider, 
            IBuildingDetailsRepository buildingDetailsRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _buildingDetailsRepository = buildingDetailsRepository;
        }

        public async Task<GetBuildingUniqueNameResponse> Handle(GetBuildingUniqueNameRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();
            var response = await GetBuildingUniqueName(applicationId);
            return response;
        }

        private async Task<GetBuildingUniqueNameResponse> GetBuildingUniqueName(Guid applicationId)
        {
            var uniqueName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

            return new GetBuildingUniqueNameResponse
            {
                UniqueName = uniqueName
            };
        }
    }
}
