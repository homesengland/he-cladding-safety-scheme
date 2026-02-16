using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.CostsScheduling;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.Declaration;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.GrantCertifyingOfficer;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.InternalDefects;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.ThirdPartyContributions;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.ConfirmToProceed;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.CostsScheduling;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.Declaration;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.GrantCertifyingOfficer;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.InternalDefects;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.PlanningPermission;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.ProjectTeam;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.ThirdPartyContributions;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.WorkPackageDutyOfCareDeedResult;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.WorkPackageSignatories;
using HE.Remediation.Core.Enums;
using UpsertTeamMemberParameters = HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.ProjectTeam.UpsertTeamMemberParameters;

namespace HE.Remediation.Core.Data.Repositories;

public interface IWorkPackageRepository
{
    #region Confirm To Proceed
    Task<bool?> GetWorkPackageConfirmToProceed();

    Task UpdateWorkPackageConfirmToProceed(bool? isConfirmedToProceed);
    #endregion

    Task InsertWorkPackage(Guid applicationId); 
    
    Task<WorkPackageTaskListSummaryResult> GetWorkPackageTaskListSummary();

    Task<bool> HasWorkPackage();

    Task SubmitWorkPackage(Guid? userId);

    Task<bool> IsWorkPackageSubmitted();

    Task<bool> IsCladdingBeingRemoved();

    Task<bool> IsSignedUpToConsiderateConstructorsScheme(Guid applicationId);

    #region Grant funding officer

    Task<IReadOnlyCollection<GrantCertifyingOfficerCandidateResult>> GetGrantCertifyingOfficerCandidates();

    Task<GrantCertifyingOfficerAnswersResult> GetGrantCertifyingOfficerAnswers();

    Task<GrantCertifyingOfficerAddressResult> GetGrantCertifyingOfficerAddress();

    Task<GrantCertifyingOfficerDetailsResult> GetGrantCertifyingOfficerDetails();

    Task<GrantCertifyingOfficerIsCorrectPersonResult> GetGrantCertifyingOfficerIsCorrectPerson();

    Task<Guid?> GetGrantCertifyingOfficerProjectTeamMemberId();

    Task<bool> GetGrantCertifyingOfficerDutyOfCareDeedTaskRaised();

    Task<ETaskStatus?> GetGrantCertifyingOfficerStatus();

    Task InsertGrantCertifyingOfficer(InsertGrantCertifyingOfficerParameters parameters);

    Task UpdateGrantCertifyingOfficer(UpdateGrantCertifyingOfficerParameters parameters);

    Task UpdateGrantCertifyingOfficerDetails(UpdateGrantCertifyingOfficerParameters parameters);

    Task UpdateWorkPackageGrantCertifyingOfficerAddress(UpdateWorkPackageGrantCertifyingOfficerAddressParameters parameters);

    Task UpdateGrantCertifyingOfficerConfirmation(ECertifyingOfficerResponse certifyingOfficerResponse);

    Task UpdateGrantCertifyingOfficerDutyOfCareDeedTask(bool dutyOfCareDeedTaskRaised);

    Task UpdateGrantCertifyingOfficerStatus(ETaskStatus taskStatus);

    Task ResetGrantCertifyingOfficer();

    #endregion

    #region Planning Permission

    Task<WorkPackagePlanningPermissionResult> GetRequirePlanningPermission();

    Task<ETaskStatus?> GetWorkPackagePlanningPermissionStatus();

    Task InsertRequirePlanningPermission(bool? planningPermissionRequired, string planningPermissionNotRequiredReason);

    Task UpdateRequirePlanningPermission(bool? planningPermissionRequired, string planningPermissionNotRequiredReason);

    Task UpdateWorkPackagePlanningPermissionStatus(ETaskStatus taskStatus);

    Task ResetWorkPackagePlanningPermission();

    #endregion



    #region Key Dates

    Task<KeyDatesResult> GetKeyDates();

    Task<KeyDatesResult> GetLatestWorkPackageKeyDatesByApplication();

    Task<ETaskStatus?> GetKeyDatesStatus();

    Task InsertKeyDates(InsertKeyDatesParameters parameters);

    Task UpdateKeyDates(UpdateKeyDatesParameters parameters);

    Task UpdateKeyDatesStatus(ETaskStatus taskStatus);

    Task ResetKeyDates();

    #endregion

    #region Duty of Care Deed

    Task<ETaskStatus?> GetDutyOfCareDeedStatus();

    Task<DutyOfCareDeedResult> GetDutyOfCareDeed();

    Task InsertDutyOfCareDeed();

    Task UpdateDutyOfCareDeedStatus(ETaskStatus taskStatus);

    #endregion

    #region Declaration

    Task<DeclarationResult> GetDeclaration();

    Task<ETaskStatus?> GetDeclarationStatus();

    Task InsertDeclaration(InsertDeclarationParameters parameters);

    Task UpdateDeclaration(UpdateDeclarationParameters parameters);

    Task UpdateDeclarationStatus(ETaskStatus taskStatus);

    Task ResetDeclaration();

    #endregion

    #region Project Team

    Task InsertTeam();

    Task<ETaskStatus?> GetTeamStatus();

    Task UpdateTeamStatus(ETaskStatus taskStatus);

    Task<List<ProjectTeamMembersResult>> GetTeamMembers();

    Task<ProjectTeamMemberResult> GetTeamMember(Guid? teamMemberId);

    Task<Guid> UpsertTeamMember(UpsertTeamMemberParameters parameters);

    Task DeleteTeamMember(Guid teamMemberId);

    Task<Guid?> GetLatestTeamProgressReportId();

    Task CopyLatestTeamFromProgressReport(Guid? progressReportId);

    Task<Guid?> GetWorkPackageRegulatoryCompliance();

    Task UpdateWorkPackageRegulatoryCompliance(Guid teamMemberId);

    Task<GetRegulatoryComplianceTeamMemberResult> GetRegulatoryComplianceTeamMember();

    #endregion

    #region Cost Schedule

    Task InsertCostsSchedule();

    Task<ETaskStatus?> GetCostsScheduleStatus();

    Task UpdateCostsScheduleStatus(ETaskStatus taskStatus);

    Task UpdateWorkPackageCladdingSystemStatus(ETaskStatus taskStatus);

    Task<ENoYes?> GetCostsScheduleSoughtQuotes();

    Task UpdateCostsScheduleSoughtQuotes(ENoYes? soughtQuotes);

    Task<PreferredContractorLinksResult> GetCostsSchedulePreferredContractorLinks();
    Task UpdateCostSchedulePReferredContractorLinks(EYesNoNonBoolean? preferredContractorLinks, string preferredContractorLinkAdditionalNotes);

    Task<string> GetCostsScheduleNoQuotesReason();

    Task UpdateCostsScheduleNoQuotesReason(string reason);

    Task<ENoYes?> GetCostsScheduleRequiresSubcontractors();

    Task UpdateCostsScheduleRequiresSubcontractors(ENoYes? requiresSubcontractors);

    Task<ENoYes?> GetCostsScheduleHasIneligibleCosts();

    Task UpdateCostsScheduleHasIneligibleCosts(ENoYes? hasIneligibleCosts);

    Task<CostsScheduleFireRiskCladdingWorksResult> GetCostsScheduleFireRiskCladdingWorks();

    Task<CostsScheduleCladdingSystemResult> GetCostsScheduleCladdingSystemIsBeingRemoved(Guid? fireRiskCladdingSystemsId);

    Task<CostsScheduleCladdingSystemDetailsResult> GetCostsScheduleCladdingSystemDetails(Guid? fireRiskCladdingSystemsId);

    Task<CostsScheduleCladdingSystemAnswersResult> GetCostsScheduleCladdingSystemAnswers(Guid? fireRiskCladdingSystemsId);

    Task InsertCostsScheduleCladdingSystem(InsertCladdingSystemParameters parameters);

    Task UpdateCostsScheduleCladdingSystemIsBeingRemoved(UpdateCladdingSystemParameters parameters);

    Task UpdateCostsScheduleCladdingSystemDetails(UpdateCladdingSystemDetailsParameters parameters);

    Task UpdateCostsScheduleCladdingSystemStatus(Guid fireRiskCladdingSystemsId, ETaskStatus taskStatus);

    Task ResetCladdingSystem(Guid fireRiskCladdingSystemsId);

    Task<GetWorkPackageCostsResult> GetWorkPackageCosts();

    Task<GetWorkPackageCostsResult> GetWorkPackageCostsDraft();

    Task CreateWorkPackageCosts();

    Task UpdateUnsafeCladdingCosts(UpdateUnsafeCladdingCostsParameters parameters);

    Task<List<CostsScheduleSubcontractorResult>> GetCostsScheduleSubcontractors();

    Task<CostsScheduleSubcontractorResult> GetCostsScheduleSubcontractor(Guid? subcontractorId);

    Task<Guid> UpsertCostsScheduleSubcontractor(UpsertCostsSchedulingSubcontractorParameters parameters);

    Task DeleteCostsScheduleSubcontractor(Guid subcontractorId);

    Task UpdateInstallationOfCladdingCosts(UpdateInstallationOfCladdingCostsParameters parameters);

    Task UpdatePreliminaryCosts(UpdatePreliminaryCostsParameters parameters);

    Task UpdateOtherCosts(UpdateOtherCostsParameters parameters);

    Task UpdateIneligibleCosts(UpdateIneligibleCostsParameters parameters);

    Task<List<CostScheduleCladdingSystemReplacementResult>> GetCladdingReplacementSystemsForApplication();

    Task<Guid?> GetWorkPackageCostsDraftByApplicationId();

    Task<GetWorkPackageCostsResult> GetWorkPackageCostsByVariationRequestId(Guid variationRequestId);

    Task<Guid?> GetLatestWorkPackageCostsId();
    Task<GetLatestCostScheduleResult> GetLatestCostSchedule(Guid applicationId);

    #endregion

    #region Third Party Contributions
    Task InsertThirdPartyContributions();

    Task<EThirdPartyContributionPursuitStatus?> GetThirdPartyContributionsPursuingThirdPartyContribution();

    Task UpdateThirdPartyContributionsPursuingThirdPartyContribution(EThirdPartyContributionPursuitStatus? thirdPartyContributionPursuitStatusTypeId);

    Task<ThirdPartyContributionResult> GetThirdPartyContributionsThirdPartyContribution();

    Task<List<EFundingStillPursuing>> GetThirdPartyContributionsThirdPartyContributionPursuingTypes();

    Task UpdateThirdPartyContributionsThirdPartyContribution(ThirdPartyContributionParameters parameters);

    Task UpdateThirdPartyContributionsStatus(ETaskStatus taskStatus);

    Task<CheckYourAnswersResult> GetWorkPackageThirdPartyContributionsCheckYourAnswers();

    Task ResetThirdPartyContributions();

    Task<ETaskStatus?> GetThirdPartyContributionsStatus();

    #endregion

    #region AuthorisedSignatories

    Task<GrantCertifyingOfficerAuthorisedSignatoriesResult> GetGrantCertifyingOfficerAuthorisedSignatories();

    Task UpdateGrantCertifyingOfficerAuthorisedSignatories(UpdateGrantCertifyingOfficerAuthorisedSignatoriesParameters parameters);

    #endregion

    #region Programme Plan

    Task CreateWorkPackageProgrammePlan(Guid applicationId);
    Task SetWorkPackageProgrammePlanTaskStatus(SetWorkPackageProgrammePlanTaskStatusParameters parameters);
    Task<bool?> GetHasProgrammePlan(Guid applicationId);
    Task SetHasProgrammePlan(SetHasProgrammePlanParameters parameters);
    Task<IReadOnlyCollection<FileResult>> GetProgrammePlanDocuments(Guid applicationId);
    Task InsertProgrammePlanDocument(InsertProgrammePlanDocumentParameters parameters);
    Task DeleteProgrammePlanDocument(DeleteProgrammePlanDocumentParameters parameters);
    Task<GetProgrammePlanCheckYourAnswersResult> GetProgrammePlanCheckYourAnswers(Guid applicationId);

    #endregion

    #region Internal Defects

    Task InsertInternalDefectsCost();
    Task<GetInternalDefectsCostResult> GetInternalDefectsCost();
    Task<ETaskStatus?> GetInternalDefectsStatus();
    Task UpdateInternalDefectsStatus(ETaskStatus taskStatus);
    Task SetInternalDefectsCost(SetInternalDefectsParameters parameters);

    #endregion

}