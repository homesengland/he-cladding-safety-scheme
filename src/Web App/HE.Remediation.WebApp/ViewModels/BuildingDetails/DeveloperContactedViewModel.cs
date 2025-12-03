using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class DeveloperContactedViewModel
{
    public string ReturnUrl { get; set; }
    public bool? HasDeveloperBeenContactedAboutRemediation { get; set; }
    public ESubmitAction SubmitAction { get; set; }
    public EApplicationScheme ApplicationScheme { get; set; }
}