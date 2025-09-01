using AutoMapper;
using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.Areas.ClosingReport.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.ClosingReport.ClosingReportFireRiskAssessment.Controllers;

[Area("ClosingReportFireRiskAssessment")]
[Route("ClosingReport/FireRiskAssessment")]
public class FireRiskAssessmentController(ISender sender, IMapper mapper) : BaseClosingReportFileUploadController(sender, mapper)
{
    protected override EClosingReportFileType ClosingReportFileType => EClosingReportFileType.ExitFraew;
    protected override EClosingReportTask ClosingReportTask => EClosingReportTask.FireRiskAssessment;
    protected override IActionResult NextScreenAfterAbout => 
                                        RedirectToAction("Upload", "FireRiskAssessment", 
                                            new { Area = "ClosingReportFireRiskAssessment" });
}
