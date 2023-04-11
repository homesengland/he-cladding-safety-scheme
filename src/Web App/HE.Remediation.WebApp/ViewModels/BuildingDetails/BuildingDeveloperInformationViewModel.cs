using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class BuildingDeveloperInformationViewModel 
{
    public bool? DoYouKnowOriginalDeveloper { get; set; }
    
    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}