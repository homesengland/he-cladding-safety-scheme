using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetClosingReportAbout;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.ProceedFromAbout;
using HE.Remediation.WebApp.ViewModels.ClosingReport.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.ClosingReport.Controllers;

public abstract class BaseAboutController : StartController
{
    private readonly ISender _sender;

    protected abstract EClosingReportTask ClosingReportTask { get; }
    protected abstract IActionResult NextScreenAfterAbout { get; }

    protected override IActionResult DefaultStart => RedirectToAction("About");

    public BaseAboutController(ISender sender) : base(sender)
    {
        _sender = sender;
    }

    [HttpGet("About")]
    public async Task<IActionResult> About()
    {
        var response = await _sender.Send(GetClosingReportAboutRequest.Request);

        return View(new ClosingReportAboutViewModel { ApplicationReferenceNumber = response.ApplicationReferenceNumber, BuildingName = response.BuildingName });
    }

    [HttpPost("About")]
    public async Task<IActionResult> ProceedFromAbout()
    {
        await _sender.Send(new UpdateTaskStatusRequest(ClosingReportTask, ETaskStatus.InProgress));
        return NextScreenAfterAbout;
    }
}