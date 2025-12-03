using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.UseCase.DataIngest.CSS_SSSF;
using HE.Remediation.Core.UseCase.DataIngest.CSS_SSSF.DataImporters;
using HE.Remediation.Core.UseCase.DataIngest.CSS_SSSF.Lookups;
using HE.Remediation.Core.UseCase.DataIngest.CSS_SSSF.Validation;
using MediatR;
using Moq;
using static HE.Remediation.Core.UseCase.DataIngest.CSS_SSSF.Lookups.AddressResolver;

namespace HE.Remediation.Core.Tests.UseCase.DataIngest.CSS_SSSF
{
    public class DataIngestHandlerTests
    {
        public DataIngestHandlerTests()
        {
            var dbConnection = Environment.GetEnvironmentVariable("DB_CONNSTRING");
        }

        [Fact]
        public async Task CallHandler_IsInvoked()
        {
            var xlsColumns = new Dictionary<string, string>()
            {
                { "Filename", "00AB_M_Q3_202425" },
                { "Provider_Number", "00AB"},
                { "RP_Name", "London Borough of Barking and Dagenham"},
                { "Name_of_Organisation", "Barking and dagenham"},
                { "Building_Name", "Colne House (1-100)"},
                { "Address_Line_1", "103 Harts Lane" },
                { "Postcode", "IG11 8LS" },
                { "Dev_Self_Remediation_Contract", "No" },
                { "Developer", "Bouygues" },
                { "ID_MHCLG_Code", "NULL" },
                { "Unique_Property_Reference_Number", "900000606" },
                { "Height_Bracket", "18m\u002B" },
                { "Dwelling_Units_Responsible_For", "100" },
                { "Low_Cost_Rental_Accommodation", "82" },
                { "Leasehold", "18" },
                { "Other_Dwelling_Type", "0" },
                { "Assessment_Undertaken", "Yes" },
                { "Latest_Assessment_Level", "Type 1" },
                { "LCFS_and_EWS_Defects", "Yes" },
                { "Specialist_Assessment_Required", "No" },
                { "Specialist_Assessment_Undertaken", "No" },
                { "FRAEW_Risk_Level", "NULL" },
                { "FRAEW_Recommendation", "Don\u0027t Know" },
                { "FRAEW_Company_Name", "NULL" },
                { "Government_Funded_Schemes", "No" },
                { "CSS_Application", "No" },
                { "Application_Intention", "No" },
                { "ID_Building_Safety_Fund", "NULL" },
                { "Defects_Related_To", "External Wall System" },
                { "Other_LCFS_Defect_Type", "NULL" },
                { "Total_Cost_of_Works", "11500000" },
                { "EWS_Defect_Cost", "4500000" },
                { "Other_Defect_Cost", "NULL" },
                { "Includes_Decant_Fees", "No" },
                { "Includes_Legal_Fees", "No" },
                { "Status_of_Works", "Plans in Place" },
                { "EWS_Start_Date", "2025-07-01" },
                { "EWS_Completion_Date", "2029-12-01" },
                { "ACM_Defects", "No" },
                { "ACM_Status_of_Works", "NULL" },
                { "Historic_Assessment_Level", "Type 1" },
                { "Historical_LCFS_Defects", "Don\u0027t Know" },
                { "Historical_LCFS_Defect_Type", "Both" },
                { "Historical_LCFS_Defect_Remediation_Complete", "No" },
                { "Comments", "External wall defects were identified in 2020" },
                { "Social_ID_Unique", "SH_41" },
                { "Crossover_Flag", "0" },
                { "In_ACM", "0" },
                { "In_BSF", "0" },
                { "In_CSS", "0" },
                { "In_RAS", "0" },
                { "BSF_Building_ID", "BARK_DW16-S-1" },
                { "BSF_Aggregate_ID", "BARK_DW16-S" },
                { "BSF_Registration_ID", "BARK_DW16-S" },
                { "MHCLG_Code", "NULL" },
                { "Aggregate_MHCLG_Code", "NULL" },
                { "CSS_Application_ID", "NULL" },
                { "Developer_ID", "NULL" },
                { "Data_From_Survey", "survey 06" },
                { "In_Survey_06", "1" },
                { "In_Survey_05", "1" },
                { "In_Survey_04", "1" },
                { "In_Survey_03", "1" },
                { "RP_Red_Green_List", "NULL" },
                { "Selfreported_EWS_Flag", "Yes" },
                { "Selfreported_Other_Flag", "No" },
                { "Local_Authority", "London Borough of Barking and Dagenham" }
            };

            var validator = new JsonDataIngestMapperIValidator();

            var buildingDetailsDataImporter = new Mock<IBuildingDetailsDataImporter>().Object;
            var responsibleEntityDataImporter = new Mock<IResponsibleEntityDataImporter>().Object;
            var fraDataImporter = new Mock<IFraDataImporter>().Object;
            var repo = new Mock<IDataIngestionRepository>().Object;
            var lookupService = new Mock<IDataIngestionLookupService>();

            lookupService.SetReturnsDefault(
                Task.FromResult(new DataIngestionLookupService.LookupData
                {
                    ApplicantUserId = Guid.NewGuid(),
                    BuildingAddress = new AddressLookup
                    {
                        AddressLine1 = "123 Test St",
                        Postcode = "AB12 3CD"
                    },
                    LocalAuthorityCostCentreId = "000001",
                    FireRiskAssessorId = 1
                })
            );

            var handler = new DataIngestHandler(
                validator,
                lookupService.Object,
                buildingDetailsDataImporter,
                responsibleEntityDataImporter,
                fraDataImporter,
                repo
            );

            var request = new CreateImportRequest(xlsColumns, Guid.Empty, Guid.Empty, Enums.EApplicationScheme.SocialSector);
            var result = await handler.Handle(request, default);

            Assert.Equal(Unit.Value, result);
        }
    }
}
