using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonPlanningNotApplied.GetReasonPlanningNotApplied;

public class GetReasonPlanningNotAppliedHandler : IRequestHandler<GetReasonPlanningNotAppliedRequest, GetReasonPlanningNotAppliedResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetReasonPlanningNotAppliedHandler(IApplicationDataProvider applicationDataProvider,
                                          IBuildingDetailsRepository buildingDetailsRepository,
                                          IApplicationRepository applicationRepository,
                                          IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<GetReasonPlanningNotAppliedResponse> Handle(GetReasonPlanningNotAppliedRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var planningPermissionNotAppliedReason = await _progressReportingRepository.GetProgressReportPlanningPermissionNotAppliedReason();

        var hasVisitedCheckYourAnswers = await _progressReportingRepository.GetHasVisitedCheckYourAnswers(
            new GetHasVisitedCheckYourAnswersParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = _applicationDataProvider.GetProgressReportId()
            });

        return new GetReasonPlanningNotAppliedResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            ReasonPlanningPermissionNotApplied = planningPermissionNotAppliedReason?.ReasonPlanningPermissionNotApplied,
            PlanningPermissionNeedsSupport = planningPermissionNotAppliedReason?.PlanningPermissionNeedsSupport,
            HasVisitedCheckYourAnswers = hasVisitedCheckYourAnswers
        };
    }
}
