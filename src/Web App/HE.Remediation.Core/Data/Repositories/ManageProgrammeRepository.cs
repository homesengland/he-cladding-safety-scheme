using Azure.Core;
using Dapper;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers;
using HE.Remediation.Core.UseCase.Areas.Application.ManageProgramme;
using static Dapper.SqlMapper;

namespace HE.Remediation.Core.Data.Repositories;

public class ManageProgrammeRepository : IManageProgrammeRepository
{
    private readonly IDbConnectionWrapper _dbConnection;

    public ManageProgrammeRepository(IDbConnectionWrapper dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<GetApplicationSummaryDetailsResult> GetApplicationSummaryDetails(Guid applicationId)
    {
        var applicationSummaryDetails = await _dbConnection.QuerySingleOrDefaultAsync<GetApplicationSummaryDetailsResult>(
            "GetApplicationSummaryDetails", new
            {
                ApplicationId = applicationId
            });

        return applicationSummaryDetails;
    }

    public Task<IReadOnlyCollection<GetManageProgrammeResponse>> GetManageProgrammeDetails(GetManageProgrammeRequest request, Guid? userId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("UserId", userId);
        parameters.Add("Search", string.IsNullOrWhiteSpace(request.Search) ? null : request.Search);

        var schemeFilter = request.SelectedSchemeTypeFilters.Select(x => (int)x).ToArray();
        parameters.Add("SchemeFilter", schemeFilter.ToDataTable().AsTableValuedParameter("[dbo].[IntListType]"));

        return _dbConnection.QueryAsync<GetManageProgrammeResponse>(nameof(GetManageProgrammeDetails), parameters);
    }

    public Task<IReadOnlyCollection<GetApplicationHeadlineResult>> GetApplicationHeadlines(string[] applicationIds)
    {
        var parameters = new DynamicParameters();
        parameters.Add("ApplicationIds", applicationIds.ToDataTable().AsTableValuedParameter("[dbo].[GuidListType]"));
        return _dbConnection.QueryAsync<GetApplicationHeadlineResult>(nameof(GetApplicationHeadlines), parameters);
    }

    public Task SaveManageProgrammeUpdates(SetApplicationUpdatesRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("EstimatedInvestigationCompletionDate", request.EstimatedInvestigationCompletionDate);
        parameters.Add("EstimatedStartOnSiteDate", request.EstimatedStartOnSiteDate);
        parameters.Add("EstimatedPracticalCompletionDate", request.EstimatedPracticalCompletionDate);
        parameters.Add("ApplicationIds", request.ApplicationIds.ToDataTable().AsTableValuedParameter("[dbo].[GuidListType]"));
        return _dbConnection.ExecuteAsync(nameof(SaveManageProgrammeUpdates), parameters);
    }
}