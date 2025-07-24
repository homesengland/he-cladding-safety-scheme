using System.Text.Json;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories
{
    public class DataIngestionRepository(IDbConnectionWrapper connection) : IDataIngestionRepository
    {
        private readonly IDbConnectionWrapper _connection = connection;

        public async Task UpdateUnProcessedRowWithErrorDetails(Guid rowId, Dictionary<string, string> errors)
        {
            var serializedErrors = JsonSerializer.Serialize(new { Errors = errors} );

            await _connection.ExecuteAsync(nameof(UpdateUnProcessedRowWithErrorDetails), new { Id = rowId, ErrorJson = serializedErrors });

        }
        public async Task DeleteUnProcessedRow(Guid rowId)
        {
            await _connection.ExecuteAsync(nameof(DeleteUnProcessedRow), new { Id = rowId });
        }

        public async Task LinkImportToApplication(Guid dataIngestionId, Guid applicationId, string organisationRegistrationNumber)
        {
            await _connection.ExecuteAsync("InsertApplicationDataIngestionDetails", 
                new { DataIngestionId = dataIngestionId, ApplicationId = applicationId, OrganisationRegistrationNumber = organisationRegistrationNumber });
        }
    }
}
