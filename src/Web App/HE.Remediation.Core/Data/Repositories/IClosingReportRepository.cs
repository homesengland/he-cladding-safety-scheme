using HE.Remediation.Core.Data.StoredProcedureParameters.ClosingReport;
using HE.Remediation.Core.Data.StoredProcedureResults.ClosingReport;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails;
using FileResult = HE.Remediation.Core.Data.StoredProcedureResults.FileResult;

namespace HE.Remediation.Core.Data.Repositories;

public interface IClosingReportRepository
{
    Task<GetClosingReportConfirmationDetailsResult> GetClosingReportConfirmationDetails(Guid applicationId);
    Task<bool?> GetClosingReportNeedVariations(Guid applicationId);
    Task<GetClosingReportDetailsResult> GetClosingReportDetails(Guid applicationId);
    Task<bool> IsClosingReportSubmitted(Guid applicationId);
    Task UpdateClosingReportToSubmitted(Guid applicationId);
    Task UpdateClosingReportProjectDate(Guid applicationId, DateTime? projectCompletionDate);
    Task UpdateClosingReportLifeSafetyRiskAssessment(Guid applicationId, ERiskType? lifeSafetyRiskAssessment);
    Task UpdateClosingReportDeclarations(Guid applicationId, bool? fraewRiskToLifeReduced, bool? grantFundingObligations);
    Task UpdateClosingReportConfirmation(Guid applicationId, ConfirmationParameters parameters);
    Task UpdateClosingReportNeedVariations(Guid applicationId, bool? needVariations);
    Task InsertFile(Guid applicationId, Guid fileId, EClosingReportFileType uploadType);
    Task<GetEvidenceSubmissionUploadResponse> GetApplicationEvidenceOfThirdPartyContributionFile(Guid applicationId, EClosingReportFileType uploadType);
    Task<int> DeleteFile(Guid fileId);
    Task<IReadOnlyCollection<FileResult>> GetFiles(Guid applicationId, EClosingReportFileType uploadType);
    Task<Guid?> GetSubcontractorSurveyId(Guid applicationId);
    Task UpdateSubcontractorSurveyId(Guid applicationId, Guid subContractorSurveyId);
    Task<bool> GetApplicationReadyForClosingReport(Guid applicationId);
    Task CreateClosingReport(Guid applicationId);
    Task<IReadOnlyCollection<GetClosingReportCostProfileResult>> GetClosingReportCostProfile(Guid applicationId);
    Task UpdateFinalPaymentAmount(Guid costId, decimal? amount);
    Task<decimal> GetClosingReportRequestedAmount(Guid applicationId);
    Task UpdateClosingReportCostChanged(Guid applicationId, bool haveCostsChanged);
    Task<GetClosingReportReviewPaymentOverviewResult> GetClosingReportReviewPaymentOverview(Guid applicationId);
    Task UpdateClosingReportReasonForChange(Guid applicationId, string reasonForChange);
    Task<decimal> GetClosingReportScheduledAmount(Guid applicationId);
    Task<bool> GetSubContractorsRequired(Guid applicationId);
    Task<int> GetClosingReportProjectDuration(Guid applicationId);
    Task<Guid?> GetPaymentRequestIDForClosingReport(Guid applicationId);
    Task<decimal> GetClosingReportAllowedFinalPaymentAmount(Guid applicationId);
    Task<IReadOnlyCollection<ClosingReportTaskStatusResultItem>> GetClosingReportTaskStatus(Guid applicationId);
    Task UpsertClosingReportTaskStatus(Guid applicationId, EClosingReportTask closingReportTask, ETaskStatus taskStatus, bool allowRevert = false);
    Task UpdateClosingReportHasThirdPartyContributions(Guid applicationId, bool hasThirdPartyContributions);
    Task UpdateClosingReportReasonForNoContributions(Guid applicationId, string reasonForNoContributions);
    Task<EFireRiskAssessmentType?> GetExitFraewDocumentType(Guid applicationId);
    Task SetExitFraewDocumentType(Guid applicationId, EFireRiskAssessmentType? fireRiskAssessmentType);
}