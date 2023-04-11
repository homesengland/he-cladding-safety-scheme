using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class BuildingDeveloperInformationAddressViewModel : AddressViewModel
{
    public string OrganisationName { get; set; }
    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}
