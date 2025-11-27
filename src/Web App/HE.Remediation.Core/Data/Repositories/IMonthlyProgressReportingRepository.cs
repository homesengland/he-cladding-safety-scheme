using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.Submission;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport;

namespace HE.Remediation.Core.Data.Repositories
{
    public interface IMonthlyProgressReportingRepository
    {
        Task<IReadOnlyCollection<ProgressReportResult>> GetProgressReports(Guid applicationId);
        Task<GetMonthlyProgressReportResult> GetMonthlyProgressReport(GetMonthlyProgressReportParameters parameters);
        Task SetMonthlyReportStatus(SetMonthlyReportStatusParameters parameters);
        Task<GetProgressReportDetailsResult> GetProgressReportDetails(Guid progressReportId);
        Task SetAsSubmitted(SetAsSubmittedParameters parameters);

        Task<GetProgressReportDataForTasksResult> GetProgressReportDataForTasks(GetProgressReportDataForTasksParameters parameters);
    }
}