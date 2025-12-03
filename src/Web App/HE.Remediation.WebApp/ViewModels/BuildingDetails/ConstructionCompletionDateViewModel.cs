using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class ConstructionCompletionDateViewModel
{
    public string ReturnUrl { get; set; }
    public int? ConstructionCompletionDateMonth { get; set; }
    public int? ConstructionCompletionDateYear { get; set; }
    public ESubmitAction SubmitAction { get; set; }
}
