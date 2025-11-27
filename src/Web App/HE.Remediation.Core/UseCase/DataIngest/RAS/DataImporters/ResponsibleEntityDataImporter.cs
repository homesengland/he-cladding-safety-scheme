using Dapper;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

namespace HE.Remediation.Core.UseCase.DataIngest.RAS.DataImporters;

public interface IResponsibleEntityDataImporter
{
    Task Process(ImportedDataRow importedDataRow, EApplicationScheme applicationScheme, Guid applicationId);
}

public class ResponsibleEntityDataImporter(IDbConnectionWrapper db) : IResponsibleEntityDataImporter
{
    private readonly IDbConnectionWrapper _db = db;

    public async Task Process(ImportedDataRow importedDataRow, EApplicationScheme applicationScheme, Guid applicationId)
    {
        await ExecuteWithIdentifierAsync("Responsible_Entity_Name/RE_Organisation_Type", async () =>
        {
            if(importedDataRow.ResponsibleEntityOrganisationType != null || importedDataRow.ResponsibleEntityName != null)
            {
                await _db.ExecuteAsync("InsertOrUpdateRepresentativeType", new
                {
                    ApplicationId = applicationId,
                    RepresentationTypeId = EApplicationRepresentationType.ResponsibleEntity
                });

                await _db.ExecuteAsync("UpdateResponsibleEntityRelation", new
                {
                    ApplicationId = applicationId,
                    ResponsibleEntityRelationId = importedDataRow.ResponsibleEntityOrganisationType
                });

                await _db.ExecuteAsync("SetResponsibleEntityOrganisationDetails", new
                {
                    ApplicationId = applicationId,
                    CompanyName = importedDataRow.ResponsibleEntityName,
                    RegistrationNumber = (string)null
                });

                await _db.ExecuteAsync("SetResponsibleEntityUkRegistered", new
                {
                    ApplicationId = applicationId,
                    UkRegistered = true
                });
            }
            
        });

        // Use Developer as Responsible Entity Representation Company
        await ExecuteWithIdentifierAsync("Developer", async () =>
        {
            await _db.ExecuteAsync("UpdateRepresentationEntityType", new
            {
                ApplicationId = applicationId,
                ResponsibleEntityTypeId = (int)EResponsibleEntityType.Company
            });

            var userDetails = await _db.QuerySingleOrDefaultAsync<GetRepresentationCompanyOrIndividualDetailsResponse>(
                "GetRepresentationCompanyOrIndividualDetails", new
                {
                    ApplicationId = applicationId
                });

            var p = new DynamicParameters();
            p.Add("ApplicationId", applicationId);
            p.Add("CompanyName", userDetails.CompanyName);
            p.Add("CompanyRegistration", (string)null);
            p.Add("FirstName", userDetails.FirstName);
            p.Add("LastName", userDetails.LastName);
            p.Add("EmailAddress", userDetails.EmailAddress);
            p.Add("ContactNumber", userDetails.ContactNumber);

            await _db.ExecuteAsync("UpdateRepresentationCompanyOrIndividualDetails", p);
        });

        //TODO: Is this needed for RAS?
        await ExecuteWithIdentifierAsync("LeaseholdUnits", async () =>
        {
            var hasOwners = (importedDataRow?.LeaseholdUnits ?? 0) > 0;
            await _db.ExecuteAsync("SetHasOwners", new
            {
                ApplicationId = applicationId,
                HasOwners = hasOwners,
                SharedOwnerCount = hasOwners ? importedDataRow.LeaseholdUnits : (int?)null
            });
        });

        //TODO: LowCostRentalHomesRegisteredProvider

        //TODO: CurrentFreeholder



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