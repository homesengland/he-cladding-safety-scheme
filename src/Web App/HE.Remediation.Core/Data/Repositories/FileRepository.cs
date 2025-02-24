using System.Data;
using Dapper;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories;

public class FileRepository : IFileRepository
{
    private readonly IDbConnectionWrapper _connection;

    public FileRepository(IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task InsertFile(InsertFileParameters parameters)
    {
        await _connection.ExecuteAsync("InsertFile", parameters);
    }

    public async Task<DeleteFileResult> DeleteFile(Guid fileId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@FileId", fileId);
        parameters.Add("@UserId", (Guid?)null);
        parameters.Add("@Extension", dbType: DbType.StringFixedLength, size: 150, direction: ParameterDirection.Output);
        await _connection.ExecuteAsync("DeleteFile", parameters);

        var extension = parameters.Get<string>("@Extension");
        return new DeleteFileResult
        {
            Extension = extension
        };
    }
}