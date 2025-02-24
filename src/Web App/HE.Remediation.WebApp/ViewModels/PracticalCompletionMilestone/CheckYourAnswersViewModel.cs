namespace HE.Remediation.WebApp.ViewModels.PracticalCompletionMilestone;

public class CheckYourAnswersViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public DateTime? PracticalCompletionDate { get; set; }
    public bool IsSubmitted { get; set; }
}