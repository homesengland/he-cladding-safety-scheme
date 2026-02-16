using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetClosingReportInformation;
using HE.Remediation.WebApp.ViewModels.ClosingReport;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetFinalCheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetNeedVariations;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.SetNeedVariations;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.TaskList.GetTaskList;

namespace HE.Remediation.WebApp.Areas.ClosingReport.Controllers;

[Area("ClosingReport")]
[Route("ClosingReport")]
public class ClosingReportController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ClosingReportController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart =>
        RedirectToAction("Information", "ClosingReport", new {Area = "ClosingReport"});

    [HttpGet(nameof(Information))]
    public async Task<IActionResult> Information()
    {
        var response = await _sender.Send(GetClosingReportInformationRequest.Request);
        var model = new ClosingReportInformationViewModel
        {
            IsSubmitted = response.IsSubmitted,
            ApplicationReferenceNumber = response.ApplicationReferenceNumber,
            BuildingName = response.BuildingName
        };
        return View(model);
    }

    #region Need Variations

    [HttpGet(nameof(NeedVariations))]
    public async Task<IActionResult> NeedVariations()
    {
        var response = await _sender.Send(GetNeedVariationsRequest.Request);
        var viewModel = _mapper.Map<NeedVariationsViewModel>(response);
        return View(viewModel);
    }

    [HttpPost(nameof(NeedVariations))]
    public async Task<IActionResult> NeedVariations(NeedVariationsViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new NeedVariationsViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        if (viewModel?.NeedVariations == false)
        {
            return RedirectToAction("Information", "ClosingReport");
        }

        var request = _mapper.Map<SetNeedVariationsRequest>(viewModel);
        await _sender.Send(request);

        return submitAction == ESubmitAction.Continue
            ? RedirectToAction("TaskList", "ClosingReport")
            : RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
    }


    #endregion

    #region Task List

    [HttpGet(nameof(TaskList))]
    public async Task<IActionResult> TaskList()
    {
        var response = await _sender.Send(GetTaskListRequest.Request);
        var viewModel = _mapper.Map<TaskListViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    #endregion

    #region Check Your Answers

    [HttpGet(nameof(FinalCheckYourAnswers))]
    public async Task<IActionResult> FinalCheckYourAnswers()
    {
        var response = await _sender.Send(GetFinalCheckYourAnswersRequest.Request);
        var model = _mapper.Map<FinalCheckYourAnswersViewModel>(response);
        return View(model);
    }

    #endregion
}