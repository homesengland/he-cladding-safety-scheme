using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.GrantCertifyingOfficer;

public class GrantCertifyingOfficerAddressResult
{
    public string NameNumber { get; set; }
    
    public string AddressLine1 { get; set; }
    
    public string AddressLine2 { get; set; }
    
    public string City { get; set; }
    
    public string County { get; set; }

    public string Postcode { get; set; }
}
