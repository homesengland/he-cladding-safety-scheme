using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories;

public class CommunicationRepository : ICommunicationRepository
{
    private readonly IDbConnectionWrapper _connection;

    public CommunicationRepository(IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task InsertCommunication(InsertCommunicationParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(InsertCommunication), parameters);
    }
}