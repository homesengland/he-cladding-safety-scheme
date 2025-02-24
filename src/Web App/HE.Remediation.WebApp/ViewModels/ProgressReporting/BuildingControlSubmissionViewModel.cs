using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class BuildingControlSubmissionViewModel
{
    public bool? BuildingControlRequired { get; set; }

    public int? SubmissionDateMonth { get; set; }
    public int? SubmissionDateYear { get; set; }

    public DateTime? SubmissionDate =>
        SubmissionDateMonth is >= 1 and <= 12 && SubmissionDateYear is >= 2000 and <= 3000
            ? new DateTime(SubmissionDateYear.Value, SubmissionDateMonth.Value, 1)
            : null;
    public string SubmissionInformation { get; set; }
    public string BuildingControlApplicationReference { get; set; }

    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public int Version { get; set; }
    public ESubmitAction SubmitAction { get; set; }
}