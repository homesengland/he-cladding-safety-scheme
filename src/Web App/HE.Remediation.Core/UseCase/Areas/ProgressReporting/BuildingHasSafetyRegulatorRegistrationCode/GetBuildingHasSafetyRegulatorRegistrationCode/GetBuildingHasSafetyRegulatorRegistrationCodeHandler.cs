using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingHasSafetyRegulatorRegistrationCode.GetBuildingHasSafetyRegulatorRegistrationCode;

public class GetBuildingHasSafetyRegulatorRegistrationCodeHandler : IRequestHandler<GetBuildingHasSafetyRegulatorRegistrationCodeRequest, GetBuildingHasSafetyRegulatorRegistrationCodeResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly IDbConnectionWrapper _connection;

    public GetBuildingHasSafetyRegulatorRegistrationCodeHandler(IApplicationDataProvider applicationDataProvider,
        IBuildingDetailsRepository buildingDetailsRepository,
        IApplicationRepository applicationRepository,
        IProgressReportingRepository progressReportingRepository,
        IDbConnectionWrapper connection)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
        _connection = connection;
    }

    public async ValueTask<GetBuildingHasSafetyRegulatorRegistrationCodeResponse> Handle(GetBuildingHasSafetyRegulatorRegistrationCodeRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var worksPermissionApplied = await _progressReportingRepository.GetProgressReportAppliedForPlanningPermission();
        var worksPermissionRequired = await _progressReportingRepository.GetProgressReportRequirePlanningPermission();

        var progressReportBuildingHasSafetyRegulatorRegistrationCode = await _progressReportingRepository.GetProgressReportBuildingHasSafetyRegulatorRegistrationCode();

        var version = await _progressReportingRepository.GetProgressReportVersion();

        var hasVisitedCheckYourAnswers = await _progressReportingRepository.GetHasVisitedCheckYourAnswers(
            new GetHasVisitedCheckYourAnswersParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = _applicationDataProvider.GetProgressReportId()
            });

        return new GetBuildingHasSafetyRegulatorRegistrationCodeResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            WorksPermissionApplied = worksPermissionApplied,
            WorksPermissionRequired = worksPermissionRequired,
            BuildingHasSafetyRegulatorRegistrationCode = progressReportBuildingHasSafetyRegulatorRegistrationCode,
            Version = version,
            HasVisitedCheckYourAnswers = hasVisitedCheckYourAnswers
        };
    }
}