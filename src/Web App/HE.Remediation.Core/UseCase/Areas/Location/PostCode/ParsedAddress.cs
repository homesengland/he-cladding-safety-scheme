using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.UseCase.Areas.Location.PostCode;

public class ParsedAddress
{

    public string NameNumber { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string LocalAuthority { get; set; }
    public string County { get; set; }
    public string Postcode { get; set; }

    /// <summary>
    /// Check if we have a completely empty structure
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        return ((String.IsNullOrWhiteSpace(NameNumber)) &&
                (String.IsNullOrWhiteSpace(AddressLine1)) &&
                (String.IsNullOrWhiteSpace(AddressLine2)) &&
                (String.IsNullOrWhiteSpace(City)) &&
                (String.IsNullOrWhiteSpace(LocalAuthority)) &&
                (String.IsNullOrWhiteSpace(County)) &&
                (String.IsNullOrWhiteSpace(Postcode)));
    }

    public ParsedAddress()
    {
        NameNumber = string.Empty;
        AddressLine1 = string.Empty;
        AddressLine2 = string.Empty;
        City = string.Empty;
        LocalAuthority = string.Empty;
        County = string.Empty;
        Postcode = string.Empty;
    }
}
