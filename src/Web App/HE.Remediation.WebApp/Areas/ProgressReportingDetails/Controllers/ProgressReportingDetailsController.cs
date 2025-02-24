using AutoMapper;
using HE.Remediation.Core.Attributes;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.Details.GetProgressReportCompanyDetails;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.Details.GetProgressReportDetails;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProgressReports.GetProgressReports;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.ViewModels.ProgressReporting;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.ProgressReportingDetails.Controllers;

[Area("ProgressReportingDetails")]
[Route("ProgressReporting")]
[CookieApplicationAuthorise]
[UserIdentityMustBeTheApplicationUser]
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
    public async Task<IActionResult> ProgressReportCompanyDetails([FromRoute] GetProgressReportCompanyDetailsRequest request)
    {
        var response = await _sender.Send(request);
        var model = _mapper.Map<ProgressReportCompanyDetailsViewModel>(response);

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
}