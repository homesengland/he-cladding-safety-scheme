using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class ResponsibleForTheRemediationViewModel
{
    public string ReturnUrl { get; set; }
    public EBuildingRemediationResponsibilityType? BuildingRemediationResponsibilityType { get; set; }
    public EApplicationScheme ApplicationScheme { get; set; }
    public ESubmitAction SubmitAction { get; set; }
}