using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Scope.Get
{
    public class GetAdjustScopeHandler : IRequestHandler<GetAdjustScopeRequest, GetAdjustScopeResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;
        private readonly IVariationRequestRepository _variationRequestRepository;

        public GetAdjustScopeHandler(IApplicationDataProvider applicationDataProvider,
                                     IBuildingDetailsRepository buildingDetailsRepository,
                                     IApplicationRepository applicationRepository,
                                     IVariationRequestRepository variationRequestRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _buildingDetailsRepository = buildingDetailsRepository;
            _applicationRepository = applicationRepository;
            _variationRequestRepository = variationRequestRepository;
        }

        public async Task<GetAdjustScopeResponse> Handle(GetAdjustScopeRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

            var variationScope = await _variationRequestRepository.GetVariationScope();
            var variationReason = await _variationRequestRepository.GetVariationReason();

            var isSubmitted = await _variationRequestRepository.IsVariationRequestSubmitted();

            return new GetAdjustScopeResponse
            {
                BuildingName = buildingName,
                ApplicationReferenceNumber = applicationReferenceNumber,
                ChangeOfScope = variationScope.ChangeOfScope,
                IsTimescaleVariation = variationReason.IsTimescaleVariation,
                IsCostsVariation = variationReason.IsCostsVariation,
                IsThirdPartyContributionVariation = variationReason.IsThirdPartyContributionVariation,
                IsSubmitted = isSubmitted
            };
        }
    }
}
