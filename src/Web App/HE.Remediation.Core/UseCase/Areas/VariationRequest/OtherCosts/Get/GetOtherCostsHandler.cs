using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.OtherCosts.Get;

public class GetOtherCostsHandler : IRequestHandler<GetOtherCostsRequest, GetOtherCostsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IVariationRequestRepository _variationRequestRepository;

    public GetOtherCostsHandler(IApplicationDataProvider applicationDataProvider,
                                 IBuildingDetailsRepository buildingDetailsRepository,
                                 IApplicationRepository applicationRepository,
                                 IVariationRequestRepository variationRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _variationRequestRepository = variationRequestRepository;
    }

    public async ValueTask<GetOtherCostsResponse> Handle(GetOtherCostsRequest request, CancellationToken cancellationToken)
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

        var otherCosts = await _variationRequestRepository.GetVariationOtherCosts((Guid)variationRequestId);

        if (otherCosts is null)
        {
            return new GetOtherCostsResponse
            {
                HasCostVariation = false,
                IsThirdPartyContributionVariation = variationReason?.IsThirdPartyContributionVariation
            };
        }

        return new GetOtherCostsResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            IsSubmitted = isSubmitted,

            VariationFraewSurveyCostsAmount = otherCosts.VariationFraewSurveyCostsAmount,
            VariationFraewSurveyCostsDescription = otherCosts.VariationFraewSurveyCostsDescription,
            VariationFeasibilityStageAmount = otherCosts.VariationFeasibilityStageAmount,
            VariationFeasibilityStageDescription = otherCosts.VariationFeasibilityStageDescription,
            VariationPostTenderStageAmount = otherCosts.VariationPostTenderStageAmount,
            VariationPostTenderStageDescription = otherCosts.VariationPostTenderStageDescription,
            VariationIrrecoverableVatAmount = otherCosts.VariationIrrecoverableVatAmount,
            VariationIrrecoverableVatDescription = otherCosts.VariationIrrecoverableVatDescription,
            VariationPropertyManagerAmount = otherCosts.VariationPropertyManagerAmount,
            VariationPropertyManagerDescription = otherCosts.VariationPropertyManagerDescription,

            WorkPackageFraewSurveyCostsAmount = otherCosts.WorkPackageFraewSurveyCostsAmount,
            WorkPackageFeasibilityStageAmount = otherCosts.WorkPackageFeasibilityStageAmount,
            WorkPackageFeasibilityStageDescription = otherCosts.WorkPackageFeasibilityStageDescription,
            WorkPackagePostTenderStageAmount = otherCosts.WorkPackagePostTenderStageAmount,
            WorkPackagePostTenderStageDescription = otherCosts.WorkPackagePostTenderStageDescription,
            WorkPackageIrrecoverableVatAmount = otherCosts.WorkPackageIrrecoverableVatAmount,
            WorkPackageIrrecoverableVatDescription = otherCosts.WorkPackageIrrecoverableVatDescription,
            WorkPackagePropertyManagerAmount = otherCosts.WorkPackagePropertyManagerAmount,
            WorkPackagePropertyManagerDescription = otherCosts.WorkPackagePropertyManagerDescription,

            IsThirdPartyContributionVariation = variationReason?.IsThirdPartyContributionVariation
        };
    }
}