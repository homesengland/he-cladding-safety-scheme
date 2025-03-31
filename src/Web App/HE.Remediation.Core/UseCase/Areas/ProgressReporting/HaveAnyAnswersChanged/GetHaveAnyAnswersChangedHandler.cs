using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.HaveAnyAnswersChanged;

public class GetHaveAnyAnswersChangedHandler : IRequestHandler<GetHaveAnyAnswersChangedRequest, GetHaveAnyAnswersChangedResponse>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetHaveAnyAnswersChangedHandler(
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

    public async Task<GetHaveAnyAnswersChangedResponse> Handle(GetHaveAnyAnswersChangedRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var answers = await _progressReportingRepository.GetProgressReportAnswers();

        var gcoAnswers = await _progressReportingRepository.GetGrantCerifyingOfficerAnswers();

        return answers is not null ?
            new GetHaveAnyAnswersChangedResponse
            {
                BuildingName = buildingName,
                ApplicationReferenceNumber = reference,
                HasSoughtQuotes = answers.QuotesSought,
                WhyYouHaveNotSoughtQuotes = answers.WhyYouHaveNotSoughtQuotes,
                QuotesNotSoughtReason = answers.QuotesNotSoughtReason,
                NeedsPlanningPermission = answers.RequirePlanningPermission,
                AppliedForPlanningPermission = answers.AppliedForPlanningPermission,
                PlanningPermissionSubmittedDate = answers.PlanningPermissionSubmittedDate,
                PlanningPermissionApprovedDate = answers.PlanningPermissionApprovedDate,
                PlanningPermissionPlannedSubmitDate = answers.PlanningPermissionPlannedSubmitDate,
                ReasonPlanningPermissionNotApplied = answers.ReasonPlanningPermissionNotApplied,
                PlanningPermissionNeedsSupport = answers.PlanningPermissionNeedsSupport,
                BuildingControlRequired = answers.BuildingControlRequired,
                PreviouslyShownBsr = answers.PreviouslyShownBsr,
                CurrentlyShowBsr = answers.CurrentlyShowBsr,
                BuildingHasSafetyRegulatorRegistrationCode = answers.BuildingHasSafetyRegulatorRegistrationCode,
                BuildingSafetyRegulatorRegistrationCode = answers.BuildingSafetyRegulatorRegistrationCode,
                HasAppliedForBuildingControl = answers.HasAppliedForBuildingControl,
                BuildingControlForecastSubmissionDate = answers.BuildingControlForecastSubmissionDate,
                BuildingControlForecastInformation = answers.BuildingControlForecastInformation,
                BuildingControlActualSubmissionDate = answers.BuildingControlActualSubmissionDate,
                BuildingControlActualSubmissionInformation = answers.BuildingControlActualSubmissionInformation,
                BuildingControlApplicationReference = answers.BuildingControlApplicationReference,
                BuildingControlDecisionDate = answers.BuildingControlDecisionDate,
                BuildingControlDecisionInformation = answers.BuildingControlDecisionInformation,
                BuildingControlValidationDate = answers.BuildingControlValidationDate,
                BuildingControlValidationInformation = answers.BuildingControlValidationInformation,
                BuildingControlDecision = answers.BuildingControlDecision,
                WorksPackageEstimateDate = answers.ExpectedWorksPackageSubmissionDate,
                ExpectedStartDateOnSite = answers.ExpectedStartDateOnSite,
                HasGco = gcoAnswers?.HasGco,
                TeamMember = gcoAnswers?.TeamMember,
                Role = gcoAnswers?.Role,
                NameNumber = gcoAnswers?.NameNumber,
                AddressLine1 = gcoAnswers?.AddressLine1,
                AddressLine2 = gcoAnswers?.AddressLine2,
                City = gcoAnswers?.City,
                County = gcoAnswers?.County,
                Postcode = gcoAnswers?.Postcode,
                Signatory = gcoAnswers?.Signatory,
                SignatoryEmailAddress = gcoAnswers?.SignatoryEmailAddress,
                DateAppointed = gcoAnswers?.DateAppointed,
                IntentToProceed = answers.IntentToProceedType,
                HasProjectPlanMilestones = answers.HasProjectPlanMilestones
            } :
            throw new EntityNotFoundException("Answers not found");
    }
}