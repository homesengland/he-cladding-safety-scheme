
using File = HE.Remediation.WebApp.ViewModels.Shared.File;
namespace HE.Remediation.WebApp.ViewModels.ClosingReport
{
    public class EvidenceSubmissionViewModel
    {
        public IFormFile File { get; set; }
        public string[] AcceptedFileTypes => [".pdf"];
        public string ReturnUrl { get; set; }
        public File AddedEvidenceSubmission { get; set; }

    }
}
