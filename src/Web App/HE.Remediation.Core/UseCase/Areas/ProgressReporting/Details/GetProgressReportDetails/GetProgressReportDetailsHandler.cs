using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.Details.GetProgressReportDetails;

public class GetProgressReportDetailsHandler : IRequestHandler<GetProgressReportDetailsRequest, GetProgressReportDetailsResponse>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetProgressReportDetailsHandler(
        IApplicationRepository applicationRepository,
        IBuildingDetailsRepository buildingDetailsRepository,
        IProgressReportingRepository progressReportingRepository,
        IApplicationDataProvider applicationDataProvider)
    {
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _progressReportingRepository = progressReportingRepository;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<GetProgressReportDetailsResponse> Handle(GetProgressReportDetailsRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        _progressReportingRepository.SetProgressReportId(request.ProgressReportId);

        var details = await _progressReportingRepository.GetProgressReportDetails(applicationId, request.ProgressReportId);

        var summariseProgress = await _progressReportingRepository.GetProgressReportProgressSummary(request.ProgressReportId);

        return new GetProgressReportDetailsResponse
        {
	        ApplicationReferenceNumber = reference,
	        AppliedForPlanningPermission = details.AppliedForPlanningPermission,
	        BuildingName = buildingName,
	        DateCreated = details.DateCreated,
	        DateSubmitted = details.DateSubmitted,
	        IntentToProceedType = details.IntentToProceedType,
	        NextReportExists = details.NextReportExists,
	        ExpectedWorksPackageSubmissionDate = details.ExpectedWorksPackageSubmissionDate,
	        LeadDesignerAppointed = details.LeadDesignerAppointed,
	        LeaseholdersInformed = details.LeaseholdersInformed,
	        OtherMembersAppointed = details.OtherMembersAppointed,
	        QuotesSought = details.QuotesSought,
	        WhyYouHaveNotSoughtQuotes = details.WhyYouHaveNotSoughtQuotes,
	        QuotesNotSoughtReason = details.QuotesNotSoughtReason,
	        ReasonPlanningPermissionNotApplied = details.ReasonPlanningPermissionNotApplied,
	        RequirePlanningPermission = details.RequirePlanningPermission,
	        SpentAnyFunding = details.SpentAnyFunding,
	        version = details.Version,
	        GoalSummary = summariseProgress.GoalSummary,
	        ProgressSummary = summariseProgress.ProgressSummary,
	        IsSupportNeeded = summariseProgress.RequiresSupport,
	        BuildingControlRequired = details.BuildingControlRequired,
	        HasAppliedForBuildingControl = details.HasAppliedForBuildingControl,
	        BuildingControlForecastSubmissionDate = details.BuildingControlForecastSubmissionDate,
	        BuildingControlForecastInformation = details.BuildingControlForecastInformation,
	        BuildingControlActualSubmissionDate = details.BuildingControlActualSubmissionDate,
	        BuildingControlActualSubmissionInformation = details.BuildingControlActualSubmissionInformation,
	        BuildingControlApplicationReference = details.BuildingControlApplicationReference,
	        BuildingControlValidationDate = details.BuildingControlValidationDate,
	        BuildingControlValidationInformation = details.BuildingControlValidationInformation,
	        BuildingControlDecisionDate = details.BuildingControlDecisionDate,
	        BuildingControlDecisionInformation = details.BuildingControlDecisionInformation,
	        BuildingControlDecision = details.BuildingControlDecision,
            ExpectedStartDateOnSite = details.ExpectedStartDateOnSite,
			
            TeamMembers = details.TeamMembers.Select(x => new GetProgressReportDetailsResponse.TeamMember
	        {
		        Id = x.Id,
		        Name = x.Name,
		        CompanyName = x.CompanyName,
		        OtherRole = x.OtherRole,
		        Role = x.Role
	        }).ToList()
        };
    }
}