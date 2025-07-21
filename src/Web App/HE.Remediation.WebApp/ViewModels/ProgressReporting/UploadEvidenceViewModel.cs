using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class UploadEvidenceViewModel : FileUploadViewModel
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool HasVisitedCheckYourAnswers { get; set; }

    public override string DeleteEndpoint => "/ProgressReporting/UploadEvidence/Delete";

    public override string[] AcceptedFileTypes => new[] { ".pdf" };
    public override int NumberOfFilesAllowed => 5;

    public string ReturnUrl { get; set; }
}
