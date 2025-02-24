using Dapper;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly IDbConnectionWrapper _connection;

    public DocumentRepository(IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task<IReadOnlyCollection<GetApplicantDocumentsResult>> GetApplicantDocuments(GetApplicantDocumentsParameters parameters)
    {
        var results = await _connection.QueryAsync<GetApplicantDocumentsResult>(nameof(GetApplicantDocuments), parameters);
        return results;
    }

    public async Task<FileResult> GetApplicationFile(GetApplicationFileParameters parameters)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<FileResult>(nameof(GetApplicationFile), parameters);
        return result;
    }
}