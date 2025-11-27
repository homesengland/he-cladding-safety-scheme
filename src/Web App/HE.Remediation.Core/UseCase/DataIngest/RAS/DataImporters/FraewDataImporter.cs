using Dapper;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.DataIngest.RAS.Lookups;
using System.Data;

namespace HE.Remediation.Core.UseCase.DataIngest.RAS.DataImporters;

public interface IFraewDataImporter
{
    Task Process(ImportedDataRow importedDataRow, EApplicationScheme applicationScheme, Guid applicationId, DataIngestionLookupService.LookupData lookupData);
}

public class FraewDataImporter(IDbConnectionWrapper db) : IFraewDataImporter
{
    private readonly IDbConnectionWrapper _db = db;

    public async Task Process(ImportedDataRow importedDataRow, EApplicationScheme applicationScheme, Guid applicationId, DataIngestionLookupService.LookupData lookupData)
    {
        // Has_FRAEW
        await ExecuteWithIdentifierAsync("Has_FRAEW", async () =>
        {
            await _db.ExecuteAsync("InsertOrUpdateFireRiskCompletedStatus", new
            {
                ApplicationId = applicationId,
                IsAppraisalCompleted = importedDataRow.HasFRAEW
            });
        });

        // Building_Height_Metres
        // Building_Height_Storeys
        await ExecuteWithIdentifierAsync("Building_Height_Metres/Building_Height_Storeys", async () =>
        {
            if(importedDataRow.BuildingHeightInStoreys != null || importedDataRow.BuildingHeightInMetres != null)
            {
                await _db.ExecuteAsync("InsertOrUpdateFireRiskAssessmentReportDetails", new
                {
                    ApplicationId = applicationId,
                    NumberOfStoreys = importedDataRow.BuildingHeightInStoreys,
                    BuildingHeight = importedDataRow.BuildingHeightInMetres,
                    AuthorsName = (string)null, // not null string
                    PeerReviewPerson = (string)null, // not null string
                    FraewCost = (decimal?)null,
                    CompanyUndertakingReport = (string)null,
                    BasicComplexId = (int?)null
                });
            }
        });

        // FRAEW_Company_Name
        await ExecuteWithIdentifierAsync("FRAEW_Company_Name", async () =>
        {
            if (lookupData.FraewFireRiskAssessorId != null)
            {
                await _db.ExecuteAsync("InsertOrUpdateAppraisalSurveyDetails",
                    new { 
                        ApplicationId = applicationId,
                        FireRiskAssessorId = lookupData.FraewFireRiskAssessorId, 
                        DateOfInstruction = (DateTime?)null, // not null DateTime
                        SurveyDate = (DateTime?)null  // not null DateTime
                    });

                //TODO: IS this needed?
                //await _statusTransitionService.TransitionToInternalStatus(EApplicationInternalStatus.FraewInstructed, applicationIds: applicationId);
            }
        });

        // FRAEW_Risk_Level
        await ExecuteWithIdentifierAsync("FRAEW_Risk_Level", async () =>
        {
            if (importedDataRow.FRAEWRiskLevel != null) {

                var p = new DynamicParameters();
                p.Add("ApplicationId", applicationId);
                p.Add("LifeSafetyRiskAssessment", importedDataRow.FRAEWRiskLevel);
                p.Add("RecommendCladding", null);
                p.Add("RecommendBuildingIntetim", null);
                p.Add("CaveatsLimitations", null);
                p.Add("RemediationSummary", null);
                p.Add("JustifyRecommendation", null);
                p.Add("OtherInterimMeasuresText", null);
                p.Add("RiskMitigationOtherText", null);
                p.Add("OtherRiskMitigationOptionsConsidered", null);
                p.Add("RecommendedWorksId", dbType: DbType.Guid, direction: ParameterDirection.Output);

                await _db.ExecuteAsync("InsertOrUpdateApplicationFireRiskRecommendedWorksDetails", p);

            }
        });

        // Has_Interim_Measures/Evacuation_Strategy/Interim_Measure_Type
        await ExecuteWithIdentifierAsync("Has_Interim_Measures/Evacuation_Strategy/Interim_Measure_Type", async () =>
        {
            if(importedDataRow.HasInterimMeasures != null)
            {
                await _db.ExecuteAsync("UpsertInterimMeasures", new
                {
                    ApplicationId = applicationId,
                    BuildingInterimMeasures = importedDataRow.HasInterimMeasures.Value, 
                    BuildingInterimMeasuresText = (string)null,
                    EvacuationStrategyType = importedDataRow.EvacuationStrategy, // not null
                    EvacuationStrategyText = (string)null,
                    NumberOfStairwellsPrompt = (int?)null, // not null ENoYes.No
                    NumberOfStairwells = (string)null,
                    ExternalWallAndBalconiesPolicy = (int?)null, // not null EYesNoNonBoolean
                    FireAndResueAccessRestrictions = (int?)null, // not null EYesNoNonBoolean
                    FireAndResueAccessRestrictionsText = (string)null
                });

                var interimMeasuresId = await _db.QuerySingleOrDefaultAsync<Guid>("GetFireRiskAppraisalInterimMeasuresId", new { applicationId });

                var buildingInterimMeasureTypes = !importedDataRow.BuildingInterimMeasuresTypes.Any()
                    ? null
                    : string.Join(",", importedDataRow.BuildingInterimMeasuresTypes.Select(x => (int)x));

                var otherInterimMeasure = importedDataRow.BuildingInterimMeasuresTypes.Contains(EInterimMeasuresType.Other);

                if (interimMeasuresId != Guid.Empty)
                {
                    await _db.ExecuteAsync("InsertBuildingInterimMeasureTypes", new
                    {
                        interimMeasuresId,
                        buildingInterimMeasureTypes,
                        BuildingInterimMeasures = importedDataRow.HasInterimMeasures,
                        otherInterimMeasure
                    });
                }
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