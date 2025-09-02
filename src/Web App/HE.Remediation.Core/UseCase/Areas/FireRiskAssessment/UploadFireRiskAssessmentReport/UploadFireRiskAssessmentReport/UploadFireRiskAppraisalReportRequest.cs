using HE.Remediation.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment.UploadFireRiskAssessmentReport.UploadFireRiskAssessmentReport;

public class UploadFireRiskAssessmentReportRequest : IRequest
{
    public EFireRiskAssessmentType? FireRiskAssessmentType { get; set; }
    public IFormFile FraReportFile { get; set; }
    public bool FraAlreadyUploaded { get; set; }
    public EApplicationScheme ApplicationScheme { get; set; }
}