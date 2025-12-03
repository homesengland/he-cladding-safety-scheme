using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.PlanningPermission;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;
using HE.Remediation.WebApp.ViewModels.ProgressReporting;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.MonthlyProgressReporting.MonthlyProgressReportingKeyDates.Controllers;

[Area("MonthlyProgressReportingKeyDates")]
[Route("MonthlyProgressReporting/PlanningPermission")]
[CookieApplicationAuthorise]
public class PlanningPermissionController : StartController
{
        private readonly ISender _sender;
        private readonly IMapper _mapper;

    public PlanningPermissionController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetPlanningPermissionRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<PlanningPermissionViewModel>(response);
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Index(PlanningPermissionViewModel viewModel, CancellationToken cancellationToken)
    {
        var validator = new PlanningPermissionViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View(viewModel);
        }

        var request = _mapper.Map<SetPlanningPermissionRequest>(viewModel);
        var response = await _sender.Send(request, cancellationToken);

        if (request.WorksNeedPlanningPermission != 1)
        {
            return RedirectToAction("Index", "KeyDates", new { Area = "MonthlyProgressReportingKeyDates" });
        }
        
        return RedirectToAction("HaveYouAppliedPlanningPermission", "PlanningPermission", new { Area = "MonthlyProgressReportingKeyDates" });
    }

    #region Have you applied for planning permission?
    [HttpGet("HaveYouAppliedPlanningPermission")]
    public async Task<IActionResult> HaveYouAppliedPlanningPermission(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetHaveYouAppliedPlanningPermissionRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<HaveYouAppliedPlanningPermissionViewModel>(response);
        return View(viewModel);
    }

    [HttpPost("HaveYouAppliedPlanningPermission")]
    public async Task<IActionResult> HaveYouAppliedPlanningPermission(HaveYouAppliedPlanningPermissionViewModel viewModel, CancellationToken cancellationToken)
    {
        var validator = new HaveYouAppliedPlanningPermissionViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View(viewModel);
        }

        var request = _mapper.Map<SetHaveYouAppliedPlanningPermissionRequest>(viewModel);
        var response = await _sender.Send(request, cancellationToken);

        return request.HaveAppliedPlanningPermission == true
            ? RedirectToAction("TellUsAboutPlanningPermission", "PlanningPermission", new { Area = "MonthlyProgressReportingKeyDates" })
            : RedirectToAction("ReasonNotAppliedPlanningPermission", "PlanningPermission", new { Area = "MonthlyProgressReportingKeyDates" });

    }
    #endregion

    #region Why have you not applied for a planning permission?
    [HttpGet("ReasonNotAppliedPlanningPermission")]
    public async Task<IActionResult> ReasonNotAppliedPlanningPermission(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetReasonNotAppliedPlanningPermissionRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<ReasonNotAppliedPlanningPermissionViewModel>(response);
        return View(viewModel);
    }

    [HttpPost("ReasonNotAppliedPlanningPermission")]
    public async Task<IActionResult> ReasonNotAppliedPlanningPermission(ReasonNotAppliedPlanningPermissionViewModel viewModel, CancellationToken cancellationToken)
    {
        var validator = new ReasonNotAppliedPlanningPermissionViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View(viewModel);
        }
        var request = _mapper.Map<SetReasonNotAppliedPlanningPermissionRequest>(viewModel);
        var response = await _sender.Send(request, cancellationToken);

        return viewModel.PreviousPlanToSubmitDate.HasValue &&
               viewModel.PlanToSubmitDate != viewModel.PreviousPlanToSubmitDate
            ? RedirectToAction("DatesChanged", "PlanningPermission", new { Area = "MonthlyProgressReportingKeyDates" })
            : RedirectToAction("Index", "KeyDates", new { Area = "MonthlyProgressReportingKeyDates" });
    }
    #endregion

    #region Tell us about your planning permission
    [HttpGet("TellUsAboutPlanningPermission")]
    public async Task<IActionResult> TellUsAboutPlanningPermission(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetTellUsAboutPlanningPermissionRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<TellUsAboutPlanningPermissionViewModel>(response);
        return View(viewModel);
    }
    [HttpPost("TellUsAboutPlanningPermission")]
    public async Task<IActionResult> TellUsAboutPlanningPermission(TellUsAboutPlanningPermissionViewModel viewModel, CancellationToken cancellationToken)
    {
        var validator = new TellUsAboutPlanningPermissionViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View(viewModel);
        }
        var request = _mapper.Map<SetTellUsAboutPlanningPermissionRequest>(viewModel);
        var response = await _sender.Send(request, cancellationToken);

        return response.HasChangedDates
                ? RedirectToAction("DatesChanged")
                : RedirectToAction("Index", "KeyDates");
    }
    #endregion

    [HttpGet(nameof(DatesChanged))]
    public async Task<IActionResult> DatesChanged(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetPlanningPermissionDatesChangedRequest.Request, cancellationToken);
        var model = _mapper.Map<PlanningPermissionDatesChangedViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(DatesChanged))]
    public async Task<IActionResult> DatesChanged(PlanningPermissionDatesChangedViewModel model, CancellationToken cancellationToken)
    {
        var validator = new PlanningPermissionDatesChangedViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View(model);
        }

        var request = _mapper.Map<SetPlanningPermissionDatesChangedRequest>(model);
        await _sender.Send(request, cancellationToken);

        return RedirectToAction("Index", "KeyDates");
    }

    protected override IActionResult DefaultStart => RedirectToAction("Index", "PlanningPermission", new { Area = "MonthlyProgressReportingKeyDates" });
}

