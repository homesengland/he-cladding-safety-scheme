using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.ThirdPartyContribution.Get;

public class GetThirdPartyContributionHandler : IRequestHandler<GetThirdPartyContributionRequest, GetThirdPartyContributionResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IVariationRequestRepository _variationRequestRepository;

    public GetThirdPartyContributionHandler(IApplicationDataProvider applicationDataProvider,
                                   IBuildingDetailsRepository buildingDetailsRepository,
                                   IApplicationRepository applicationRepository,
                                   IVariationRequestRepository variationRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _variationRequestRepository = variationRequestRepository;
    }

    public async Task<GetThirdPartyContributionResponse> Handle(GetThirdPartyContributionRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var variationReason = await _variationRequestRepository.GetVariationReason();

        var variationRequestId = await _variationRequestRepository.GetLatestVariationRequestId();
        var response = await _variationRequestRepository.GetThirdPartyContributionsThirdPartyContribution(variationRequestId);
        var pursuingTypes = await _variationRequestRepository.GetThirdPartyContributionsThirdPartyContributionPursuingTypes(variationRequestId);

        var isSubmitted = await _variationRequestRepository.IsVariationRequestSubmitted();

        return new GetThirdPartyContributionResponse
        {
            ContributionPursuingTypes = pursuingTypes,
            ContributionAmount = response?.ContributionAmount,
            ContributionNotes = response?.ContributionNotes,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            IsScopeVariation = variationReason.IsScopeVariation,
            IsTimescaleVariation = variationReason.IsTimescaleVariation,
            IsCostsVariation = variationReason.IsCostVariation,
            IsSubmitted = isSubmitted
        };
    }
}
