using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.Location;

public class PostCodeSelectionViewModel
{
    public string SelectedAddressId { get; set; }

    public bool HaveResults { get; set; }

    public string ReturnUrl { get; set; }

    public string PostCode { get; set; }    

    public List<KeyValuePair<string, string>> OutputLocations { get; set; }        

    public bool NonResidentialUnits { get; set; }

    public EResponsibleEntityType ResponsibleEntityType { get; set; }
}
