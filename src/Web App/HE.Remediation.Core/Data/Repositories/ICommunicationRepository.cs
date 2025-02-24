using HE.Remediation.Core.Data.StoredProcedureParameters;

namespace HE.Remediation.Core.Data.Repositories;

public interface ICommunicationRepository
{
    Task InsertCommunication(InsertCommunicationParameters parameters);
}