namespace HE.Remediation.Core.Data.Repositories
{
    public interface IDataIngestionRepository
    {
        Task UpdateUnProcessedRowWithErrorDetails(Guid unProccessedId, Dictionary<string, string> errors);
        Task DeleteUnProcessedRow(Guid unProccessedId);
        Task LinkImportToApplication(Guid dataIngestionId, Guid applicationId, string organisationRegistrationNumber);
    }
}
