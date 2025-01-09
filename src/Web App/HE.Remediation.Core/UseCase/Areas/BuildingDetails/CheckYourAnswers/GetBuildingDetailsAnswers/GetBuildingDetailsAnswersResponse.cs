namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.CheckYourAnswers.GetBuildingDetailsAnswers
{
    public class GetBuildingDetailsAnswersResponse
    {
        public string BuildingUniqueName { get; set; }
        public int ResidentialUnitsCount { get; set; }
        public bool NonResidentialUnits { get; set; }
        public int NonResidentialUnitsCount { get; set; }
        public string BuildingNameNumber { get; set; }
        public string BuildingAddressLine1 { get; set; }
        public string BuildingAddressLine2 { get; set; }
        public string BuildingCity { get; set; }
        public string BuildingLocalAuthority { get; set; }
        public string BuildingCounty { get; set; }
        public string BuildingPostcode { get; set; }
        public bool BuildingHasSafetyRegulatorRegistrationCode { get; set; }
        public string BuildingSafetyRegulatorRegistrationCode { get; set; }
        public bool PartOfDevelopment { get; set; }
        public string DevelopmentName { get; set; }
        public int Storeys { get; set; }
        public DateTime? CorrectHeightConfirmedDate { get; set; }
        public bool OriginalDeveloperKnown { get; set; }
        public string DeveloperCompanyName { get; set; }
        public string DeveloperNameNumber { get; set; }
        public string DeveloperAddressLine1 { get; set; }
        public string DeveloperAddressline2 { get; set; }
        public string DeveloperCity { get; set; }
        public string DeveloperCounty { get; set; }
        public string DeveloperPostcode { get; set; }
        public string DeveloperStillInBusiness { get; set; }
        public bool DeveloperContacted { get; set; }
        public bool ReadOnly { get; set; }
    }
}
