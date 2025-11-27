using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.DataIngest.RAS.Lookups;

namespace HE.Remediation.Core.UseCase.DataIngest.RAS.DataImporters;

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
            if(!string.IsNullOrEmpty(importedDataRow.BuildingName))
            {
                var buildingDetailsId = Guid.NewGuid();
                await _db.ExecuteAsync("InsertBuildingUniqueName", new { BuildingDetailsId = buildingDetailsId, UniqueName = importedDataRow.BuildingName });
                await _db.ExecuteAsync("UpdateBuildingDetailsId", new { ApplicationId = applicationId, BuildingDetailsId = buildingDetailsId });
            }
        });

        await ExecuteWithIdentifierAsync(nameof(importedDataRow.PostCode), async () =>
        {
            var addressId = Guid.NewGuid();
            await _db.ExecuteAsync("InsertBuildingAddress",
                            new
                            {
                                AddressId = addressId,
                                NameNumber = importedDataRow.BuildingName,
                                AddressLine1 = importedDataRow.AddressLine1 ?? string.Empty,
                                AddressLine2 = (string)null,
                                City = importedDataRow.TownCity ?? string.Empty,
                                County = (string)null,
                                Postcode = importedDataRow.PostCode,
                                LocalAuthority = importedDataRow.LocalAuthority,
                                SubBuildingName = (string)null,
                                BuildingName = importedDataRow.BuildingName,
                                BuildingNumber = (string)null,
                                Street = (string)null,
                                Town = (string)null,
                                AdminArea = (string)null,
                                UPRN = (string)null,
                                AddressLines = (string)null,
                                XCoordinate = (string)null,
                                YCoordinate = (string)null,
                                Toid = (string)null,
                                BuildingType = (string)null
                            });
            await _db.ExecuteAsync("UpdateBuildingDetailsAddressId", new { ApplicationId = applicationId, addressId });
 
        });

        await ExecuteWithIdentifierAsync(nameof(importedDataRow.LocalAuthority), async () =>
        {
            if(!string.IsNullOrEmpty(importedDataRow.LocalAuthority) && !string.IsNullOrEmpty(lookupData.LocalAuthorityCostCentreId))
            {
                await _db.ExecuteAsync("UpdateLocalAuthority",
                             new
                             {
                                 ApplicationId = applicationId,
                                 lookupData.LocalAuthorityCostCentreId
                             });
            }

        });

        await ExecuteWithIdentifierAsync(nameof(importedDataRow.BuildingHeightInStoreys), async () =>
        {
            if (importedDataRow.BuildingHeightInStoreys != null)
            {
                await _db.ExecuteAsync("UpdateBuildingHeight", new
                {
                    ApplicationId = applicationId,
                    NumberOfStoreys = importedDataRow.BuildingHeightInStoreys
                });
            }
        });

        await ExecuteWithIdentifierAsync(nameof(importedDataRow.DevelopmentName), async () =>
        {
            var isPartOfDevelopment = string.IsNullOrEmpty(importedDataRow.DevelopmentName) ? ENoYes.No : ENoYes.Yes;
            await _db.ExecuteAsync("UpdateBuildingPartOfDevelopment",
                 new
                 {
                     ApplicationId = applicationId,
                     PartOfDevelopment = ENoYes.Yes
                 }
             );

            if(isPartOfDevelopment == ENoYes.Yes)
            {
                await _db.ExecuteAsync("UpdateNameOfDevelopment",
                        new
                        {
                            ApplicationId = applicationId,
                            NameOfDevelopment = importedDataRow.DevelopmentName
                        }
                );
            }
        });

        await ExecuteWithIdentifierAsync(nameof(importedDataRow.DwellingUnitsTotal), async () =>
        {
            if (importedDataRow.DwellingUnitsTotal != null)
            {
                await _db.ExecuteAsync("UpdateResidentialUnits",
                       new
                       {
                           ApplicationId = applicationId,
                           ResidentialUnitsCount = importedDataRow.DwellingUnitsTotal,
                           NonResidentialUnits = (importedDataRow?.NonResidentialUnits ?? 0) > 0 // TODO: Or use Leasehold_Units?
                       });
            }
        });

        await ExecuteWithIdentifierAsync(nameof(importedDataRow.NonResidentialUnits), async () =>
        {
            if (importedDataRow.NonResidentialUnits != null)
            {
                await _db.ExecuteAsync("UpdateNonResidentialUnits",
                            new
                            {
                                ApplicationId = applicationId,
                                NonResidentialUnitsCount = importedDataRow.NonResidentialUnits
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