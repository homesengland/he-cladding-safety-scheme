using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CladdingSystem.Get;

public class GetCladdingSystemHandler : IRequestHandler<GetCladdingSystemRequest, GetCladdingSystemResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetCladdingSystemHandler(IApplicationDataProvider applicationDataProvider,
                                    IBuildingDetailsRepository buildingDetailsRepository,
                                    IApplicationRepository applicationRepository,
                                    IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async Task<GetCladdingSystemResponse> Handle(GetCladdingSystemRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var claddingSystemResult = await _workPackageRepository.GetCostsScheduleCladdingSystemIsBeingRemoved(request.FireRiskCladdingSystemsId);
        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetCladdingSystemResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            FireRiskCladdingSystemsId = request.FireRiskCladdingSystemsId,
            CladdingSystemIndex = request.CladdingSystemIndex,
            CladdingSystemTypeName = claddingSystemResult?.CladdingSystemTypeName,
            CladdingManufacturerName = claddingSystemResult?.CladdingManufacturerName,
            InsulationTypeName = claddingSystemResult?.InsulationTypeName,
            InsulationManufacturerName = claddingSystemResult?.InsulationManufacturerName,
            IsBeingRemoved = claddingSystemResult?.IsBeingRemoved,
            IsSubmitted = isSubmitted
        };
    }
}
