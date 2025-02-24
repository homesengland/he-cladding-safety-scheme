using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class UploadFireRiskAppraisalReportViewModel
    {
        public string DeleteEndpoint => "/FireRiskAppraisal/DeleteReport";
        public string[] FraewAcceptedFileTypes => new[] { ".pdf" };
        public string[] FraewSummaryAcceptedFileTypes => new[] { ".xlsx" };

        public Shared.File AddedFraew { get; set; }

        public IFormFile Fraew { get; set; }

        public Shared.File AddedSummary { get; set; }

        public IFormFile FraewSummary { get; set; }

        public string ReturnUrl { get; set; }
    }
}
