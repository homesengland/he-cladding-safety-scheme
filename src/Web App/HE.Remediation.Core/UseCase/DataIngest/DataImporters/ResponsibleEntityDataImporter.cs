using Dapper;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.DataIngest.Lookups;
using System.Data;

namespace HE.Remediation.Core.UseCase.DataIngest.DataImporters
{
    public interface IResponsibleEntityDataImporter
    {
        Task Process(ImportedDataRow importedDataRow, EApplicationScheme applicationScheme, Guid applicationId, DataIngestionLookupService.LookupData lookupData);
    }

    public class ResponsibleEntityDataImporter(IDbConnectionWrapper db) : IResponsibleEntityDataImporter
    {
        private readonly IDbConnectionWrapper _db = db;

        public async Task Process(ImportedDataRow importedDataRow, EApplicationScheme applicationScheme, Guid applicationId, DataIngestionLookupService.LookupData lookupData)
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
            });

            await ExecuteWithIdentifierAsync("Leasehold", async () =>
            {
                await _db.ExecuteAsync("SetHasOwners", new
                {
                    ApplicationId = applicationId,
                    HasOwners = importedDataRow.IsLeaseHoldersOrSharedOwners,
                    SharedOwnerCount = importedDataRow.IsLeaseHoldersOrSharedOwners ? (int?)null : importedDataRow.HowManyLeaseholders
                });
            });

            //FRAEW

            await ExecuteWithIdentifierAsync("Specialist_Assessment_Undertaken", async () =>
            {
                await _db.ExecuteAsync("InsertOrUpdateFireRiskCompletedStatus", new
                {
                    ApplicationId = applicationId,
                    IsAppraisalCompleted = importedDataRow.HasCompletedFireRiskAppraisal
                });
            });

            await ExecuteWithIdentifierAsync("FRAEW_Company_Name", async () =>
            {
                if (lookupData.FireRiskAssessorId != null)
                {
                    await _db.ExecuteAsync("InsertOrUpdateAppraisalSurveyDetails",
                        new { ApplicationId = applicationId, lookupData.FireRiskAssessorId, DateOfInstruction = (DateTime?)null, SurveyDate = (DateTime?)null });
                }
            });

            await ExecuteWithIdentifierAsync("Defects_Related_To", async () =>
            {
                await _db.ExecuteAsync("SetExternalFireRiskWorksRequired",
                    new { ApplicationId = applicationId, ExternalWorksRequired = importedDataRow.IsExternalWorks ? ENoYes.Yes : ENoYes.No });
                await _db.ExecuteAsync("SetInternalFireRiskWorksRequired",
                    new { ApplicationId = applicationId, InternalWorksRequired = importedDataRow.IsInternalMitigationWorks ? ENoYes.Yes : ENoYes.No });
            });

            await ExecuteWithIdentifierAsync("Other_LCFS_Defect_Type", async () =>
            {
                if (!string.IsNullOrEmpty(importedDataRow.InternalElement))
                {
                    await _db.ExecuteAsync("InsertOrUpdateFireRiskWallWorks", new
                    {
                        ApplicationId = applicationId,
                        Description = importedDataRow.InternalElement,
                        WorkTypeId = EWorkType.Internal
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
}