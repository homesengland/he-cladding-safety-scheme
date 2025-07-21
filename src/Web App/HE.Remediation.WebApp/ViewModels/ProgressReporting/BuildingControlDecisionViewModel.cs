using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class BuildingControlDecisionViewModel
{
    public bool? BuildingControlRequired { get; set; }

    public int? DecisionDateMonth { get; set; }
    public int? DecisionDateYear { get; set; }

    public DateTime? DecisionDate =>
        DecisionDateMonth is >= 1 and <= 12 && DecisionDateYear is >= 2000 and <= 3000
            ? new DateTime(DecisionDateYear.Value, DecisionDateMonth.Value, 1)
            : null;

    public bool? Decision { get; set; }

    public string DecisionInformation { get; set; }

    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public int Version { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }
    public ESubmitAction SubmitAction { get; set; }
}