using AutoMapper;
using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.Areas.ClosingReport.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.ClosingReport.ClosingReportFinalCost.Controllers;

[Area("ClosingReportFinalCost")]
[Route("ClosingReport/FinalCost")]
public class FinalCostController(ISender sender, IMapper mapper) : BaseClosingReportFileUploadController(sender, mapper)
{
    protected override EClosingReportFileType ClosingReportFileType => EClosingReportFileType.FinalCost;
    protected override EClosingReportTask ClosingReportTask => EClosingReportTask.UploadFinalCostReport;
    protected override IActionResult NextScreenAfterAbout => 
                                        RedirectToAction("Upload", "FinalCost", 
                                            new { Area = "ClosingReportFinalCost" });
}
