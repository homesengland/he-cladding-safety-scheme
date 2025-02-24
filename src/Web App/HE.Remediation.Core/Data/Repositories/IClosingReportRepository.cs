using HE.Remediation.Core.Data.StoredProcedureParameters.ClosingReport;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.ClosingReport;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.Repositories;

public interface IClosingReportRepository
{
    Task<GetClosingReportConfirmationDetailsResult> GetClosingReportConfirmationDetails(Guid applicationId);
    Task<GetClosingReportDetailsResult> GetClosingReportDetails(Guid applicationId);
    Task<bool> IsClosingReportSubmitted(Guid applicationId);
    Task UpdateClosingReportToSubmitted(Guid applicationId);
    Task UpdateClosingReportProjectDate(Guid applicationId, DateTime? projectCompletionDate);
    Task UpdateClosingReportLifeSafetyRiskAssessment(Guid applicationId, ERiskType? lifeSafetyRiskAssessment);
    Task UpdateClosingReportConfirmation(Guid applicationId, ConfirmationParameters parameters);
    Task InsertFile(Guid applicationId, Guid fileId, EClosingReportFileType uploadType);
    Task DeleteFile(Guid fileId);
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

}