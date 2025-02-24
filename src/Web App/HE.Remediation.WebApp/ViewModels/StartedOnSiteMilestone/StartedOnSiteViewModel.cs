using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.StartedOnSiteMilestone;

public class StartedOnSiteViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public DateTime? StartedOnSiteDate { get; set; }

    public ESubmitAction SubmitAction { get; set; }
}