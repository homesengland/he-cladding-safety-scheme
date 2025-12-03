using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.WorksPlanning;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.MonthlyProgressReporting.MonthlyProgressReportingKeyDates.Controllers;
[Area("MonthlyProgressReportingKeyDates")]
[Route("MonthlyProgressReporting/WorksPlanning")]
[CookieApplicationAuthorise]
public class WorksPlanningController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public WorksPlanningController(ISender sender, IMapper mapper)
    :base (sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetWorksPlanningRequest.Request, cancellationToken);
        var model = _mapper.Map<WorksPlanningViewModel>(response);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Index(WorksPlanningViewModel model, CancellationToken cancellationToken)
    {
        var validator = new WorksPlanningViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View(model);
        }

        var request = _mapper.Map<SetWorksPlanningRequest>(model);
        var response = await _sender.Send(request, cancellationToken);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("TaskList", "MonthlyProgressReporting", new { Area = "MonthlyProgressReporting" });
        }

        return response.HasChangedDates
            ? RedirectToAction("DatesChanged", "WorksPlanning", new { Area = "MonthlyProgressReportingKeyDates" })
            : RedirectToAction("ContractorTender", "WorksPlanning", new { Area = "MonthlyProgressReportingKeyDates" });
    }

    [HttpGet(nameof(DatesChanged))]
    public async Task<IActionResult> DatesChanged(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetWorksPlanningDatesChangedRequest.Request, cancellationToken);
        var model = _mapper.Map<WorksPlanningDatesChangedViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(DatesChanged))]
    public async Task<IActionResult> DatesChanged(WorksPlanningDatesChangedViewModel model , CancellationToken cancellationToken)
    {
        var validator = new WorksPlanningDatesChangedViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View(model);
        }

        var request = _mapper.Map<SetWorksPlanningDatesChangedRequest>(model);
        await _sender.Send(request, cancellationToken);

        return model.SubmitAction == ESubmitAction.Exit
            ? RedirectToAction("TaskList", "MonthlyProgressReporting", new { Area = "MonthlyProgressReporting" })
            : RedirectToAction("ContractorTender", "WorksPlanning", new { Area = "MonthlyProgressReportingKeyDates" });
    }

    #region Contractor Tender
    [HttpGet("ContractorTender")]
    public async Task<IActionResult> ContractorTender(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetContractorTenderRequest.Request, cancellationToken);
        var model = _mapper.Map<ContractorTenderViewModel>(response);
        return View(model);
    }

    [HttpPost("ContractorTender")]
    public async Task<IActionResult> ContractorTender(ContractorTenderViewModel model, CancellationToken cancellationToken)
    {
        var validator = new ContractorTenderViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View(model);
        }
        var request = _mapper.Map<SetContractorTenderRequest>(model);
        await _sender.Send(request, cancellationToken);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("TaskList", "MonthlyProgressReporting", new { Area = "MonthlyProgressReporting" });
        }

        if (model.SubmitAction == ESubmitAction.Continue && model.ContractorTenderType == EContractorTenderType.NonCompetitive)
        {
            return RedirectToAction("ReasonForNonCompetitiveTender", "WorksPlanning", new { Area = "MonthlyProgressReportingKeyDates" });
        }

        return RedirectToAction("Index", "KeyDates", new { Area = "MonthlyProgressReportingKeyDates" });
    }
    #endregion

    #region Reason For Non Competitive Tender
    [HttpGet("ReasonForNonCompetitiveTender")]
    public async Task<IActionResult> ReasonForNonCompetitiveTender(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetReasonForNonCompetitiveTenderRequest.Request, cancellationToken);
        var model = _mapper.Map<ReasonForNonCompetitiveTenderViewModel>(response);
        return View(model);
    }

    [HttpPost("ReasonForNonCompetitiveTender")]
    public async Task<IActionResult> ReasonForNonCompetitiveTender(ReasonForNonCompetitiveTenderViewModel model, CancellationToken cancellationToken)
    {
        var validator = new ReasonForNonCompetitiveTenderViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View(model);
        }
        var request = _mapper.Map<SetReasonForNonCompetitiveTenderRequest>(model);
        await _sender.Send(request, cancellationToken);
        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("TaskList", "MonthlyProgressReporting", new { Area = "MonthlyProgressReporting" });
        }
        return RedirectToAction("Index", "KeyDates", new { Area = "MonthlyProgressReportingKeyDates" });
    }
    #endregion

    protected override IActionResult DefaultStart => RedirectToAction("Index", "WorksPlanning", new { Area = "MonthlyProgressReportingKeyDates" });
}