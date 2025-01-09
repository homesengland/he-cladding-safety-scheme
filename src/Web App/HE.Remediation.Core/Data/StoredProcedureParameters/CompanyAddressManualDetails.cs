
namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class CompanyAddressManualDetails
{
    public string NameNumber { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string Postcode { get; set; }
    public int? CountryId { get; set; }
}
