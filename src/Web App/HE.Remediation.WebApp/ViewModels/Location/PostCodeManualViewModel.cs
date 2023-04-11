using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.Location;

public class PostCodeManualViewModel : AddressViewModel
{
    //public string NameNumber { get; set; }
    //public string AddressLine1 { get; set; }
    //public string AddressLine2 { get; set; }
    //public string City { get; set; }
    public string LocalAuthority { get; set; }
    //public string County { get; set; }
    //public string Postcode { get; set; }
    public string ReturnUrl { get; set; }

    public bool NonResidentialUnits { get; set; }

    public EResponsibleEntityType ResponsibleEntityType { get; set; }

    public List<string> outputLocations { get; set; }
}
