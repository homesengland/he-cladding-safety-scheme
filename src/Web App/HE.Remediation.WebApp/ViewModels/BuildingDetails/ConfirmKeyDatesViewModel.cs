using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class ConfirmKeyDatesViewModel
{
    public string ReturnUrl { get; set; }
    public int? StartDateMonth { get; set; }
    public int? StartDateYear { get; set; }
    public int? UnsafeCladdingRemovalDateMonth { get; set; }
    public int? UnsafeCladdingRemovalDateYear { get; set; }
    public int? ExpectedDateForCompletionMonth { get; set; }
    public int? ExpectedDateForCompletionYear { get; set; }
    public ESubmitAction SubmitAction { get; set; }
}