using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.IneligibleCosts;

public class GetIneligibleCostsHandler : IRequestHandler<GetIneligibleCostsRequest, GetIneligibleCostsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetIneligibleCostsHandler(
        IApplicationDataProvider applicationDataProvider, 
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<GetIneligibleCostsResponse> Handle(GetIneligibleCostsRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var costs = await _workPackageRepository.GetWorkPackageCosts();
        
        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetIneligibleCostsResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            IneligibleAmount = costs.IneligibleAmount,
            IneligibleDescription = costs.IneligibleDescription,
            IsSubmitted = isSubmitted
        };
    }
}