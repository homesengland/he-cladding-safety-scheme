using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AddressDetails.Get;

public class GetAddressDetailsResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public string NameNumber { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public string City { get; set; }

    public string County { get; set; }

    public string Postcode { get; set; }

    public ECertifyingOfficerResponse CertifyingOfficerResponse { get; set; }
}