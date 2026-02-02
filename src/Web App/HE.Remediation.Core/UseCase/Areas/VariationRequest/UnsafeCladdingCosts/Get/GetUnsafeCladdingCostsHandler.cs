using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults.VariationRequest;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.UnsafeCladdingCosts.Get;

public class GetUnsafeCladdingCostsHandler : IRequestHandler<GetUnsafeCladdingCostsRequest, GetUnsafeCladdingCostsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IVariationRequestRepository _variationRequestRepository;

    public GetUnsafeCladdingCostsHandler(IApplicationDataProvider applicationDataProvider,
                                 IBuildingDetailsRepository buildingDetailsRepository,
                                 IApplicationRepository applicationRepository,
                                 IVariationRequestRepository variationRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _variationRequestRepository = variationRequestRepository;
    }

    public async ValueTask<GetUnsafeCladdingCostsResponse> Handle(GetUnsafeCladdingCostsRequest request, CancellationToken cancellationToken)
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

        var unsafeCladdingCosts = await _variationRequestRepository.GetVariationUnsafeCladdingCosts((Guid)variationRequestId);

        if (unsafeCladdingCosts is null)
        {
            return new GetUnsafeCladdingCostsResponse
            {
                HasCostVariation = false,
                IsThirdPartyContributionVariation = variationReason?.IsThirdPartyContributionVariation
            };
        }

        return new GetUnsafeCladdingCostsResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            IsSubmitted = isSubmitted,
            VariationRemovalOfCladdingAmount = unsafeCladdingCosts?.VariationRemovalOfCladdingAmount,
            VariationRemovalOfCladdingDescription = unsafeCladdingCosts.VariationRemovalOfCladdingDescription,
            WorkPackageRemovalOfCladdingAmount = unsafeCladdingCosts?.WorkPackageRemovalOfCladdingAmount,
            WorkPackageRemovalOfCladdingDescription = unsafeCladdingCosts.WorkPackageRemovalOfCladdingDescription,
            IsThirdPartyContributionVariation = variationReason?.IsThirdPartyContributionVariation
        };
    }
}
