using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

public class GetInformationHandler : IRequestHandler<GetInformationRequest, GetInformationResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;

    public GetInformationHandler(
        IApplicationDataProvider applicationDataProvider, 
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
    }

    public async Task<GetInformationResponse> Handle(GetInformationRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        return new GetInformationResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName
        };
    }
}

public class GetInformationRequest : IRequest<GetInformationResponse>
{
    private GetInformationRequest()
    {
    }

    public static readonly GetInformationRequest Request = new();
}

public class GetInformationResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
}