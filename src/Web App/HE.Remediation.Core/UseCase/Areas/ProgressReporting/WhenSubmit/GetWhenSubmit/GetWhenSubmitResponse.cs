﻿
namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenSubmit.GetWhenSubmit;

public class GetWhenSubmitResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool? HasAppliedForBuildingControl { get; set; }
	public int? SubmissionMonth { get; set; }
    public int? SubmissionYear { get; set; }
    public int Version { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }
}
