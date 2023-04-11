namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress
{
    public class GetBuildingAddressResponse
    {
        public bool? NonResidentialUnits { get; set; }
        public string NameNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string LocalAuthority { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
    }
}
