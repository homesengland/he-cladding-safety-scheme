using Auth0.AspNetCore.Authentication;
using Dapper;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories;

public class MilestoneRepository : IMilestoneRepository
{
    private readonly IDbConnectionWrapper _connection;

    public MilestoneRepository(IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task UpdateStartOnSiteMilestone(UpdateStartOnSiteMilestoneParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(UpdateStartOnSiteMilestone), parameters);
    }

    public async Task UpdatePracticalCompletionMilestone(UpdatePracticalCompletionMilestoneParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(UpdatePracticalCompletionMilestone), parameters);
    }

    public async Task<GetPracticalCompletionResult> GetPracticalCompletion(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<GetPracticalCompletionResult>(nameof(GetPracticalCompletion), parameters);
        return result;
    }

    public async Task<GetStartOnSiteResult> GetStartOnSite(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<GetStartOnSiteResult>(nameof(GetStartOnSite), parameters);
        return result;
    }

    public async Task SubmitPracticalCompletion(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        await _connection.ExecuteAsync(nameof(SubmitPracticalCompletion), parameters);
    }

    public async Task SubmitStartOnSite(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        await _connection.ExecuteAsync(nameof(SubmitStartOnSite), parameters);
    }
}