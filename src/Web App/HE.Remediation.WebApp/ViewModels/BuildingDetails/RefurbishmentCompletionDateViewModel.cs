using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class RefurbishmentCompletionDateViewModel
{
    public string ReturnUrl { get; set; }
    public int? RefurbishmentCompletionDateMonth { get; set; }
    public int? RefurbishmentCompletionDateYear { get; set; }
    public ESubmitAction SubmitAction { get; set; }
}