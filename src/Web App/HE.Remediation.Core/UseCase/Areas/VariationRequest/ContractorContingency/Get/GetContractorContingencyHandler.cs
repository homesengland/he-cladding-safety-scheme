using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.ContractorContingency.Get;

public class GetContractorContingencyHandler : IRequestHandler<GetContractorContingencyRequest, GetContractorContingencyResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IVariationRequestRepository _variationRequestRepository;

    public GetContractorContingencyHandler(IApplicationDataProvider applicationDataProvider,
                                 IBuildingDetailsRepository buildingDetailsRepository,
                                 IApplicationRepository applicationRepository,
                                 IVariationRequestRepository variationRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _variationRequestRepository = variationRequestRepository;
    }

    public async Task<GetContractorContingencyResponse> Handle(GetContractorContingencyRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var isSubmitted = await _variationRequestRepository.IsVariationRequestSubmitted();
        var variationRequestId = await _variationRequestRepository.GetLatestVariationRequestId();

        if (!variationRequestId.HasValue)
        {
            throw new EntityNotFoundException(
                 "No valid Variation Request found for this Application.");
        }

        var usedContractorContingency = await _variationRequestRepository.UsedVariationContractorContingency((Guid)variationRequestId);
        var usedContractorContingencyResult = await _variationRequestRepository.GetVariationContractorContingency((Guid)variationRequestId);
        var hasVariationIneligibleCosts = await _variationRequestRepository.HasVariationIneligibleCosts((Guid)variationRequestId);
        return new GetContractorContingencyResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            IsSubmitted = isSubmitted,
            UsedVariationContractorContingency = usedContractorContingency,
            ContractorContingencyAdditionalNotes = usedContractorContingencyResult.UsedContractorContingencyDescription,
            IneligibleCosts = hasVariationIneligibleCosts
        };
    }
}
