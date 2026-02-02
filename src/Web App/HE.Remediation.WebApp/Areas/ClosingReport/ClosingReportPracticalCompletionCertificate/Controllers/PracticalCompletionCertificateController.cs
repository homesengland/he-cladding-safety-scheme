using AutoMapper;
using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.Areas.ClosingReport.Controllers;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.ClosingReport.ClosingReportPracticalCompletionCertificate.Controllers;

[Area("ClosingReportPracticalCompletionCertificate")]
[Route("ClosingReport/PracticalCompletionCertificate")]
public class PracticalCompletionCertificateController(ISender sender, IMapper mapper) : BaseClosingReportFileUploadController(sender, mapper)
{
    protected override EClosingReportFileType ClosingReportFileType => EClosingReportFileType.PracticalCompletionCertificate;
    protected override EClosingReportTask ClosingReportTask => EClosingReportTask.PracticalCompletionCertificate;
    protected override IActionResult NextScreenAfterAbout =>
                                        RedirectToAction("Upload", "PracticalCompletionCertificate",
                                            new { Area = "ClosingReportPracticalCompletionCertificate" });
}