using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.Areas.MonthlyProgressReporting.MonthlyProgressReportingKeyDates.Controllers;

[Area("MonthlyProgressReportingKeyDates")]
[Route("MonthlyProgressReporting/KeyDates")]
[CookieApplicationAuthorise]
public class KeyDatesController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly Guid _reportId;

    public KeyDatesController(ISender sender, IMapper mapper, IApplicationDataProvider applicationDataProvider)
        : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
        _reportId = applicationDataProvider.GetProgressReportId();
    }

    protected override IActionResult DefaultStart => RedirectToAction("Index", "KeyDates", new { Area = "MonthlyProgressReportingKeyDates" });

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var response = await _sender.Send(new GetKeyDatesRequest(_reportId));
        var viewModel = _mapper.Map<KeyDatesViewModel>(response);

        return View(viewModel);
    }

    [HttpPost()]
    public async Task<IActionResult> ValidateAndSave()
    {
        // refetch view model
        var response = await _sender.Send(new GetKeyDatesRequest(_reportId));
        var viewModel = _mapper.Map<KeyDatesViewModel>(response);

        // validate
        var validator = new KeyDatesViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return View("Index", viewModel);
        }

        // complete task
        await _sender.Send(new SetKeyDatesRequest(ETaskStatus.Completed));

        return RedirectToAction("TaskList", "MonthlyProgressReporting", new { Area = "MonthlyProgressReporting" });
    }
}