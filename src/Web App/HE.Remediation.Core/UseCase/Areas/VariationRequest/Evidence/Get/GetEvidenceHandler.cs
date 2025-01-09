using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Evidence.Get;

public class GetEvidenceHandler : IRequestHandler<GetEvidenceRequest, GetEvidenceResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IVariationRequestRepository _variationRequestRepository;

    public GetEvidenceHandler(IApplicationDataProvider applicationDataProvider,
                              IBuildingDetailsRepository buildingDetailsRepository,
                              IApplicationRepository applicationRepository,
                              IVariationRequestRepository variationRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _variationRequestRepository = variationRequestRepository;
    }

    public async Task<GetEvidenceResponse> Handle(GetEvidenceRequest request, CancellationToken cancellationToken)
    {
        var variationRequestId = await _variationRequestRepository.GetLatestVariationRequestId();

        if (!variationRequestId.HasValue)
        {
            throw new EntityNotFoundException(
                 "No valid Variation Request found for this Application.");
        }

        var applicationId = _applicationDataProvider.GetApplicationId();
        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var isSubmitted = await _variationRequestRepository.IsVariationRequestSubmitted();

        var evidenceFiles = await _variationRequestRepository.GetEvidence();
        var hasVariationIneligibleCosts = await _variationRequestRepository.HasVariationIneligibleCosts((Guid)variationRequestId);
        var variationReason = await _variationRequestRepository.GetVariationReason();

        return new GetEvidenceResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            IsSubmitted = isSubmitted,
            IneligibleCosts = hasVariationIneligibleCosts,
            AddedFiles = evidenceFiles,
            IsThirdPartyContributionVariation = variationReason?.IsThirdPartyContributionVariation,
        };
    }
}
