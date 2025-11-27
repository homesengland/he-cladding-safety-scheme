using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.ProjectPlan;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;

public class ProgressReportingProjectPlanRepository : IProgressReportingProjectPlanRepository
{
    private readonly IDbConnectionWrapper _connection;

    public ProgressReportingProjectPlanRepository(IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task InsertPtsUpliftDocument(InsertPtsUpliftDocumentParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(InsertPtsUpliftDocument), parameters);
    }

    public async Task<IReadOnlyCollection<FileResult>> GetPtsUpliftDocument(GetPtsUpliftDocumentParameters parameters)
    {
        var results = await _connection.QueryAsync<FileResult>(nameof(GetPtsUpliftDocument), parameters);
        return results;
    }

    public async Task DeletePtsUpliftDocument(DeletePtsUpliftDocumentParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(DeletePtsUpliftDocument), parameters);
    }

    public async Task<GetProjectPlanCheckYourAnswersResult> GetProjectPlanCheckYourAnswers(GetProjectPlanCheckYourAnswersParameters parameters)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetProjectPlanCheckYourAnswersResult>(nameof(GetProjectPlanCheckYourAnswers), parameters);
        return result;
    }

    public async Task SetProjectPlanTaskStatus(SetProjectPlanTaskStatusParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetProjectPlanTaskStatus), parameters);
    }

    public async Task DeleteMonthlyProgressReportUploadProjectPlanFile(MonthlyProgressReportDeleteUploadProjectPlanParameters parameters)
    {
        await _connection.ExecuteAsync("DeleteMonthlyProgressReportUploadProjectPlanFile", parameters);
    }

    public async Task<GetProjectPlanDetailsResult> GetProjectPlanDetails(Guid applicationId, Guid projectReportId)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetProjectPlanDetailsResult>("GetProjectPlanDetails", new
        {
            ApplicationId = applicationId,
            ProgressReportId = projectReportId
        });
        return result;
    }

    public async Task<GetMonthlyReportProjectPlanDocumentsResult> GetMonthlyReportProjectPlanDocuments(GetMonthlyReportProjectPlanDocumentsParameters parameters)
    {
        GetMonthlyReportProjectPlanDocumentsResult result = null;

        await _connection.QueryAsync<GetMonthlyReportProjectPlanDocumentsResult, FileResult, GetMonthlyReportProjectPlanDocumentsResult>(
            nameof(GetMonthlyReportProjectPlanDocuments),
            (details, file) =>
            {
                result ??= details;

                if (file is not null && result.ProjectPlanDocuments.All(x => x.Id != file.Id))
                {
                    result.ProjectPlanDocuments.Add(file);
                }

                return result;
            },
            parameters);

        return result;
    }

    public async Task SetMonthlyProgressReportProjectPlanCheckYourAnswers(SetMonthlyProgressReportProjectPlanCheckYourAnswersParameters parameters)
    {
        await _connection.ExecuteAsync("SetMonthlyProgressReportProjectPlanCheckYourAnswers", parameters);
    }

    public async Task SetProjectPlanDetails(SetProjectPlanParameters parameters)
    {
        await _connection.ExecuteAsync("SetProjectPlanDetails", parameters);
    }

    public async Task InsertProgressReportProjectPlanFile(InsertProgressReportProjectPlanFileParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(InsertProgressReportProjectPlanFile), parameters);
    }
}