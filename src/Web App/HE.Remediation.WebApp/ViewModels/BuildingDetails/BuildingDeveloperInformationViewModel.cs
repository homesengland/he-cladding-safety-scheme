using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class BuildingDeveloperInformationViewModel : AddressViewModel
{
    public bool? DoYouKnowOriginalDeveloper { get; set; }
    public string OrganisationName { get; set; }
    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}