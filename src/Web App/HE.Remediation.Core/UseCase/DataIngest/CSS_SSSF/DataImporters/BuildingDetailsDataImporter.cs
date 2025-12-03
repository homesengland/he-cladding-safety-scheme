using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.DataIngest.CSS_SSSF.Lookups;

namespace HE.Remediation.Core.UseCase.DataIngest.CSS_SSSF.DataImporters;

public interface IBuildingDetailsDataImporter
{
    Task<Guid> Process(ImportedDataRow importedDataRow, EApplicationScheme applicationScheme, Guid userId, DataIngestionLookupService.LookupData lookupData);
}

public class BuildingDetailsDataImporter(IDbConnectionWrapper db) : IBuildingDetailsDataImporter
{
    private readonly IDbConnectionWrapper _db = db;

    public async Task<Guid> Process(ImportedDataRow importedDataRow, EApplicationScheme applicationScheme, Guid userId, DataIngestionLookupService.LookupData lookupData)
    {
        Guid applicationId = Guid.Empty;

        await ExecuteWithIdentifierAsync(nameof(importedDataRow.BuildingName), async () =>
        {
            applicationId = await _db.QuerySingleOrDefaultAsync<Guid>("InsertApplication", new
            {
                UserId = userId,
                StatusId = EApplicationStatus.ApplicationInProgress,
                StageId = EApplicationStage.ApplyForGrant,
                SchemeId = (int)applicationScheme
            });
        });

        await ExecuteWithIdentifierAsync(nameof(importedDataRow.BuildingName), async () =>
        {
            var buildingDetailsId = Guid.NewGuid();
            await _db.ExecuteAsync("InsertBuildingUniqueName", new { BuildingDetailsId = buildingDetailsId, UniqueName = importedDataRow.BuildingName });
            await _db.ExecuteAsync("UpdateBuildingDetailsId", new { ApplicationId = applicationId, BuildingDetailsId = buildingDetailsId });
        });

        await ExecuteWithIdentifierAsync(nameof(importedDataRow.ResidentialUnitsCount), async () =>
        {
            await _db.ExecuteAsync("UpdateResidentialUnits",
                        new
                        {
                            ApplicationId = applicationId,
                            importedDataRow.ResidentialUnitsCount,
                            NonResidentialUnits = 0
                        });
        });

        await ExecuteWithIdentifierAsync(nameof(importedDataRow.PostCode), async () =>
        {
            var details = lookupData.BuildingAddress;

            await _db.ExecuteAsync("InsertBuildingAddress",
                            new
                            {
                                details.AddressId,
                                details.NameNumber,
                                details.AddressLine1,
                                details.AddressLine2,
                                details.City,
                                details.County,
                                details.Postcode,
                                details.LocalAuthority,
                                details.SubBuildingName,
                                details.BuildingName,
                                details.BuildingNumber,
                                details.Street,
                                details.Town,
                                details.AdminArea,
                                details.UPRN,
                                details.AddressLines,
                                details.XCoordinate,
                                details.YCoordinate,
                                details.Toid,
                                details.BuildingType
                            }
            );

            await _db.ExecuteAsync("UpdateBuildingDetailsAddressId", new { ApplicationId = applicationId, details.AddressId });
        });

        await ExecuteWithIdentifierAsync(nameof(importedDataRow.LocalAuthority), async () =>
        {
            await _db.ExecuteAsync("UpdateLocalAuthority",
                 new
                 {
                     ApplicationId = applicationId,
                     lookupData.LocalAuthorityCostCentreId
                 }
            );
        });

        await ExecuteWithIdentifierAsync(nameof(importedDataRow.NumberOfStoreys), async () =>
        {
            await _db.ExecuteAsync("UpdateBuildingHeight", new
            {
                ApplicationId = applicationId,
                importedDataRow.NumberOfStoreys
            });
        });

        await ExecuteWithIdentifierAsync(nameof(importedDataRow.KnowOriginalBuilder), async () =>
        {
            await _db.ExecuteAsync("SetBuildingOriginalDeveloperIsKnown", new
            {
                ApplicationId = applicationId,
                DoYouKnowOriginalDeveloper = importedDataRow.KnowOriginalBuilder
            });
        });

        await ExecuteWithIdentifierAsync(nameof(importedDataRow.OriginalBuilderName), async () =>
        {
            if (importedDataRow.KnowOriginalBuilder)
            {
                await _db.ExecuteAsync("UpdateBuildingDeveloperInformation",
                    new
                    {
                        ApplicationId = applicationId,
                        OrganisationName = importedDataRow.OriginalBuilderName,
                        NameNumber = string.Empty,
                        AddressLine1 = string.Empty,
                        AddressLine2 = string.Empty,
                        City = string.Empty,
                        County = string.Empty,
                        Postcode = string.Empty
                    });
            }
        });

        return applicationId;
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