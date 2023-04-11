using HE.Remediation.Core.Services.Location;

namespace HE.Remediation.WebApp.ViewModels;

public class PostCodeResultsViewModel
{
    public string PostCode { get; set; }

    public bool HaveResults { get; set; }

    public bool NoResultsReturned { get; set; }

    public PostCodeResults Results { get; set; }

    public string SelectedAddressId { get; set; }

    public List<PostCodeResult> locations { get; set; }                    

    public List<string> outputLocations { get; set; }        

    public bool NonResidentialUnits { get; set; }

    public string ReturnUrl { get; set; }   
}
