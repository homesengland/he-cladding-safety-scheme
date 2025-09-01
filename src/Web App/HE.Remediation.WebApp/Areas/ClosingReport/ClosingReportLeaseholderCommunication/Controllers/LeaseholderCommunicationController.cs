using AutoMapper;
using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.Areas.ClosingReport.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.ClosingReport.ClosingReportLeaseholderCommunication.Controllers;

[Area("ClosingReportLeaseholderCommunication")]
[Route("ClosingReport/LeaseholderCommunication")]
public class LeaseholderCommunicationController(ISender sender, IMapper mapper) : BaseClosingReportFileUploadController(sender, mapper)
{
    protected override EClosingReportFileType ClosingReportFileType => EClosingReportFileType.LeaseholderResidentCommunication;
    protected override EClosingReportTask ClosingReportTask => EClosingReportTask.CommunicationWithLeaseholders;
    protected override IActionResult NextScreenAfterAbout => 
                                        RedirectToAction("Upload", "LeaseholderCommunication", 
                                            new { Area = "ClosingReportLeaseholderCommunication" });
}
