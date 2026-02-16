using AutoMapper;
using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.Areas.ClosingReport.Controllers;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.ClosingReport.ClosingReportBuildingControlEvidence.Controllers;

[Area("ClosingReportBuildingControlEvidence")]
[Route("ClosingReport/BuildingControlEvidence")]
public class BuildingControlEvidenceController(ISender sender, IMapper mapper) : BaseClosingReportFileUploadController(sender, mapper)
{
    protected override EClosingReportFileType ClosingReportFileType => EClosingReportFileType.BuildingRegulations;
    protected override EClosingReportTask ClosingReportTask => EClosingReportTask.BuildingControlEvidence;
    protected override IActionResult NextScreenAfterAbout => 
                                        RedirectToAction("Upload", "BuildingControlEvidence", 
                                            new { Area = "ClosingReportBuildingControlEvidence" });
}
