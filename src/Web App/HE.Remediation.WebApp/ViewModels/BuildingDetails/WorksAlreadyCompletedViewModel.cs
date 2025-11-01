using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class WorksAlreadyCompletedViewModel
{
    public string ReturnUrl { get; set; }
    public bool? WorksAlreadyCompleted { get; set; }
    public EApplicationScheme ApplicationScheme { get; set; }
    public ESubmitAction SubmitAction { get; set; }
}