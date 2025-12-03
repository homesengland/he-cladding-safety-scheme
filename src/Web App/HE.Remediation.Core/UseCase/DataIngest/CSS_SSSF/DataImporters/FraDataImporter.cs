using Dapper;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.DataIngest.CSS_SSSF.Lookups;
using System.Data;

namespace HE.Remediation.Core.UseCase.DataIngest.CSS_SSSF.DataImporters;

public interface IFraDataImporter
{
    Task Process(ImportedDataRow importedDataRow, EApplicationScheme applicationScheme, Guid applicationId, DataIngestionLookupService.LookupData lookupData);
}

public class FraDataImporter(IDbConnectionWrapper db) : IFraDataImporter
{
    private readonly IDbConnectionWrapper _db = db;

    public async Task Process(ImportedDataRow importedDataRow, EApplicationScheme applicationScheme, Guid applicationId, DataIngestionLookupService.LookupData lookupData)
    {
        await ExecuteWithIdentifierAsync("Specialist_Assessment_Undertaken", async () =>
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
                HasFra = true
            });

            await _db.ExecuteAsync("SetFraTaskStatus",
            new
            {
                ApplicationId = applicationId,
                TaskStatusId = (int)ETaskStatus.InProgress
            });

            await _db.ExecuteAsync("InsertOrUpdateFireRiskCompletedStatus", new
            {
                ApplicationId = applicationId,
                IsAppraisalCompleted = importedDataRow.HasCompletedFireRiskAppraisal
            });
        });

        // NOTE: We do not have DateOfInstruction and SurveyDate (mandatory fields) so have been requested by HE to leave this out for now.

        //await ExecuteWithIdentifierAsync("FRAEW_Company_Name", async () =>
        //{
        //    if (lookupData.FireRiskAssessorId != null)
        //    {
        //        var defaultDate = DateTime.UtcNow.Date;
        //        await _db.ExecuteAsync("InsertOrUpdateAppraisalSurveyDetails",
        //            new { ApplicationId = applicationId, lookupData.FireRiskAssessorId, DateOfInstruction = defaultDate, SurveyDate = defaultDate });
        //    }
        //});

        await ExecuteWithIdentifierAsync("Defects_Related_To", async () =>
        {
            await _db.ExecuteAsync("SetExternalFireRiskWorksRequired",
                new { ApplicationId = applicationId, ExternalWorksRequired = importedDataRow.IsExternalWorks ? ENoYes.Yes : ENoYes.No });
            await _db.ExecuteAsync("SetInternalFireRiskWorksRequired",
                new { ApplicationId = applicationId, InternalWorksRequired = importedDataRow.IsInternalMitigationWorks ? ENoYes.Yes : ENoYes.No });
        });

        await ExecuteWithIdentifierAsync("Other_LCFS_Defect_Type", async () =>
        {
            if (importedDataRow.InternalElement != null)
            {
                var description = importedDataRow.InternalElement == EInternalFireSafetyDefect.Other 
                                                                        ? importedDataRow.InternalElementDescription : 
                                                                        string.Empty;

                var defectList = new List<EInternalFireSafetyDefect>() { importedDataRow.InternalElement.Value };
                var tvp = defectList.Cast<int>().ToDataTable().AsTableValuedParameter("[dbo].[IntListType]");

                await _db.ExecuteAsync("SetInternalFireSafetyDefects", new
                {
                    ApplicationId = applicationId,
                    InternalFireSafetyDefectIds = tvp,
                    OtherInternalFireSafetyRisk = description
                });
            }
        });

        await ExecuteWithIdentifierAsync("FRAEW_Risk_Level/FRAEW_Recommendation", async () =>
        {
            if (importedDataRow.RiskLevel != null || importedDataRow.CladdingReplacement != null)
            {
                var p = new DynamicParameters();
                p.Add("ApplicationId", applicationId);
                p.Add("LifeSafetyRiskAssessment", importedDataRow.RiskLevel);
                p.Add("RecommendCladding", importedDataRow.CladdingReplacement);
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

        await ExecuteWithIdentifierAsync("Latest_Assessment_Level", async () =>
        {
            await _db.ExecuteAsync("UpdateFireRiskAssessmentType",
              new
              {
                  ApplicationId = applicationId,
                  FireRiskAssessmentTypeId = (int?)importedDataRow.LatestAssessmentLevel
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