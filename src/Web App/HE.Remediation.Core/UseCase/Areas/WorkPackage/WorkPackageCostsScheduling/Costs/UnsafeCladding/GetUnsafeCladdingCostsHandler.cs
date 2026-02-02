using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.UnsafeCladding;

public class GetUnsafeCladdingCostsHandler : IRequestHandler<GetUnsafeCladdingCostsRequest, GetUnsafeCladdingCostsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetUnsafeCladdingCostsHandler(
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

    public async ValueTask<GetUnsafeCladdingCostsResponse> Handle(GetUnsafeCladdingCostsRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var costs = await _workPackageRepository.GetWorkPackageCosts();

        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetUnsafeCladdingCostsResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            UnsafeCladdingRemovalAmount = costs.RemovalOfCladdingAmount,
            UnsafeCladdingRemovalDescription = costs.RemovalOfCladdingDescription,
            IsSubmitted = isSubmitted
        };
    }
}