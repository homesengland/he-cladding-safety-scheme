using HE.Remediation.WebApp.ViewModels.ClosingReport.Shared;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport
{
    public class UploadEvidenceSubmissionUploadViewModel : ClosingReportBaseViewModel
    {
        public Guid? Id { get; set; }
        public Guid? FileId { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string MimeType { get; set; }
        public int? Size { get; set; }

        public IFormFile File { get; set; }

        public string[] AcceptedFileTypes => new[] { ".pdf" };

        public int NumberOfFilesAllowed => 1;

        public bool ViaCheckAnswer { get; set; }
    }
}
