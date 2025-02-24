using MediatR;
using Microsoft.AspNetCore.Http;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport.UploadFireRiskAppraisalReport
{
    public class UploadFireRiskAppraisalReportRequest : IRequest
    {
        public IFormFile FraewFile { get; set; }
        public IFormFile SummaryFile { get; set; }
    }
}
