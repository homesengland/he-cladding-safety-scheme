using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.PlanningPermissionPlannedSubmitDate.GetPlanningPermissionPlannedSubmitDate
{
    public class GetPlanningPermissionPlannedSubmitDateHandler : IRequestHandler<GetPlanningPermissionPlannedSubmitDateRequest, GetPlanningPermissionPlannedSubmitDateResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;
        private readonly IProgressReportingRepository _progressReportingRepository;

        public GetPlanningPermissionPlannedSubmitDateHandler(IApplicationDataProvider applicationDataProvider,
            IBuildingDetailsRepository buildingDetailsRepository,
            IApplicationRepository applicationRepository,
            IProgressReportingRepository progressReportingRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _buildingDetailsRepository = buildingDetailsRepository;
            _applicationRepository = applicationRepository;
            _progressReportingRepository = progressReportingRepository;
        }

        public async Task<GetPlanningPermissionPlannedSubmitDateResponse> Handle(GetPlanningPermissionPlannedSubmitDateRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

            var planningPermissionPlannedSubmitDate = await _progressReportingRepository.GetProgressReportPlanningPermissionPlannedSubmitDate();

            var version = await _progressReportingRepository.GetProgressReportVersion();

            return new GetPlanningPermissionPlannedSubmitDateResponse
            {
                BuildingName = buildingName,
                ApplicationReferenceNumber = applicationReferenceNumber,
                PlanningPermissionPlannedSubmitMonth = planningPermissionPlannedSubmitDate?.Month,
                PlanningPermissionPlannedSubmitYear = planningPermissionPlannedSubmitDate?.Year,
                Version = version
            };
        }
    }
}