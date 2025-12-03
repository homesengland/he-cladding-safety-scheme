using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.Submission;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport;
using HE.Remediation.Core.Interface;


namespace HE.Remediation.Core.Data.Repositories;

public class MonthlyProgressReportingRepository : IMonthlyProgressReportingRepository
{
    private readonly IDbConnectionWrapper _connection;

    public MonthlyProgressReportingRepository(
        IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task<IReadOnlyCollection<ProgressReportResult>> GetProgressReports(Guid applicationId)
    {
        return await _connection.QueryAsync<ProgressReportResult>("GetProgressReportsV2", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task<GetMonthlyProgressReportResult> GetMonthlyProgressReport(GetMonthlyProgressReportParameters parameters)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetMonthlyProgressReportResult>(nameof(GetMonthlyProgressReport), parameters);
        return result;
    }

    public async Task SetMonthlyReportStatus(SetMonthlyReportStatusParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetMonthlyReportStatus), parameters);
    }

    public async Task<GetProgressReportDetailsResult> GetProgressReportDetails(Guid progressReportId)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetProgressReportDetailsResult>("GetProgressReportDetailsV2", new { ProgressReportId = progressReportId });
        return result;
    }

    public async Task SetAsSubmitted(SetAsSubmittedParameters parameters)
    {
        await _connection.ExecuteAsync("SetMonthlyReportAsSubmitted", parameters);
    }

    public async Task<GetProgressReportDataForTasksResult> GetProgressReportDataForTasks(GetProgressReportDataForTasksParameters parameters)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetProgressReportDataForTasksResult>(nameof(GetProgressReportDataForTasks), parameters);
        return result;
    }
}
