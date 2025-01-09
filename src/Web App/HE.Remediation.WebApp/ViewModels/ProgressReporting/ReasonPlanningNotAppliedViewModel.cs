using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ReasonPlanningNotAppliedViewModel
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public string ReasonPlanningPermissionNotApplied { get; set; }

    public bool? PlanningPermissionNeedsSupport { get; set; }

    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}
