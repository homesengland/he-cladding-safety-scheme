namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageInternalDefects;

public class CheckYourAnswersViewModel
{
    public decimal? InternalDefectsCost { get; set; }
    public string Description { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
}