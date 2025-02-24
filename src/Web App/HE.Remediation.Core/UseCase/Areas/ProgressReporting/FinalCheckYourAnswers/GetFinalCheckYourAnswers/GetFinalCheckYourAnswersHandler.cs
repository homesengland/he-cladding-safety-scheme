using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.FinalCheckYourAnswers.GetFinalCheckYourAnswers;

public class GetFinalCheckYourAnswersHandler : IRequestHandler<GetFinalCheckYourAnswersRequest, GetFinalCheckYourAnswersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetFinalCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider,
                                           IBuildingDetailsRepository buildingDetailsRepository,
                                           IApplicationRepository applicationRepository,
                                           IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<GetFinalCheckYourAnswersResponse> Handle(GetFinalCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var checkMyAnswersResult = await _progressReportingRepository.GetProgressReportCheckMyAnswers();

        var teamMembers = await _progressReportingRepository.GetTeamMembers();

        var gco = await _progressReportingRepository.GetGrantCerifyingOfficerAnswers();

        return new GetFinalCheckYourAnswersResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            LeaseholdersInformed = checkMyAnswersResult?.LeaseholdersInformed,
            LeadDesignerAppointed = checkMyAnswersResult?.LeadDesignerAppointed,
            OtherMembersAppointed = checkMyAnswersResult?.OtherMembersAppointed,
            QuotesSought = checkMyAnswersResult?.QuotesSought,
            RequirePlanningPermission = checkMyAnswersResult?.RequirePlanningPermission,
            AppliedForPlanningPermission = checkMyAnswersResult?.AppliedForPlanningPermission,
            PlanningPermissionPlannedSubmitDate = checkMyAnswersResult?.PlanningPermissionPlannedSubmitDate,
            PlanningPermissionSubmittedDate = checkMyAnswersResult?.PlanningPermissionSubmittedDate,
            PlanningPermissionApprovedDate = checkMyAnswersResult?.PlanningPermissionApprovedDate,
            ShowBuildingSafetyRegulatorRegistration = checkMyAnswersResult.ShowBuildingSafetyRegulatorRegistration,
            BuildingHasSafetyRegulatorRegistrationCode = checkMyAnswersResult?.BuildingHasSafetyRegulatorRegistrationCode,
            BuildingSafetyRegulatorRegistrationCode = checkMyAnswersResult?.BuildingSafetyRegulatorRegistrationCode,
            LeadDesignerNotAppointedReason = checkMyAnswersResult?.LeadDesignerNotAppointedReason,
            OtherMembersNotAppointedReason = checkMyAnswersResult?.OtherMembersNotAppointedReason,
            ReasonPlanningPermissionNotApplied = checkMyAnswersResult?.ReasonPlanningPermissionNotApplied,
            IntentToProceedType = checkMyAnswersResult?.IntentToProceedTypeId,
            WhyYouHaveNotSoughtQuotes = checkMyAnswersResult?.WhyYouHaveNotSoughtQuotes,
            QuotesNotSoughtReason = checkMyAnswersResult?.QuotesNotSoughtReason,
            ExpectedWorksPackageSubmissionDate = checkMyAnswersResult?.ExpectedWorksPackageSubmissionDate,
            ExpectedStartDateOnSite = checkMyAnswersResult?.ExpectedStartDateOnSite,
            LeadDesignerNeedsSupport = checkMyAnswersResult?.LeadDesignerNeedsSupport,
            OtherMembersNeedsSupport = checkMyAnswersResult?.OtherMembersNeedsSupport,
            SupportNeededReason = checkMyAnswersResult?.SupportNeededReason,
            PlanningPermissionNeedsSupport = checkMyAnswersResult?.PlanningPermissionNeedsSupport,
            QuotesNeedsSupport = checkMyAnswersResult?.QuotesNeedsSupport,
            TeamMembers = teamMembers,
			BuildingControlDetailsRequired = checkMyAnswersResult?.BuildingControlRequired,
            HasAppliedForBuildingControl = checkMyAnswersResult?.HasAppliedForBuildingControl,
			BuildingControlActualDate = checkMyAnswersResult?.BuildingControlActualSubmissionDate,
            BuildingControlActualInformation = checkMyAnswersResult?.BuildingControlActualSubmissionInformation,
            BuildingControlApplicationReference = checkMyAnswersResult?.BuildingControlApplicationReference,
            BuildingControlDecisionDate = checkMyAnswersResult?.BuildingControlDecisionDate,
			BuildingControlDecisionInformation = checkMyAnswersResult?.BuildingControlDecisionInformation,
			BuildingControlDecision = checkMyAnswersResult?.BuildingControlDecision,
			BuildingControlForecastDate = checkMyAnswersResult?.BuildingControlForecastSubmissionDate,
            BuildingControlForecastInformation = checkMyAnswersResult?.BuildingControlForecastInformation,
            BuildingControlValidationDate = checkMyAnswersResult?.BuildingControlValidationDate,
            BuildingControlValidationInformation = checkMyAnswersResult?.BuildingControlValidationInformation,
            HasGco = gco?.HasGco,
            TeamMember = gco?.TeamMember,
            Role = gco?.Role,
            NameNumber = gco?.NameNumber,
            AddressLine1 = gco?.AddressLine1,
            AddressLine2 = gco?.AddressLine2,
            City = gco?.City,
            County = gco?.County,
            Postcode = gco?.Postcode,
            Signatory = gco?.Signatory,
            SignatoryEmailAddress = gco?.SignatoryEmailAddress,
            DateAppointed = gco?.DateAppointed
        };
    }
}
