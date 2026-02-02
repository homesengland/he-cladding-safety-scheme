using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.AboutCosts.Get
{
    public class GetAboutCostsHandler : IRequestHandler<GetAboutCostsRequest, GetAboutCostsResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;
        private readonly IVariationRequestRepository _variationRequestRepository;

        public GetAboutCostsHandler(IApplicationDataProvider applicationDataProvider,
                                    IBuildingDetailsRepository buildingDetailsRepository,
                                    IApplicationRepository applicationRepository,
                                    IVariationRequestRepository variationRequestRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _buildingDetailsRepository = buildingDetailsRepository;
            _applicationRepository = applicationRepository;
            _variationRequestRepository = variationRequestRepository;
        }

        public async ValueTask<GetAboutCostsResponse> Handle(GetAboutCostsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

            var variationReason = await _variationRequestRepository.GetVariationReason();

            var isSubmitted = await _variationRequestRepository.IsVariationRequestSubmitted();

            return new GetAboutCostsResponse
            {
                BuildingName = buildingName,
                ApplicationReferenceNumber = applicationReferenceNumber,
                IsScopeVariation = variationReason.IsScopeVariation,
                IsTimescaleVariation = variationReason.IsTimescaleVariation,
                IsThirdPartyContributionVariation = variationReason.IsThirdPartyContributionVariation,
                IsSubmitted = isSubmitted
            };
        }
    }
}
