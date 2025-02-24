using System.Data;
using Dapper;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories;

public class SupportTicketRepository : ISupportTicketRepository
{
    private readonly IDbConnectionWrapper _connection;

    public SupportTicketRepository(IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task<Guid> InsertSupportTicket(InsertSupportTicketParameters parameters)
    {
        var @params = new DynamicParameters();
        @params.Add("@ApplicationId", parameters.ApplicationId);
        @params.Add("@SupportTicketTypeId", parameters.SupportTicketTypeId);
        @params.Add("@Description", parameters.Description);
        @params.Add("@SupportTicketId", dbType: DbType.Guid, direction: ParameterDirection.Output);

        await _connection.ExecuteAsync(nameof(InsertSupportTicket), @params);

        var id = @params.Get<Guid>("@SupportTicketId");

        return id;
    }

    public async Task InsertSupportTicketArea(InsertSupportTicketAreaParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(InsertSupportTicketArea), parameters);
    }
}