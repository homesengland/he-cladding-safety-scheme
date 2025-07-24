using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class BuildingControlValidationViewModel
{
    public bool? BuildingControlRequired { get; set; }

    public int? ValidationDateMonth { get; set; }
    public int? ValidationDateYear { get; set; }

    public DateTime? ValidationDate =>
        ValidationDateMonth is >= 1 and <= 12 && ValidationDateYear is >= 2000 and <= 3000
            ? new DateTime(ValidationDateYear.Value, ValidationDateMonth.Value, 1)
            : null;

    public string ValidationInformation { get; set; }
    
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public int Version { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }
    public ESubmitAction SubmitAction { get; set; }
}