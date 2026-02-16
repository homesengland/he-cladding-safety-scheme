using HE.Remediation.Core.Enums;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport.UploadFireRiskAppraisalReport
{
    public class UploadFireRiskAppraisalReportRequest : IRequest
    {
        public IFormFile FraewFile { get; set; }
        public IFormFile SummaryFile { get; set; }

        public bool FraewAlreadyUploaded { get; set; }
        public bool SummaryAlreadyUploaded { get; set; }

        public EApplicationScheme ApplicationScheme { get; set; }
    }
}
