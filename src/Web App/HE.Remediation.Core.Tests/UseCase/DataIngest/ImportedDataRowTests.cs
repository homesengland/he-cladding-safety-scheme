using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.DataIngest;
using HE.Remediation.Core.UseCase.DataIngest.Validation;

namespace HE.Remediation.Core.Tests.UseCase.DataIngest
{
    public class ImportedDataRowTests
    {
        [Theory]
        [InlineData("Building_Name")]
        [InlineData("Building Name")]
        [InlineData("Building name")]
        public void BuildingName_ReturnsExpectedValue(string columnName)
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { columnName, "Test Building" } });
            Assert.Equal("Test Building", mapper.BuildingName);

            mapper = new ImportedDataRow(new Dictionary<string, string>());
            Assert.Equal(string.Empty, mapper.BuildingName);
        }

        [Fact]
        public void AddressLine1_ReturnsExpectedValue()
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "Address_Line_1", "123 Main St" } });
            Assert.Equal("123 Main St", mapper.AddressLine1);

            mapper = new ImportedDataRow(new Dictionary<string, string>());
            Assert.Equal(string.Empty, mapper.AddressLine1);
        }

        [Fact]
        public void PostCode_ReturnsExpectedValue()
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "Postcode", "AB12 3CD" } });
            Assert.Equal("AB12 3CD", mapper.PostCode);

            mapper = new ImportedDataRow(new Dictionary<string, string>());
            Assert.Equal(string.Empty, mapper.PostCode);
        }

        [Fact]
        public void LocalAuthority_ReturnsExpectedValue()
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "Local_Authority", "Test Authority" } });
            Assert.Equal("Test Authority", mapper.LocalAuthority);

            mapper = new ImportedDataRow(new Dictionary<string, string>());
            Assert.Equal(string.Empty, mapper.LocalAuthority);
        }

        [Theory]
        [InlineData("123", 123)]
        [InlineData("12abc", 12)]
        [InlineData("abc", null)]
        [InlineData("", null)]
        [InlineData(null, null)]
        public void ResidentialUnitsCount_ParsesIntOrReturnsZero(string input, int? expected)
        {
            var dict = new Dictionary<string, string>();
            if (input != null) dict["Dwelling_Units_Responsible_For"] = input;
            var mapper = new ImportedDataRow(dict);
            Assert.Equal(expected, mapper.ResidentialUnitsCount);
        }

        [Fact]
        public void NumberOfStories_ReturnsExpectedValue()
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "Height_Bracket", "18m+" } });
            Assert.Equal(7, mapper.NumberOfStoreys);

            mapper = new ImportedDataRow(new Dictionary<string, string> { { "Height_Bracket", "11-18m" } });
            Assert.Equal(5, mapper.NumberOfStoreys);

            mapper = new ImportedDataRow(new Dictionary<string, string>());
            Assert.Null(mapper.NumberOfStoreys);
        }

        [Fact]
        public void OriginalBuilderName_ReturnsExpectedValue()
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "Developer", "Builder Inc" } });
            Assert.Equal("Builder Inc", mapper.OriginalBuilderName);

            mapper = new ImportedDataRow(new Dictionary<string, string>());
            Assert.Equal(string.Empty, mapper.OriginalBuilderName);
        }

        [Fact]
        public void KnowOriginalBuilder_ReturnsTrueIfDeveloperPresent()
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "Developer", "Builder Inc" } });
            Assert.True(mapper.KnowOriginalBuilder);

            mapper = new ImportedDataRow(new Dictionary<string, string> { { "Developer", "" } });
            Assert.False(mapper.KnowOriginalBuilder);

            mapper = new ImportedDataRow(new Dictionary<string, string>());
            Assert.False(mapper.KnowOriginalBuilder);
        }

        [Fact]
        public void OrganisationName_ReturnsExpectedValue()
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "RP_Name", "OrgName" } });
            Assert.Equal("OrgName", mapper.OrganisationName);

            mapper = new ImportedDataRow(new Dictionary<string, string>());
            Assert.Equal(string.Empty, mapper.OrganisationName);
        }

        [Theory]
        [InlineData("RP", EApplicationResponsibleEntityOrganisationType.RegisteredProvider)]
        [InlineData(" RP", EApplicationResponsibleEntityOrganisationType.RegisteredProvider)]
        [InlineData("LA", EApplicationResponsibleEntityOrganisationType.LocalAuthority)]
        [InlineData("ZZ", null)]
        public void OrganisationType_ReturnsExpectedValue(string input, EApplicationResponsibleEntityOrganisationType? expectedMapping)
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "RE organisation type", input } });
            Assert.Equal(expectedMapping, mapper.OrganisationType);

            mapper = new ImportedDataRow(new Dictionary<string, string>());
            Assert.Null(mapper.OrganisationType);
        }

        [Fact]
        public void RegistrationNumber_ReturnsExpectedValue()
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "Provider_Number", "12345" } });
            Assert.Equal("12345", mapper.RegistrationNumber);

            mapper = new ImportedDataRow(new Dictionary<string, string>());
            Assert.Equal(string.Empty, mapper.RegistrationNumber);
        }

        [Theory]
        [InlineData("10", 10)]
        [InlineData("10abc", 10)]
        [InlineData("abc", 0)]
        [InlineData("", 0)]
        [InlineData(null, 0)]
        public void HowManyLeaseholders_ParsesIntOrReturnsZero(string input, int expected)
        {
            var dict = new Dictionary<string, string>();
            if (input != null) dict["Leasehold"] = input;
            var mapper = new ImportedDataRow(dict);
            Assert.Equal(expected, mapper.HowManyLeaseholders);
        }

        [Fact]
        public void IsLeaseHoldersOrSharedOwners_ReturnsTrueIfLeaseholdersGreaterThanZero()
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "Leasehold", "5" } });
            Assert.True(mapper.IsLeaseHoldersOrSharedOwners);

            mapper = new ImportedDataRow(new Dictionary<string, string> { { "Leasehold", "abc" } });
            Assert.False(mapper.IsLeaseHoldersOrSharedOwners);

            mapper = new ImportedDataRow(new Dictionary<string, string>());
            Assert.False(mapper.IsLeaseHoldersOrSharedOwners);
        }

        [Fact]
        public void SpecialistAssessmentUndertaken_ReturnsTrueIfStartsWithYesAFRAEW()
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "Specialist_Assessment_Undertaken", "Yes - a FRAEW was done" } });
            Assert.True(mapper.HasCompletedFireRiskAppraisal);

            mapper = new ImportedDataRow(new Dictionary<string, string> { { "Specialist_Assessment_Undertaken", "No" } });
            Assert.False(mapper.HasCompletedFireRiskAppraisal);

            mapper = new ImportedDataRow(new Dictionary<string, string> { { "Specialist_Assessment_Undertaken", "NULL" } });
            Assert.False(mapper.HasCompletedFireRiskAppraisal);

            mapper = new ImportedDataRow(new Dictionary<string, string>());
            Assert.False(mapper.HasCompletedFireRiskAppraisal);
        }

        [Fact]
        public void CompanyWhoDidTheSurvey_ReturnsExpectedValue()
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "FRAEW_Company_Name", "SurveyCo" } });
            Assert.Equal("SurveyCo", mapper.CompanyWhoDidTheSurvey);

            mapper = new ImportedDataRow(new Dictionary<string, string>());
            Assert.Equal(string.Empty, mapper.CompanyWhoDidTheSurvey);
        }

        [Theory]
        [InlineData("external wall system", true)]
        [InlineData("ews", true)]
        [InlineData("both", true)]
        [InlineData("other fire safety defects", false)]
        [InlineData("ofsd", false)]
        [InlineData("random", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void ExternalWorks_ReturnsTrueForExpectedPrefixes(string input, bool expected)
        {
            var dict = new Dictionary<string, string>();
            if (input != null) dict["Defects_Related_To"] = input;
            var mapper = new ImportedDataRow(dict);
            Assert.Equal(expected, mapper.IsExternalWorks);
        }

        [Theory]
        [InlineData("other fire safety defects", true)]
        [InlineData("ofsd", true)]
        [InlineData("both", true)]
        [InlineData("external wall system", false)]
        [InlineData("ews", false)]
        [InlineData("random", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void InternalMitigationWorks_ReturnsTrueForExpectedPrefixes(string input, bool expected)
        {
            var dict = new Dictionary<string, string>();
            if (input != null) dict["Defects_Related_To"] = input;
            var mapper = new ImportedDataRow(dict);
            Assert.Equal(expected, mapper.IsInternalMitigationWorks);
        }

        [Theory]
        [InlineData("Inadequate/defective fire doors", EInternalFireSafetyDefect.InadequateDefectiveFireDoors)]
        [InlineData("Inadequate fire stopping around cables or pipes", EInternalFireSafetyDefect.InadequateStoppingAroundFireCablesAndPipes)]
        [InlineData("Inadequate/defective firefighting equipment or installations", EInternalFireSafetyDefect.InadequateDefectiveFireFightingEquipmentOrInstallation)]
        [InlineData("Inadequate/defective fire detection and alarm systems", EInternalFireSafetyDefect.InadequateDefectiveFireDetectionAndAlarmSystems)]
        [InlineData("Defective walls, ceilings or floors that can cause fire spread between dwellings or between dwellings and common parts", EInternalFireSafetyDefect.DefectiveWallsCeilingsFloors)]
        [InlineData("Incorrect or missing fire escape signage", EInternalFireSafetyDefect.InadequateOrMissingFireEscapeSignage)]
        [InlineData("Unprotected means of escape", EInternalFireSafetyDefect.UnprotectedMeansOfEscape)]
        [InlineData("Other", EInternalFireSafetyDefect.Other)]
        [InlineData("Don't Know", EInternalFireSafetyDefect.Other)]
        [InlineData("Random text by accident", EInternalFireSafetyDefect.Other)]
        [InlineData(null, null)]
        public void InternalElement_ReturnsExpectedValue(string input, EInternalFireSafetyDefect? expected)
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "Other_LCFS_Defect_Type", input } });
            Assert.Equal(expected, mapper.InternalElement);
            Assert.Equal(input, mapper.InternalElementDescription);
        }

        [Fact]
        public void RiskLevel_ReturnsExpectedValue()
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "FRAEW_Risk_Level", "High" } });
            Assert.Equal(ERiskType.High, mapper.RiskLevel);

            mapper = new ImportedDataRow(new Dictionary<string, string>());
            Assert.Null(mapper.RiskLevel);
        }

        [Theory]
        [InlineData("full remediation", EReplacementCladding.Full)]
        [InlineData("partial remediation", EReplacementCladding.Partial)]
        [InlineData("mitigation", EReplacementCladding.None)]
        [InlineData("none", EReplacementCladding.None)]
        [InlineData("unknown", null)]
        [InlineData("", null)]
        [InlineData(null, null)]
        public void CladdingReplacement_ReturnsExpectedValue(string input, EReplacementCladding? expected)
        {
            var dict = new Dictionary<string, string>();
            if (input != null) dict["FRAEW_Recommendation"] = input;
            var mapper = new ImportedDataRow(dict);
            Assert.Equal(expected, mapper.CladdingReplacement);
        }

        [Theory]
        [InlineData("RE Organisation Type")]
        [InlineData("RE_Organisation_Type")]
        [InlineData("RE organisation Type")]
        public void REOrganisationType_ColumnNameDifference_ReturnsExpectedValue(string columnName)
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { columnName, "Local Authority" } });
            Assert.Equal(EApplicationResponsibleEntityOrganisationType.LocalAuthority, mapper.OrganisationType);

            mapper = new ImportedDataRow(new Dictionary<string, string>());
            Assert.Null(mapper.OrganisationType);
        }

        [Theory]
        [InlineData("Local Authority", EApplicationResponsibleEntityOrganisationType.LocalAuthority)]
        [InlineData("LA", EApplicationResponsibleEntityOrganisationType.LocalAuthority)]
        [InlineData("RP", EApplicationResponsibleEntityOrganisationType.RegisteredProvider)]
        public void REOrganisationType_ReturnsExpectedValue(string input, EApplicationResponsibleEntityOrganisationType expected)
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "RE_Organisation_Type", input } });
            Assert.Equal(expected, mapper.OrganisationType);
        }

        [Theory]
        [InlineData("Type 1", EFireRiskAssessmentType.Type1FireRiskAssessment)]
        [InlineData("Type 2", EFireRiskAssessmentType.Type2FireRiskAssessment)]
        [InlineData("Type 3", EFireRiskAssessmentType.Type3FireRiskAssessment)]
        [InlineData("Type 4", EFireRiskAssessmentType.Type4FireRiskAssessment)]
        [InlineData("Don't Know", EFireRiskAssessmentType.DontKnow)]
        [InlineData("Don’t Know", EFireRiskAssessmentType.DontKnow)]
        [InlineData("NULL", null)]
        public void LatestAssessmentLevel_ReturnsExpectedValue(string input, EFireRiskAssessmentType? expected)
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "Latest_Assessment_Level", input } });
            Assert.Equal(expected, mapper.LatestAssessmentLevel);
        }

        [Fact]
        public void ValidationTest()
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
            var cssMapper = new ImportedDataRow(xlsColumns);
            var validator = new JsonDataIngestMapperIValidator();
            var result = validator.Validate(cssMapper);

            Assert.True(result.IsValid);
        }
    }
}
