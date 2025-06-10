using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.SummariseProgress;

namespace HE.Remediation.Core.Data.Repositories;

public interface IProgressReportingRepository
{
    void SetProgressReportId(Guid? progressReportId);

    Task<LatestProgressReportResult> GetLatestProgressReport(Guid? applicationId = null);

    Task<ProgressReportCheckMyAnswersResult> GetProgressReportCheckMyAnswers();

    Task<TeamMemberDetails> GetLeadDesignerCompanyDetails();

    Task UpdateLeaseholdersInformed(bool? leaseholdersInformed);

    Task InsertTeamMember(TeamMemberDetails companyDetails);

    Task UpdateTeamMember(TeamMemberDetails teamMemberDetails);

    Task<bool?> GetProgressReportLeaseholdersInformed();

    Task<bool?> GetProgressReportOtherMembersAppointed();

    Task UpdateOtherMembersAppointed(bool? otherMembersAppointed);

    Task<ProgressReportOtherMembersNotAppointedReasonResult> GetProgressReportOtherMembersNotAppointedReason();

    Task UpdateOtherMembersNotAppointedReason(string otherMembersNotAppointedReason, bool? otherMembersNeedsSupport);

    Task<string> GetProgressReportTeamMemberName(Guid teamMemberId);

    Task DeleteProgressReportTeamMember(Guid teamMemberId);

    Task<bool?> GetProgressReportQuotesSought();

    Task UpdateQuotesSought(bool? quotesSought);

    Task<ProgressReportQuotesNotSoughtReasonResult> GetProgressReportQuotesNotSoughtReason();

    Task UpdateQuotesNotSoughtReason(EWhyYouHaveNotSoughtQuotes? whyYouHaveNotSoughtQuotes, string quotesNotSoughtReason, bool? quotesNeedsSupport);

    Task<GetTeamMemberResult> GetTeamMember(Guid? teamMemberId);

    Task<Guid> UpsertTeamMember(UpsertTeamMemberParameters parameters);

    Task<EYesNoNonBoolean?> GetProgressReportRequirePlanningPermission();

    Task UpdateRequirePlanningPermission(EYesNoNonBoolean? planningPermissionRequired);

    Task<bool?> GetProgressReportAppliedForPlanningPermission();

    Task UpdateAppliedForPlanningPermission(bool? appliedForPlanningPermission);

    Task<ProgressReportPlanningPermissionNotAppliedReasonResult> GetProgressReportPlanningPermissionNotAppliedReason();

    Task UpdatePlanningPermissionNotAppliedReason(string planningPermissionNotAppliedReason, bool? planningPermissionNeedsSupport);

    Task<List<GetTeamMembersResult>> GetTeamMembers();

    Task<DateTime?> GetProgressReportExpectedWorksPackageSubmissionDate();

    Task<DateTime?> GetProgressReportExpectedStartDateOnSite();

    Task UpdateProgressReportExpectedWorksPackageSubmissionDate(DateTime? submissionDate);

    Task UpdateProgressReportExpectedStartDateOnSite(DateTime? expectedStartDateOnSite);

    Task<DateTime?> GetProgressReportPlanningPermissionPlannedSubmitDate();

    Task UpdateProgressReportPlanningPermissionPlannedSubmitDate(DateTime? planningPermissionPlannedSubmitDate);

    Task<ProgressReportPlanningPermissionDetailsResult> GetProgressReportPlanningPermissionDetails();

    Task UpdateProgressReportPlanningPermissionDetails(DateTime? planningPermissionSubmittedDate, DateTime? planningPermissionApprovedDate);

    Task<bool> GetProgressReportShowBuildingSafetyRegulatorRegistrationCode();

    Task<bool?> GetProgressReportBuildingHasSafetyRegulatorRegistrationCode();

    Task UpdateProgressReportingBuildingHasSafetyRegulatorRegistrationCode(bool? buildingHasSafetyRegulatorRegistrationCode);

    Task<string> GetProgressReportBuildingSafetyRegulatorRegistrationCode();

    Task UpdateProgressReportingBuildingSafetyRegulatorRegistrationCode(string buildingSafetyRegulatorRegistrationCode);

    Task<string> GetProgressReportSupportNeededReason();

    Task<bool?> GetProgressReportLeadDesignerNeedsSupport();

    Task<bool?> GetProgressReportOtherMembersNeedsSupport();

    Task<bool?> GetProgressReportQuotesNeedsSupport();

    Task<bool?> GetProgressReportPlanningPermissionNeedsSupport();

    Task UpdateProgressReportSupportNeededReason(string supportNeededReason);

    Task<DateTime?> GetProgressReportDateSubmitted();

    Task UpdateProgressReportDateSubmitted(DateTime? dateSubmitted, Guid? userId);

    Task<GetProgressReportResult> GetProgressReportDetails(Guid applicationId, Guid progressReportId);

    Task<IReadOnlyCollection<FileResult>> GetProgressReportLeaseholdersInformedFiles();

    Task UpdateProgressReportLeaseholdersInformedFileId(Guid? fileId);

    Task<IReadOnlyCollection<ProgressReportResult>> GetProgressReports();

    Task<bool> HasSubmittedProgressReports();

    Task DeleteLeadDesignerNotAppointedReason();

    Task DeleteLeadDesignerForCurrentProgressReport();

    Task<bool> IsProgressReportSubmitted(Guid applicationId, Guid progressReportId);

    Task ResetProgressReport();

    Task<int> GetProgressReportVersion();

    Task<GetProgressReportAnswersResult> GetProgressReportAnswers();

    Task<DateTime?> GetProgressReportLeaseholdersInformedLastDate();

    Task UpdateLeaseholdersInformedLastDate(DateTime? leaseholderInformedLastDate);

    Task<GetProgressReportProgressSummaryResult> GetProgressReportProgressSummary();
    Task<GetProgressReportProgressSummaryResult> GetProgressReportProgressSummary(Guid progressReportId);

    Task UpdateSummariseProgress(SetSummariseProgressRequest request);
    Task<GetProgressReportSupportResult> GetProgressReportSupport();
    Task UpdateProgressReportSupport(UpdateProgressReportSupportParameters sprocParameters);

    Task<ProgressReportSecondaryCheckMyAnswersResult> GetProgressReportSecondaryCheckMyAnswers();
    Task<GetProgressReportSupportNeedsResult> GetProgressReportSupportNeeds();

    Task<bool?> GetLastSubmittedProgressReportOtherMembersAppointed();

    Task<int?> GetLastSubmittedProgressReportRequirePlanningPermission();

    Task<List<GetTeamMembersResult>> GetLastSubmittedProgressReportTeamMembers();

    Task<bool?> GetBuildingControlRequired();

    Task UpdateBuildingControlRequired(bool buildingControlRequired);
    Task<bool?> GetHasGrantCertifyingOfficer();
    Task UpdateHasGrantCertifyingOfficer(bool hasGco);
    Task<Guid?> GetGrantCertifyingOfficerTeamMember();
    Task<IReadOnlyCollection<GetProjectManagersAndQuantitySurveyorsResult>> GetProjectManagersAndQuantitySurveyors();
    Task UpdateGrantCertifyingOfficerTeamMember(Guid projectTeamMemberId);
    Task<GetGcoDetailsResult> GetGcoDetails();
    Task UpdateGrantCertifiyingOfficerResponse(ECertifyingOfficerResponse certifyingOfficerResponse);
    Task UpdateGrantCertifyingOfficerDetails();
    Task<GetGrantCertifyingOfficerAddressResult> GetGrantCertifyingOfficerAddress();
    Task UpdateGrantCertifyingOfficerAddress(UpdateGrantCertifyingOfficerAddressParameters parameters);
    Task<GetGrantCertifyingOfficerSignatoryResult> GetGrantCertifyingOfficerSignatory();
    Task UpdateGrantCertifyingOfficerSignatory(UpdateGrantCertifyingOfficerSignatoryParameters parameters);
    Task<GetGrantCerifyingOfficerAnswersResult> GetGrantCerifyingOfficerAnswers();
    Task<bool> IsGrantCertifyingOfficerComplete();
    Task SetDutyOfCareDeedTaskRaised(bool taskRaised);
    Task<bool> GetDutyOfCareDeedTaskRaised();
    Task<EIntentToProceedType?> GetIntentToProceedType(GetIntentToProceedTypeParameters parameters);
    Task UpdateIntentToProceedType(UpdateIntentToProceedTypeParameters parameters);
    Task<bool?> GetProgressReportHasProjectPlanMilestones();
    Task UpdateProgressReportHasProjectPlanMilestones(bool? hasProjectPlanMilestones);
    Task RemoveProgressReportLeaseholderInformationDocument(RemoveProgressReportLeaseholderInformationDocumentParameters parameters);

    Task<GetBuildingControlDecisionResult> GetBuildingControlDecision(GetBuildingControlDecisionParameters parameters);
    Task UpdateBuildingControlDecision(UpdateBuildingControlDecisionParameters parameters);
    Task<GetBuildingControlForecastResult> GetBuildingControlForecast(GetBuildingControlForecastParameters parameters);
    Task UpdateBuildingControlForecast(UpdateBuildingControlForecastParameters parameters);
    Task<GetBuildingControlSubmissionResult> GetBuildingControlSubmission(GetBuildingControlSubmissionParameters parameters);
    Task UpdateBuildingControlSubmission(UpdateBuildingControlSubmissionParameters parameters);
    Task<GetBuildingControlValidationResult> GetBuildingControlValidation(GetBuildingControlValidationParameters parameters);
    Task UpdateBuildingControlValidation(UpdateBuildingControlValidationParameters parameters);
    Task<GetHasAppliedForBuildingControlResult> GetHasAppliedForBuildingControl(GetHasAppliedForBuildingControlParameters parameters);
    Task UpdateHasAppliedForBuildingControl(UpdateHasAppliedForBuildingControlParameters parameters);
}
