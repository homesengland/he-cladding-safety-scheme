using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.UseCase.DataIngest.CSS_SSSF.DataImporters;

public interface IResponsibleEntityDataImporter
{
    Task Process(ImportedDataRow importedDataRow, EApplicationScheme applicationScheme, Guid applicationId);
}

public class ResponsibleEntityDataImporter(IDbConnectionWrapper db) : IResponsibleEntityDataImporter
{
    private readonly IDbConnectionWrapper _db = db;

    public async Task Process(ImportedDataRow importedDataRow, EApplicationScheme applicationScheme, Guid applicationId)
    {
        await ExecuteWithIdentifierAsync("RP_Name/Provider_Number", async () =>
        {
            await _db.ExecuteAsync("InsertOrUpdateRepresentativeType", new
            {
                ApplicationId = applicationId,
                RepresentationTypeId = EApplicationRepresentationType.ResponsibleEntity
            });

            await _db.ExecuteAsync("SetResponsibleEntityOrganisationDetails",
                new { ApplicationId = applicationId, CompanyName = importedDataRow.OrganisationName, importedDataRow.RegistrationNumber });

            await _db.ExecuteAsync("UpdateResponsibleEntityCompanyType", new
            {
                ApplicationId = applicationId,
                OrganisationTypeId = (int?)importedDataRow.OrganisationType
            });

            await _db.ExecuteAsync("SetResponsibleEntityUkRegistered", new
            {
                ApplicationId = applicationId,
                UkRegistered = true
            });
        });

        await ExecuteWithIdentifierAsync("Leasehold", async () =>
        {
            await _db.ExecuteAsync("SetHasOwners", new
            {
                ApplicationId = applicationId,
                HasOwners = importedDataRow.IsLeaseHoldersOrSharedOwners,
                SharedOwnerCount = importedDataRow.IsLeaseHoldersOrSharedOwners ? importedDataRow.HowManyLeaseholders : (int?)null
            });
        });
    }

    private static async Task ExecuteWithIdentifierAsync(string columnName, Func<Task> databaseInserter)
    {
        try
        {
            await databaseInserter();
        }
        catch (DataImportException ex)
        {
            ex.ColumnName = columnName;
            throw;
        }
    }
}