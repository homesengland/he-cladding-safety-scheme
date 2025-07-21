using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.ConfirmToProceed.GetConfirmToProceed;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.ConfirmToProceed.SetConfirmToProceed;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.TaskList.GetTaskList;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.ViewModels.WorksPackage;
using HE.Remediation.WebApp.ViewModels.WorksPackage.ConfirmToProceed;
using MediatR;

using Microsoft.AspNetCore.Mvc;

using System.Reflection;

namespace HE.Remediation.WebApp.Areas.WorksPackage.Controllers;

[Area("WorksPackage")]
[Route("WorksPackage")]
[CookieApplicationAuthorise]
public class WorkPackageController : Controller
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    public WorkPackageController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    #region ConfirmToProceed

    [HttpGet(nameof(ConfirmToProceed))]
    public async Task<IActionResult> ConfirmToProceed()
    {
        var response = await _sender.Send(GetWorkPackageConfirmToProceedRequest.Request);
        var viewModel = _mapper.Map<ConfirmToProceedViewModel>(response);

        if (viewModel.IsConfirmedToProceed == ENoYes.Yes)
            return RedirectToAction("TaskList");

        return View(viewModel);
    }

    [HttpPost("ConfirmToProceed")]
    public async Task<IActionResult> ConfirmToProceed(ConfirmToProceedViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new ConfirmToProceedViewModelValidtor();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }
        var request = _mapper.Map<SetWorkPackageConfirmToProceedRequest>(viewModel);
        await _sender.Send(request);

        return viewModel.IsConfirmedToProceed == ENoYes.Yes
            ? RedirectToAction("TaskList")
            : RedirectToAction("Index", "StageDiagram", new { area = "Application" });

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

    [HttpPost(nameof(TaskList))]
    public IActionResult TaskList(TaskListViewModel viewModel, ESubmitAction submitAction)
    {
        return View(viewModel);
    }

    #endregion
}
