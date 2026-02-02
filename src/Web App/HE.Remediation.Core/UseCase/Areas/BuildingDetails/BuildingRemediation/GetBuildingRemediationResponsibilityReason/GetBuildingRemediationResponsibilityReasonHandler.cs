using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;


namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingRemediation.GetBuildingRemediationResponsibilityReason
{
    internal class GetBuildingRemediationResponsibilityReasonHandler : IRequestHandler<GetBuildingRemediationResponsibilityReasonRequest, GetBuildingRemediationResponsibilityResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;

        public GetBuildingRemediationResponsibilityReasonHandler(
            IApplicationDataProvider applicationDataProvider, 
            IBuildingDetailsRepository buildingDetailsRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _buildingDetailsRepository = buildingDetailsRepository;
        }

        public async ValueTask<GetBuildingRemediationResponsibilityResponse> Handle(GetBuildingRemediationResponsibilityReasonRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var result = await _buildingDetailsRepository.GetBuildingRemediationResponsibilityType(applicationId);

            var response = new GetBuildingRemediationResponsibilityResponse
            {
                BuildingRemediationResponsibilityType = result?.BuildingRemediationResponsibilityTypeId,
                HasBsrCode = result?.BuildingHasSafetyRegulatorRegistrationCode,
                ApplicationScheme = _applicationDataProvider.GetApplicationScheme()
            };

            return response;
        }
    }
}
