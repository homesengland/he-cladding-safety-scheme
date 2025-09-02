using HE.Remediation.Core.Enums;
using Microsoft.AspNetCore.Http;
using FileRes = HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails
{
    public class UploadEvidenceSubmissionUploadResponse
    {
        public EClosingReportFileType UploadType { get; set; }
        public IReadOnlyCollection<FileRes.FileResult> AddedFile { get; set; }
        public IFormFile File { get; set; }
        public string[] AcceptedFileTypes => new[] { ".pdf" };
    }
}
