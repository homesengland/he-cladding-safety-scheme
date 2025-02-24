using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.PreliminaryCosts.Get;

public class GetPreliminaryCostsHandler : IRequestHandler<GetPreliminaryCostsRequest, GetPreliminaryCostsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IVariationRequestRepository _variationRequestRepository;

    public GetPreliminaryCostsHandler(IApplicationDataProvider applicationDataProvider,
                                 IBuildingDetailsRepository buildingDetailsRepository,
                                 IApplicationRepository applicationRepository,
                                 IVariationRequestRepository variationRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _variationRequestRepository = variationRequestRepository;
    }

    public async Task<GetPreliminaryCostsResponse> Handle(GetPreliminaryCostsRequest request, CancellationToken cancellationToken)
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

        var preliminaryCosts = await _variationRequestRepository.GetVariationPreliminaryCosts((Guid)variationRequestId);

        if (preliminaryCosts is null)
        {
            return new GetPreliminaryCostsResponse
            {
                HasCostVariation = false,
                IsThirdPartyContributionVariation = variationReason?.IsThirdPartyContributionVariation
            };
        }

        return new GetPreliminaryCostsResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            IsSubmitted = isSubmitted,
            VariationMainContractorPreliminariesAmount = preliminaryCosts.VariationMainContractorPreliminariesAmount,
            VariationMainContractorPreliminariesDescription = preliminaryCosts.VariationMainContractorPreliminariesDescription,
            VariationAccessAmount = preliminaryCosts.VariationAccessAmount,
            VariationAccessDescription = preliminaryCosts.VariationAccessDescription,
            VariationOverheadsAndProfitAmount = preliminaryCosts.VariationOverheadsAndProfitAmount,
            VariationOverheadsAndProfitDescription = preliminaryCosts.VariationOverheadsAndProfitDescription,
            VariationContractorContingenciesAmount = preliminaryCosts.VariationContractorContingenciesAmount,
            VariationContractorContingenciesDescription = preliminaryCosts.VariationContractorContingenciesDescription,

            WorkPackageMainContractorPreliminariesAmount = preliminaryCosts.WorkPackageMainContractorPreliminariesAmount,
            WorkPackageMainContractorPreliminariesDescription = preliminaryCosts.WorkPackageMainContractorPreliminariesDescription,
            WorkPackageAccessAmount = preliminaryCosts.WorkPackageAccessAmount,
            WorkPackageAccessDescription = preliminaryCosts.WorkPackageAccessDescription,
            WorkPackageOverheadsAndProfitAmount = preliminaryCosts.WorkPackageOverheadsAndProfitAmount,
            WorkPackageOverheadsAndProfitDescription = preliminaryCosts.WorkPackageOverheadsAndProfitDescription,
            WorkPackageContractorContingenciesAmount = preliminaryCosts.WorkPackageContractorContingenciesAmount,
            WorkPackageContractorContingenciesDescription = preliminaryCosts.WorkPackageContractorContingenciesDescription,

            IsThirdPartyContributionVariation = variationReason?.IsThirdPartyContributionVariation
        };
    }
}


