using AutoMapper;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting;
using LegacyProgressReportCompanyDetails = HE.Remediation.Core.UseCase.Areas.ProgressReporting.Details.GetProgressReportCompanyDetails;
using LegacyProgressReportModels = HE.Remediation.WebApp.ViewModels.ProgressReporting;

namespace HE.Remediation.WebApp.Areas.ProgressReportingDetails.Controllers;

[Area("ProgressReportingDetails")]
[Route("ProgressReporting")]
[CookieApplicationAuthorise]
public class ProgressReportingDetailsController : Controller
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ProgressReportingDetailsController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    #region Details

    [HttpGet("Details/{progressReportId:guid}")]
    public async Task<IActionResult> ProgressReportDetails([FromRoute] GetProgressReportDetailsRequest request)
    {
        var response = await _sender.Send(request);
        var model = _mapper.Map<ProgressReportDetailsViewModel>(response);

        return View(model);
    }

    [HttpGet("Details/Company/{teamMemberId:guid}")]
    public async Task<IActionResult> ProgressReportCompanyDetails([FromRoute] LegacyProgressReportCompanyDetails.GetProgressReportCompanyDetailsRequest request)
    {
        var response = await _sender.Send(request);
        var model = _mapper.Map<LegacyProgressReportModels.ProgressReportCompanyDetailsViewModel>(response);

        return View(model);
    }

    #endregion

    #region Progress Reports

    [HttpGet(nameof(ProgressReports))]
    public async Task<IActionResult> ProgressReports()
    {
        var submittedResponse = await _sender.Send(GetProgressReportsRequest.Request);
        var viewModel = _mapper.Map<ProgressReportsViewModel>(submittedResponse);

        return View(viewModel);
    }

    #endregion

    [HttpGet("MonthlyReportPdf/{progressReportId:guid}")]
    public async Task<IActionResult> MonthlyReportPdf([FromRoute] MonthlyReportPdfRequest request)
    {
        var file = await _sender.Send(request);
        return File(file.File, "application/pdf", file.Filename);
    }
}