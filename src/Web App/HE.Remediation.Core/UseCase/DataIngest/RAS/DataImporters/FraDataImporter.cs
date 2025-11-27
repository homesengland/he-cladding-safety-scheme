using Dapper;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.DataIngest.RAS.Lookups;
using System.Data;

namespace HE.Remediation.Core.UseCase.DataIngest.RAS.DataImporters;

public interface IFraDataImporter
{
    Task Process(ImportedDataRow importedDataRow, EApplicationScheme applicationScheme, Guid applicationId, DataIngestionLookupService.LookupData lookupData);
}

public class FraDataImporter(IDbConnectionWrapper db) : IFraDataImporter
{
    private readonly IDbConnectionWrapper _db = db;

    public async Task Process(ImportedDataRow importedDataRow, EApplicationScheme applicationScheme, Guid applicationId, DataIngestionLookupService.LookupData lookupData)
    {
        // Has_FRA

        await ExecuteWithIdentifierAsync("Has_FRA", async () =>
        {
            await _db.ExecuteAsync("CreateFra",
            new
            {
                ApplicationId = applicationId
            });

            await _db.ExecuteAsync("SetHasFra",
            new
            {
                ApplicationId = applicationId,
                HasFra = importedDataRow.HasFRA
            });

            await _db.ExecuteAsync("SetFraTaskStatus",
            new
            {
                ApplicationId = applicationId,
                TaskStatusId = (int)ETaskStatus.InProgress
            });
        });

        // FRA_Company_Name

        await ExecuteWithIdentifierAsync("FRA_Company_Name", async () =>
        {
            if (lookupData.FraFireRiskAssessorId != null)
            {
                await _db.ExecuteAsync("SetAssessorAndFraDate",
                    new
                    {
                        ApplicationId = applicationId,
                        AssessorId = lookupData.FraFireRiskAssessorId,
                        FraDate = (DateTime?)null
                    });
            }
        });

        // FRA_Outcome

        await ExecuteWithIdentifierAsync("FRA_Outcome", async () =>
        {
            if(importedDataRow.FRAOutcome != null)
            {
                await _db.ExecuteAsync("SetFireRiskRating",
                            new
                            {
                                ApplicationId = applicationId,
                                FireRiskRatingId = importedDataRow.FRAOutcome.Value,
                                HasInternalFireSafetyRisks = (bool?)null
                            });
            }
        });

        // FRA_Recommendations (always import as Other)

        await ExecuteWithIdentifierAsync("FRA_Recommendations", async () =>
        {
            if (importedDataRow.FRARecommendations != null)
            {
                var @params = new DynamicParameters();
                @params.Add("@ApplicationId", applicationId);
                @params.Add("@InternalFireSafetyDefectIds", new int[] { (int)EInternalFireSafetyDefect.Other }.Cast<int>().ToDataTable().AsTableValuedParameter("[dbo].[IntListType]"), DbType.Object);
                @params.Add("@OtherInternalFireSafetyRisk", importedDataRow.FRARecommendations);

                await _db.ExecuteAsync("SetInternalFireSafetyDefects", @params);
            }
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