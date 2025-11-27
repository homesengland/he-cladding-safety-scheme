using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.Submission;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.Submission;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.MonthlyProgressReporting.MonthlyProgressReportingSubmission.Controllers;

[Area("MonthlyProgressReportingSubmission")]
[Route("MonthlyProgressReporting/Submission")]
[CookieApplicationAuthorise]
public class MonthlyProgressReportingSubmissionController : Controller
{
    private readonly ISender _sender;

    public MonthlyProgressReportingSubmissionController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("Submit")]
    public async Task<IActionResult> Submit()
    {
        var response = await _sender.Send(new MonthlyReportSubmissionRequest());
        return View(new SubmissionViewModel() { ApplicationReferenceNumber = response.ApplicationReferenceNumber, BuildingName = response.BuildingName });
    }

    [HttpPost("Submit")]
    public async Task<IActionResult> Submit(SubmissionViewModel model)
    {
        if (!model.Confirmation)
        {
            ModelState.AddModelError("Confirmation", "You must confirm the information is correct.");
            return View(model);
        }
        await _sender.Send(new MonthlyReportSubmissionRequest() { IsConfirmed = model.Confirmation });
        return RedirectToAction("Submitted");
    }

    [HttpGet("Submitted")]
    public IActionResult Submitted()
    {
        return View();
    }
}