namespace HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.GrantCertifyingOfficer;

public class UpdateWorkPackageGrantCertifyingOfficerAddressParameters
{
    public Guid ApplicationId { get; set; }
    
    public string NameNumber { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public string City { get; set; }

    public string LocalAuthority { get; set; }

    public string County { get; set; }

    public string Postcode { get; set; }

    public string SubBuildingName { get; set; }

    public string BuildingName { get; set; }

    public string BuildingNumber { get; set; }

    public string Street { get; set; }

    public string Town { get; set; }

    public string AdminArea { get; set; }

    public string UPRN { get; set; }

    public string AddressLines { get; set; }

    public string XCoordinate { get; set; }

    public string YCoordinate { get; set; }

    public string Toid { get; set; }

    public string BuildingType { get; set; }
}
