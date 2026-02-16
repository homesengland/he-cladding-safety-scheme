using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppliedForPlanningPermission.GetAppliedForPlanningPermission;

public class GetAppliedForPlanningPermissionHandler : IRequestHandler<GetAppliedForPlanningPermissionRequest, GetAppliedForPlanningPermissionResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetAppliedForPlanningPermissionHandler(IApplicationDataProvider applicationDataProvider,
                                           IBuildingDetailsRepository buildingDetailsRepository,
                                           IApplicationRepository applicationRepository,
                                           IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<GetAppliedForPlanningPermissionResponse> Handle(GetAppliedForPlanningPermissionRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var appliedForPlanningPermission = await _progressReportingRepository.GetProgressReportAppliedForPlanningPermission();
        var version = await _progressReportingRepository.GetProgressReportVersion();

        var hasVisitedCheckYourAnswers = await _progressReportingRepository.GetHasVisitedCheckYourAnswers(
            new GetHasVisitedCheckYourAnswersParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = _applicationDataProvider.GetProgressReportId()
            });

        return new GetAppliedForPlanningPermissionResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            AppliedForPlanningPermission = appliedForPlanningPermission,
            Version = version,
            HasVisitedCheckYourAnswers = hasVisitedCheckYourAnswers
        };
    }
}
