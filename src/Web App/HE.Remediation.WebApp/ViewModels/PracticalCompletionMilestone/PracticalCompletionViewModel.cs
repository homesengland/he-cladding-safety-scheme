using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.PracticalCompletionMilestone;

public class PracticalCompletionViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public DateTime? PracticalCompletionDate { get; set; }

    public ESubmitAction SubmitAction { get; set; }
}