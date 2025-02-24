namespace HE.Remediation.WebApp.ViewModels.StartedOnSiteMilestone;

public class CheckYourAnswersViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public DateTime? StartedOnSiteDate { get; set; }
    public bool IsSubmitted { get; set; }
}