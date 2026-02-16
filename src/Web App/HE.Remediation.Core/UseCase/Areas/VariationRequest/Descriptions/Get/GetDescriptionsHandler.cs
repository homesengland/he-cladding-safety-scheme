using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Descriptions.Get;

public class GetDescriptionsHandler : IRequestHandler<GetDescriptionsRequest, GetDescriptionsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IVariationRequestRepository _variationRequestRepository;

    public GetDescriptionsHandler(IApplicationDataProvider applicationDataProvider,
                                 IBuildingDetailsRepository buildingDetailsRepository,
                                 IApplicationRepository applicationRepository,
                                 IVariationRequestRepository variationRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _variationRequestRepository = variationRequestRepository;
    }

    public async ValueTask<GetDescriptionsResponse> Handle(GetDescriptionsRequest request, CancellationToken cancellationToken)
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

        var costDescriptions = await _variationRequestRepository.GetVariationCostDescriptions((Guid)variationRequestId);

        if (costDescriptions is null)
        {
            return new GetDescriptionsResponse
            {
                HasCostVariation = false,
                IsThirdPartyContributionVariation = variationReason?.IsThirdPartyContributionVariation
            };
        }

        return new GetDescriptionsResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            IsSubmitted = isSubmitted,
            VariationFraewSurveyCostsDescription = costDescriptions.FraewSurveyDescription,
            VariationRemovalOfCladdingDescription = costDescriptions.RemovalOfCladdingDescription,
            VariationNewCladdingDescription = costDescriptions.NewCladdingDescription,
            VariationOtherEligibleWorkToExternalWallDescription = costDescriptions.OtherEligibleWorkToExternalWallDescription,
            VariationInternalMitigationWorksDescription = costDescriptions.InternalMitigationWorksDescription,
            VariationMainContractorPreliminariesDescription = costDescriptions.MainContractorPreliminariesDescription,
            VariationAccessDescription = costDescriptions.AccessDescription,
            VariationOverheadsAndProfitDescription = costDescriptions.OverheadsAndProfitDescription,
            VariationContractorContingenciesDescription = costDescriptions.ContractorContingenciesDescription,
            VariationFeasibilityStageDescription = costDescriptions.FeasibilityStageDescription,
            VariationPostTenderStageDescription = costDescriptions.PostTenderStageDescription,
            VariationIrrecoverableVatDescription = costDescriptions.IrrecoverableVatDescription,
            VariationPropertyManagerDescription = costDescriptions.PropertyManagerDescription,
            IsThirdPartyContributionVariation = variationReason?.IsThirdPartyContributionVariation
        };
    }
}
