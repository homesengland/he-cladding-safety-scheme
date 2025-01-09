using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.IneligibleCostsChanges.Get;

public class GetIneligibleCostsChangesHandler : IRequestHandler<GetIneligibleCostsChangesRequest, GetIneligibleCostsChangesResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IVariationRequestRepository _variationRequestRepository;

    public GetIneligibleCostsChangesHandler(IApplicationDataProvider applicationDataProvider,
                                 IBuildingDetailsRepository buildingDetailsRepository,
                                 IApplicationRepository applicationRepository,
                                 IVariationRequestRepository variationRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _variationRequestRepository = variationRequestRepository;
    }

    public async Task<GetIneligibleCostsChangesResponse> Handle(GetIneligibleCostsChangesRequest request, CancellationToken cancellationToken)
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

        var ineligibleCosts = await _variationRequestRepository.GetVariationIneligibleCostsChanges((Guid)variationRequestId);

        if (ineligibleCosts is null)
        {
            return new GetIneligibleCostsChangesResponse
            {
                HasCostVariation = false,
                IsThirdPartyContributionVariation = variationReason?.IsThirdPartyContributionVariation
            };
        }

        return new GetIneligibleCostsChangesResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            IsSubmitted = isSubmitted,
            VariationIneligibleAmount = ineligibleCosts?.VariationIneligibleAmount,
            VariationIneligibleDescription = ineligibleCosts.VariationIneligibleDescription,
            WorkPackageIneligibleAmount = ineligibleCosts?.WorkPackageIneligibleAmount ?? 0,
            WorkPackageIneligibleDescription = ineligibleCosts.WorkPackageIneligibleDescription,
            IsThirdPartyContributionVariation = variationReason?.IsThirdPartyContributionVariation
        };
    }
}

