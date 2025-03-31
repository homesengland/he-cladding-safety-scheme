namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProgrammePlan;

public class CheckYourAnswersViewModel
{
    public bool? HasProjectPlan { get; set; }
    public IList<string> FileNames { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
}