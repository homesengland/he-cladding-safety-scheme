using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class UploadEvidenceViewModel
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public string DeleteEndpoint => "/ProgressReporting/UploadEvidence/Delete";

    public string[] AcceptedFileTypes => new[] { ".pdf" };

    public Shared.File AddedFile { get; set; }

    public IFormFile File { get; set; }

    public ESubmitAction SubmitAction { get; set; }

    public string ReturnUrl { get; set; }
}
