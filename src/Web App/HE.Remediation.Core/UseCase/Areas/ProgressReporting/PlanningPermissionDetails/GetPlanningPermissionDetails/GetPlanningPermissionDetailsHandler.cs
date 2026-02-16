using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.PlanningPermissionDetails.GetPlanningPermissionDetails
{
    public class GetPlanningPermissionDetailsHandler : IRequestHandler<GetPlanningPermissionDetailsRequest, GetPlanningPermissionDetailsResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;
        private readonly IProgressReportingRepository _progressReportingRepository;

        public GetPlanningPermissionDetailsHandler(IApplicationDataProvider applicationDataProvider,
            IBuildingDetailsRepository buildingDetailsRepository,
            IApplicationRepository applicationRepository,
            IProgressReportingRepository progressReportingRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _buildingDetailsRepository = buildingDetailsRepository;
            _applicationRepository = applicationRepository;
            _progressReportingRepository = progressReportingRepository;
        }

        public async ValueTask<GetPlanningPermissionDetailsResponse> Handle(GetPlanningPermissionDetailsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
            var showBuildingSafetyRegulatorRegistrationCode = await _progressReportingRepository.GetProgressReportShowBuildingSafetyRegulatorRegistrationCode();

            var progressReportPlanningPermissionDetails = await _progressReportingRepository.GetProgressReportPlanningPermissionDetails();

            var version = await _progressReportingRepository.GetProgressReportVersion();

            var hasVisitedCheckYourAnswers = await _progressReportingRepository.GetHasVisitedCheckYourAnswers(
                new GetHasVisitedCheckYourAnswersParameters
                {
                    ApplicationId = applicationId,
                    ProgressReportId = _applicationDataProvider.GetProgressReportId()
                });

            return new GetPlanningPermissionDetailsResponse
            {
                BuildingName = buildingName,
                ApplicationReferenceNumber = applicationReferenceNumber,
                ShowBuildingSafetyRegulatorRegistrationCode = showBuildingSafetyRegulatorRegistrationCode,
                PlanningPermissionSubmittedMonth = progressReportPlanningPermissionDetails.PlanningPermissionSubmittedDate?.Month,
                PlanningPermissionSubmittedYear = progressReportPlanningPermissionDetails.PlanningPermissionSubmittedDate?.Year,
                PlanningPermissionApprovedMonth = progressReportPlanningPermissionDetails.PlanningPermissionApprovedDate?.Month,
                PlanningPermissionApprovedYear = progressReportPlanningPermissionDetails.PlanningPermissionApprovedDate?.Year,
                Version = version,
                HasVisitedCheckYourAnswers = hasVisitedCheckYourAnswers
            };
        }
    }
}