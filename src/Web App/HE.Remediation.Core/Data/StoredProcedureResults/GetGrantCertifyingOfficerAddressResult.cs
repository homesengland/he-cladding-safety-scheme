using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetGrantCertifyingOfficerAddressResult
{
    public Guid Id { get; set; }
    public string NameNumber { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string Postcode { get; set; }
    public string LocalAuthority { get; set; }
    public int? CountryId { get; set; }
    public int? CertifyingOfficerResponseId { get; set; }
}