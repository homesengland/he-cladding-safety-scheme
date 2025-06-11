using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories;

public class RightToManageRepository : IRightToManageRepository
{
    private readonly IDbConnectionWrapper _connection;

    public RightToManageRepository(IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task CreateRightToManageIfNotExists(Guid applicationId)
    {
        await _connection.ExecuteAsync(nameof(CreateRightToManageIfNotExists), new { ApplicationId = applicationId });
    }

    public async Task<bool?> GetHasAcquiredRightToManage(Guid applicationId)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<bool?>(nameof(GetHasAcquiredRightToManage), new
        {
            ApplicationId = applicationId
        });

        return result;
    }

    public async Task UpdateHasAcquiredRightToManage(UpdateHasAcquiredRightToManageParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(UpdateHasAcquiredRightToManage), parameters);
    }

    public async Task<DateTime?> GetRightToManageAcquisition(Guid applicationId)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<DateTime?>(nameof(GetRightToManageAcquisition), new
        {
            ApplicationId = applicationId
        });

        return result;
    }

    public async Task UpdateRightToManageAcquisition(UpdateRightToManageAcquisitionParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(UpdateRightToManageAcquisition), parameters);
    }

    public async Task<IReadOnlyCollection<FileResult>> GetRightToManageEvidence(Guid applicationId)
    {
        var results = await _connection.QueryAsync<FileResult>(nameof(GetRightToManageEvidence), new
        {
            ApplicationId = applicationId
        });

        return results;
    }

    public async Task AddRightToManageEvidence(AddRightToManageEvidenceParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(AddRightToManageEvidence), parameters);
    }

    public async Task DeleteRightToManageEvidence(DeleteRightToManageEvidenceParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(DeleteRightToManageEvidence), parameters);
    }
}