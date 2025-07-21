using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class HaveYouAppliedForBuildingControlViewModel
{
    public bool? BuildingControlRequired { get; set; }
    public bool? HasAppliedForBuildingControl { get; set; }

    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public int Version { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }
    public ESubmitAction SubmitAction { get; set; }
}