using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.DataIngest.RAS;
using HE.Remediation.Core.UseCase.DataIngest.RAS.Validation;

namespace HE.Remediation.Core.Tests.UseCase.DataIngest.RAS
{
    public class ImportedDataRowTests
    {
        [Fact]
        public void Developer_ReturnsExpectedValue()
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "Developer", "Global Corp Ltd" } });
            Assert.Equal("Global Corp Ltd", mapper.Developer);

            mapper = new ImportedDataRow(new Dictionary<string, string>());
            Assert.Equal(string.Empty, mapper.Developer);
        }

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

            mapper = new ImportedDataRow(new Dictionary<string, string> { { "Address_Line_1", string.Empty } });
            Assert.Equal(string.Empty, mapper.AddressLine1);

            mapper = new ImportedDataRow(new Dictionary<string, string>());
            Assert.Equal(string.Empty, mapper.AddressLine1);
        }

        [Fact]
        public void AddressLine1_IfTooLong_ReturnsNull()
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "Address_Line_1", "I am too long. I am too long. I am too long. I am too long. I am too long. I am too long. I am too long. I am too long. I am too long. I am too long. I am too long. I am too long. " } });
            Assert.Null(mapper.AddressLine1);
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
            Assert.Equal(expected, mapper.DwellingUnitsTotal);
        }

        [Fact]
        public void NumberOfStories_ReturnsExpectedValue()
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "Building_Height_Storeys", "7" } });
            Assert.Equal(7, mapper.BuildingHeightInStoreys);

            mapper = new ImportedDataRow(new Dictionary<string, string>());
            Assert.Null(mapper.BuildingHeightInStoreys);
        }

        [Fact]
        public void DevelopmentName_ReturnsExpectedValue()
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "DevelopmentName", "Tree Towers" } });
            Assert.Equal("Tree Towers", mapper.DevelopmentName);

            mapper = new ImportedDataRow(new Dictionary<string, string>());
            Assert.Equal(string.Empty, mapper.DevelopmentName);
        }

        [Theory]
        [InlineData("10", 10)]
        [InlineData("10abc", 10)]
        [InlineData("abc", null)]
        [InlineData("", null)]
        [InlineData(null, null)]
        public void HowManyLeaseholders_ParsesIntOrReturnsZero(string input, int? expected)
        {
            var dict = new Dictionary<string, string>();
            if (input != null) dict["Leasehold_Units"] = input;
            var mapper = new ImportedDataRow(dict);
            Assert.Equal(expected, mapper.LeaseholdUnits);
        }

        [Fact]
        public void FRA_Outcome_ReturnsExpectedValue()
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "FRA_Outcome", "High" } });
            Assert.Equal(EFraRiskRating.High, mapper.FRAOutcome);

            mapper = new ImportedDataRow(new Dictionary<string, string>());
            Assert.Null(mapper.FRAOutcome);
        }

        [Theory]
        [InlineData("Resident management company (RMC)", EResponsibleEntityRelation.ResidentLedOrganisation)]
        [InlineData("Right-To-Manage Company (RTM)", EResponsibleEntityRelation.RightToManageCompany)]
        [InlineData("Freeholder", EResponsibleEntityRelation.Freeholder)]
        [InlineData("Head Leaseholder", EResponsibleEntityRelation.HeadLeaseholder)]
        [InlineData("Random Company Name Ltd", null)]
        public void REOrganisationType_ReturnsExpectedValue(string input, EResponsibleEntityRelation? expected)
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "RE_Organisation_Type", input } });
            Assert.Equal(expected, mapper.ResponsibleEntityOrganisationType);
        }

        [Theory]
        [InlineData("No - Interim Measures Not Required", EYesNoNonBoolean.No)]
        [InlineData(null, null)]
        public void Has_Interim_Measures_ReturnsExpectedValue(string input, EYesNoNonBoolean? expected)
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "Has_Interim_Measures", input } });
            Assert.Equal(expected, mapper.HasInterimMeasures);
        }

        [Fact]
        public void Interim_Measure_Type_ReturnsExpectedValue()
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> {
                { "Interim_Measure_Type_1", "Common Fire Alarm" },
                { "Interim_Measure_Type_2", "nothing" },
                { "Interim_Measure_Type_3", "Fire Heat Smoke Detection" }
            });
            Assert.Equal(2, mapper.BuildingInterimMeasuresTypes.Count());

            mapper = new ImportedDataRow(new Dictionary<string, string> { 
                { "Interim_Measure_Type_1", "Common Fire Alarm" },
                { "Interim_Measure_Type_2", "Evacuation Management" },
                { "Interim_Measure_Type_3", "Fire Heat Smoke Detection" },
                { "Interim_Measure_Type_4", "Fire Supression System" },
                { "Interim_Measure_Type_5", "Waking Watch" },
                { "Interim_Measure_Type_6", "Simultaneous Evacuation Strategy" },
                { "Interim_Measure_Type_7", "Not Applicable" }
            });
            Assert.Equal(7, mapper.BuildingInterimMeasuresTypes.Count());
        }

        [Theory]
        [InlineData("Common Fire Alarm", EInterimMeasuresType.CommonFireAlarm)]
        [InlineData("Evacuation Management", EInterimMeasuresType.EvacuationManagement)]
        [InlineData("Fire Heat Smoke Detection", EInterimMeasuresType.FireHeatSmokeDetection)]
        [InlineData("Fire Supression System", EInterimMeasuresType.FireSupressionSystem)]
        [InlineData("Waking Watch", EInterimMeasuresType.WakingWatch)]
        [InlineData("Simultaneous Evacuation Strategy", EInterimMeasuresType.SimultaneousEvacuationStrategy)]
        [InlineData("Not Applicable", EInterimMeasuresType.NotApplicable)]
        [InlineData("Other", EInterimMeasuresType.Other)]
        public void Interim_Measure_Types_ReturnsExpectedValue(string input, EInterimMeasuresType expected)
        {
            var mapper = new ImportedDataRow(new Dictionary<string, string> { { "Interim_Measure_Type_1", input } });
            Assert.Equal(expected, mapper.BuildingInterimMeasuresTypes.First());
        }

        [Fact]
        public void ValidationTest()
        {
            var xlsColumns = new Dictionary<string, string>()
            {
                { "Developer", "Test Company" },
                { "Building_Name", "Jamie Test" },
                { "Address_Line_1", "17-53 (odd numbers only) QUERCETUM CLOSE, AYLESBURY" },
                { "Postcode", "HP19 8JN" },
                { "Town_City", "AYLESBURY" },
                { "Local_Authority", "Buckinghamshire" },
                { "Development_Name", "HOLMANS PLACE" },
                { "Building_Height_Metres", "11.4" },
                { "Building_Height_Storeys", "5" },
                { "Practical_Completion_Date", "01/07/2016 00:00:00" },
                { "Major_Refurbishment_Date", "NA" },
                { "Dwelling_Units_Responsible_For", "19" },
                { "Leasehold_Units", "19" },
                { "Non_Residential_Units", "" },
                { "Low_Cost_Rental_Homes_Registered_Provider", "" },
                { "Current_Freeholder", "ABBEY DEVELOPMENTS LTD" },
                { "RE_Organisation_Type", "Resident management company (RMC)" },
                { "Responsible_Entity_Name", "HOLMANS PLACE MANAGEMENT LTD" },
                { "Has_FRA", "Yes" },
                { "FRA_Company_Name", "4site Consulting Ltd" },
                { "FRA_Outcome", "Other (Please specify)" },
                { "FRA_Recommendations", "Latest annual FSA dated 27/11/23. Action points noted, which include some repair/maint. work  ." },
                { "Has_FRAEW", "Yes" },
                { "FRAEW_Company_Name", "Semper Fire Engineering" },
                { "FRAEW_Risk_Level", "Medium Risk - tolerable" },
                { "Evacuation_Strategy", "Stay Put" },
                { "Has_Interim_Measures", "Yes" },
                { "Interim_Measure_Type_1", "Evacuation Management" },
                { "Interim_Measure_Type_2", "" },
                { "Interim_Measure_Type_3", "" },
                { "Interim_Measure_Type_4", "" },
                { "Interim_Measure_Type_5", "" },
                { "Interim_Measure_Type_6", "" },
                { "Interim_Measure_Type_7", "" }
            };

            var cssMapper = new ImportedDataRow(xlsColumns);
            var validator = new JsonDataIngestMapperIValidator();
            var result = validator.Validate(cssMapper);

            Assert.True(result.IsValid);
        }
    }
}
