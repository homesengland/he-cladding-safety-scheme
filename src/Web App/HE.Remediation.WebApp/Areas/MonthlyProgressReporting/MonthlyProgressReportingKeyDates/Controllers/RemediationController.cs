using AutoMapper;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using FluentValidation.AspNetCore;
using HE.Remediation.WebApp.Attributes.Routing;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.Remediation;

namespace HE.Remediation.WebApp.Areas.MonthlyProgressReporting.MonthlyProgressReportingKeyDates.Controllers;

[Area("MonthlyProgressReportingKeyDates")]
[Route("MonthlyProgressReporting/Remediation")]
[CookieApplicationAuthorise]
[AreaRedirect]
[RecordRoute]
public class RemediationController : Controller
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly Guid _reportId;

    public RemediationController(ISender sender, IMapper mapper, IApplicationDataProvider applicationDataProvider)
    {
        _sender = sender;
        _mapper = mapper;
        _reportId = applicationDataProvider.GetProgressReportId();
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var response = await _sender.Send(new GetRemediationRequest(_reportId));
        var viewModel = _mapper.Map<RemediationViewModel>(response);
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Index(RemediationViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (model.SubmitAction == ESubmitAction.Continue)
        {
            var validator = new RemediationViewModelValidator();

            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetRemediationRequest>(model);
            var response = await _sender.Send(request, cancellationToken);

            return response.HasChangedDates
                ? RedirectToAction("DatesChanged")
                : RedirectToAction("Index", "KeyDates");
        }

        return RedirectToAction("Index", "KeyDates");
    }

    [HttpGet(nameof(DatesChanged))]
    public async Task<IActionResult> DatesChanged(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetRemediationDatesChangedRequest.Request, cancellationToken);
        var model = _mapper.Map<RemediationDatesChangedViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(DatesChanged))]
    public async Task<IActionResult> DatesChanged(RemediationDatesChangedViewModel model, CancellationToken cancellationToken)
    {
        var validator = new RemediationDatesChangedViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View(model);
        }

        var request = _mapper.Map<SetRemediationDatesChangedRequest>(model);
        await _sender.Send(request, cancellationToken);

        return model.SubmitAction == ESubmitAction.Exit
            ? RedirectToAction("TaskList", "MonthlyProgressReporting")
            : RedirectToAction("Index", "KeyDates");
    }
}