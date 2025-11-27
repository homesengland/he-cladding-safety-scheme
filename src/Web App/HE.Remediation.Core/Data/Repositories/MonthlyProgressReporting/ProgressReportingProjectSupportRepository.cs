using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectSupport;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.ProjectSupport;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
public class ProgressReportingProjectSupportRepository : IProgressReportingProjectSupportRepository
{
    private readonly IDbConnectionWrapper _connection;

    public ProgressReportingProjectSupportRepository(
        IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task<GetProjectSupportDetailsResult> GetProjectSupportDetails(Guid applicationId, Guid projectReportId)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetProjectSupportDetailsResult>("GetProjectSupportDetails", new
        {
            ApplicationId = applicationId,
            ProgressReportId = projectReportId
        });
        return result;
    }

    public async Task SetProjectSupportDetails(SetProjectSupportParameters parameters)
    {
        await _connection.ExecuteAsync("SetProjectSupportDetails", parameters);
    }

    public async Task<GetProgressReportSupportTypeResult> GetProgressReportSupportType(GetProgressReportSupportTypeParameters parameters)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetProgressReportSupportTypeResult>(nameof(GetProgressReportSupportType), parameters);
        return result;
    }

    public async Task SetProgressReportSupportType(SetProjectSupportTypeParameters parameters)
    {
        await _connection.ExecuteAsync("SetProgressReportSupportType", parameters);
    }

    public async Task<GetProjectSupportCheckYourAnswersResult> GetProjectSupportCheckYourAnswers(GetProjectSupportCheckYourAnswersParameters parameters)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetProjectSupportCheckYourAnswersResult>(nameof(GetProjectSupportCheckYourAnswers), parameters);
        return result;
    }

    public async Task SetProjectSupportCheckYourAnswers(SetProjectSupportCheckYourAnswersParameters parameters)
    {
        await _connection.ExecuteAsync("SetProjectSupportCheckYourAnswers", parameters);
    }
}
