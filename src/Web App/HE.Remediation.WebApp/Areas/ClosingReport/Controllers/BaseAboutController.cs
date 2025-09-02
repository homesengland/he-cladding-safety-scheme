using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.ProceedFromAbout;
using HE.Remediation.WebApp.Attributes.Authorisation;
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
    public IActionResult About()
    {
        return View();
    }

    [HttpPost("About")]
    public async Task<IActionResult> ProceedFromAbout()
    {
        await _sender.Send(new UpdateTaskStatusRequest(ClosingReportTask, ETaskStatus.InProgress));
        return NextScreenAfterAbout;
    }
}