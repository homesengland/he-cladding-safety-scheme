using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Submitted.Get;

public class GetSubmittedHandler : IRequestHandler<GetSubmittedRequest, GetSubmittedResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IVariationRequestRepository _variationRequestRepository;

    public GetSubmittedHandler(IApplicationDataProvider applicationDataProvider,
                               IBuildingDetailsRepository buildingDetailsRepository,
                               IApplicationRepository applicationRepository,
                               IVariationRequestRepository variationRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _variationRequestRepository = variationRequestRepository;
    }

    public async ValueTask<GetSubmittedResponse> Handle(GetSubmittedRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var variationNumber = await _variationRequestRepository.GetVariationNumber();

        return new GetSubmittedResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            VariationNumber = variationNumber
        };
    }
}
