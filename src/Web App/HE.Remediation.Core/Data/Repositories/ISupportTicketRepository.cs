using HE.Remediation.Core.Data.StoredProcedureParameters;

namespace HE.Remediation.Core.Data.Repositories;

public interface ISupportTicketRepository
{
    Task<Guid> InsertSupportTicket(InsertSupportTicketParameters parameters);

    Task InsertSupportTicketArea(InsertSupportTicketAreaParameters parameters);
}