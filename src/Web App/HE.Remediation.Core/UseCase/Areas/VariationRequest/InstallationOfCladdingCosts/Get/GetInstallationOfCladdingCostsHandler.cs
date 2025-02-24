using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.InstallationOfCladdingCosts.Get;

public class GetInstallationOfCladdingCostsHandler : IRequestHandler<GetInstallationOfCladdingCostsRequest, GetInstallationOfCladdingCostsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IVariationRequestRepository _variationRequestRepository;

    public GetInstallationOfCladdingCostsHandler(IApplicationDataProvider applicationDataProvider,
                                 IBuildingDetailsRepository buildingDetailsRepository,
                                 IApplicationRepository applicationRepository,
                                 IVariationRequestRepository variationRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _variationRequestRepository = variationRequestRepository;
    }

    public async Task<GetInstallationOfCladdingCostsResponse> Handle(GetInstallationOfCladdingCostsRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var isSubmitted = await _variationRequestRepository.IsVariationRequestSubmitted();

        var variationRequestId = await _variationRequestRepository.GetLatestVariationRequestId();
        var variationReason = await _variationRequestRepository.GetVariationReason();

        if (!variationRequestId.HasValue)
        {
            throw new EntityNotFoundException(
                 "No valid Variation Request found for this Application.");
        }

        var newCladdingCosts = await _variationRequestRepository.GetVariationNewCladdingCosts((Guid)variationRequestId);

        if (newCladdingCosts is null)
        {
            return new GetInstallationOfCladdingCostsResponse
            {
                HasCostVariation = false,
                IsThirdPartyContributionVariation = variationReason?.IsThirdPartyContributionVariation
            };
        }

        return new GetInstallationOfCladdingCostsResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            IsSubmitted = isSubmitted,
            VariationNewCladdingAmount = newCladdingCosts?.VariationNewCladdingAmount,
            VariationNewCladdingDescription = newCladdingCosts.VariationNewCladdingDescription,
            WorkPackageNewCladdingAmount = newCladdingCosts?.WorkPackageNewCladdingAmount,
            WorkPackageNewCladdingDescription = newCladdingCosts.WorkPackageNewCladdingDescription,
            VariationOtherEligibleWorkToExternalWallAmount = newCladdingCosts?.VariationOtherEligibleWorkToExternalWallAmount,
            VariationOtherEligibleWorkToExternalWallDescription = newCladdingCosts.VariationOtherEligibleWorkToExternalWallDescription,
            WorkPackageOtherEligibleWorkToExternalWallAmount = newCladdingCosts?.WorkPackageOtherEligibleWorkToExternalWallAmount,
            WorkPackageOtherEligibleWorkToExternalWallDescription = newCladdingCosts.WorkPackageOtherEligibleWorkToExternalWallDescription,
            VariationInternalMitigationWorksAmount = newCladdingCosts?.VariationInternalMitigationWorksAmount,
            VariationInternalMitigationWorksDescription = newCladdingCosts.VariationInternalMitigationWorksDescription,
            WorkPackageInternalMitigationWorksAmount = newCladdingCosts?.WorkPackageInternalMitigationWorksAmount,
            WorkPackageInternalMitigationWorksDescription = newCladdingCosts.WorkPackageInternalMitigationWorksDescription,
            IsThirdPartyContributionVariation = variationReason?.IsThirdPartyContributionVariation
        };
    }
}

