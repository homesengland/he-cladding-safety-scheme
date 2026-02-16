using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Declaration.Get;

public class GetDeclarationHandler : IRequestHandler<GetDeclarationRequest, GetDeclarationResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IVariationRequestRepository _variationRequestRepository;

    public GetDeclarationHandler(IApplicationDataProvider applicationDataProvider,
                                 IBuildingDetailsRepository buildingDetailsRepository,
                                 IApplicationRepository applicationRepository,
                                 IVariationRequestRepository variationRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _variationRequestRepository = variationRequestRepository;
    }

    public async ValueTask<GetDeclarationResponse> Handle(GetDeclarationRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var isSubmitted = await _variationRequestRepository.IsVariationRequestSubmitted();

        var declaration = await _variationRequestRepository.GetDeclaration();

        return new GetDeclarationResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            ConfirmedAwareOfApproval = declaration?.ConfirmedAwareOfApproval,
            ConfirmedCostsReasonable = declaration?.ConfirmedCostsReasonable,
            ConfirmedCoversRecommendations = declaration?.ConfirmedCoversRecommendations,
            IsSubmitted = isSubmitted
        };
    }
}
