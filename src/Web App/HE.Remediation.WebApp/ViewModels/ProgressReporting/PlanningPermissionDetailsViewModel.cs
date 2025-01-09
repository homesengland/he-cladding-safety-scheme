using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class PlanningPermissionDetailsViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool ShowBuildingSafetyRegulatorRegistrationCode { get; set; }
    public int? PlanningPermissionSubmittedMonth { get; set; }
    public int? PlanningPermissionSubmittedYear { get; set; }
    public int? PlanningPermissionApprovedMonth { get; set; }
    public int? PlanningPermissionApprovedYear { get; set; }
    public int Version { get; set; }
    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}
