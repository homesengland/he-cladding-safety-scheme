using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class BuildingControlForecastViewModel
{
    public bool? BuildingControlRequired { get; set; }
    public int? ForecastDateMonth { get; set; }
    public int? ForecastDateYear { get; set; }

    public DateTime? ForecastDate =>
        ForecastDateMonth is >= 1 and <= 12 && ForecastDateYear is >= 2000 and <= 3000
            ? new DateTime(ForecastDateYear.Value, ForecastDateMonth.Value, 1)
            : null;

    public string ForecastInformation { get; set; }

    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public int Version { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }
    public ESubmitAction SubmitAction { get; set; }
}