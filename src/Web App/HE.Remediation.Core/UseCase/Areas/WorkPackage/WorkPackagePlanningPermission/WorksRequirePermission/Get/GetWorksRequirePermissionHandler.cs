using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackagePlanningPermission.WorksRequirePermission.Get;

public class GetWorksRequirePermissionHandler : IRequestHandler<GetWorksRequirePermissionRequest, GetWorksRequirePermissionResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetWorksRequirePermissionHandler(IApplicationDataProvider applicationDataProvider,
                                            IBuildingDetailsRepository buildingDetailsRepository,
                                            IApplicationRepository applicationRepository,
                                            IProgressReportingRepository progressReportingRepository,
                                            IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async Task<GetWorksRequirePermissionResponse> Handle(GetWorksRequirePermissionRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var permissionRequiredResult = await _workPackageRepository.GetRequirePlanningPermission();

        var permissionRequired = permissionRequiredResult?.RequirePlanningPermission;

        if (!permissionRequired.HasValue)
        {
            var progressReportRequiredPermission = await _progressReportingRepository.GetLastSubmittedProgressReportRequirePlanningPermission();
            permissionRequired = (EYesNoNonBoolean?)progressReportRequiredPermission switch
            {
                EYesNoNonBoolean.No => false,
                EYesNoNonBoolean.Yes => true,
                _ => null
            };
        }
            
        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetWorksRequirePermissionResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            PermissionRequired = permissionRequired,
            ReasonPermissionNotRequired = permissionRequiredResult?.ReasonPlanningPermissionNotRequired,
            IsSubmitted = isSubmitted
        };
    }
}
