using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingControl.GetBuildingControlRequired
{
    public class GetBuildingControlRequiredHandler : IRequestHandler<GetBuildingControlRequiredRequest, GetBuildingControlRequiredResponse>
    {
        private readonly IProgressReportingRepository _progressReportingRepository;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;

        public GetBuildingControlRequiredHandler(IProgressReportingRepository progressReportingRepository, IApplicationDataProvider applicationDataProvider, IApplicationRepository applicationRepository, IBuildingDetailsRepository buildingDetailsRepository)
        {
            _progressReportingRepository = progressReportingRepository;
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
            _buildingDetailsRepository = buildingDetailsRepository;
        }

        public async Task<GetBuildingControlRequiredResponse> Handle(GetBuildingControlRequiredRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
            var worksPermissionApplied = await _progressReportingRepository.GetProgressReportAppliedForPlanningPermission();
            var worksPermissionRequired = await _progressReportingRepository.GetProgressReportRequirePlanningPermission();
            var showBuildingSafetyRegulatorRegistrationCode = await _progressReportingRepository.GetProgressReportShowBuildingSafetyRegulatorRegistrationCode();

            var buildingHasSafetyRegulatorRegistrationCode = await _progressReportingRepository.GetProgressReportBuildingHasSafetyRegulatorRegistrationCode();
            var version = await _progressReportingRepository.GetProgressReportVersion();

            var result = await _progressReportingRepository.GetBuildingControlRequired();

            return new GetBuildingControlRequiredResponse
            {
                BuildingHasSafetyRegulatorRegistrationCode = buildingHasSafetyRegulatorRegistrationCode,
                BuildingControlRequired = result,
                BuildingName = buildingName,
                WorksPermissionApplied = worksPermissionApplied,
                WorksPermissionRequired = worksPermissionRequired,
                ShowBuildingSafetyRegulatorRegistrationCode = showBuildingSafetyRegulatorRegistrationCode,
                ApplicationReferenceNumber = applicationReferenceNumber,
                Version = version,
        };
        }
    }

    public class GetBuildingControlRequiredRequest : IRequest<GetBuildingControlRequiredResponse>
    {
        private GetBuildingControlRequiredRequest()
        {
        }

        public static readonly GetBuildingControlRequiredRequest Request = new();
    }

    public class GetBuildingControlRequiredResponse
    {
        public bool? BuildingHasSafetyRegulatorRegistrationCode { get; set; }
        public bool? BuildingControlRequired { get; set; }
        public string BuildingName { get; set; }
        public bool? WorksPermissionApplied { get; set; }
        public EYesNoNonBoolean? WorksPermissionRequired { get; set; }
        public bool ShowBuildingSafetyRegulatorRegistrationCode { get; set; }
        public string ApplicationReferenceNumber { get; set; }
        public int Version { get; set; }
    }
}
