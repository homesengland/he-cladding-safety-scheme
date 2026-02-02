using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.BaseInformation.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackagePlanningPermission.CheckYourAnswers.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackagePlanningPermission.CheckYourAnswers.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackagePlanningPermission.Reset;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackagePlanningPermission.StartInformation.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackagePlanningPermission.WorksRequirePermission.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackagePlanningPermission.WorksRequirePermission.Set;
using HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackagePlanningPermission;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.WorksPackage.WorksPackagePlanningPermission.Controllers;

[Area("WorksPackagePlanningPermission")]
[Route("WorksPackage/PlanningPermission")]
public class PlanningPermissionController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public PlanningPermissionController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart =>
        RedirectToAction("StartInformation", "PlanningPermission", new { Area = "WorksPackagePlanningPermission" });

    #region Start Information

    [HttpGet(nameof(StartInformation))]
    public async Task<IActionResult> StartInformation()
    {
        var response = await _sender.Send(GetStartInformationRequest.Request);
        var viewModel = _mapper.Map<StartInformationViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(StartInformation))]
    public IActionResult StartInformation(StartInformationViewModel viewModel,
        ESubmitAction submitAction)
    {
        return RedirectToAction("WorksRequirePermission", "PlanningPermission", new { Area = "WorksPackage" });
    }

    #endregion

    #region Works require planning permission

    [HttpGet(nameof(WorksRequirePermission))]
    public async Task<IActionResult> WorksRequirePermission()
    {
        var response = await _sender.Send(GetWorksRequirePermissionRequest.Request);
        var viewModel = _mapper.Map<WorksRequirePermissionViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(WorksRequirePermission))]
    public async Task<IActionResult> WorksRequirePermission(WorksRequirePermissionViewModel viewModel,
        ESubmitAction submitAction)
    {
        var validator = new WorksRequirePermissionViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var useCaseRequest = _mapper.Map<SetWorksRequirePermissionRequest>(viewModel);
        await _sender.Send(useCaseRequest);

        return submitAction == ESubmitAction.Continue
            ? RedirectToAction("CheckYourAnswers", "PlanningPermission", new { Area = "WorksPackage" })
            : RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
    }

    #endregion

    #region Check Your Answers

    [HttpGet(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers()
    {
        var response = await _sender.Send(GetCheckYourAnswersRequest.Request);
        var viewModel = _mapper.Map<CheckYourAnswersViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers(CheckYourAnswersViewModel viewModel,
        ESubmitAction submitAction)
    {
        if (submitAction == ESubmitAction.Continue)
        {
            var useCaseRequest = SetCheckYourAnswersRequest.Request;
            await _sender.Send(useCaseRequest);
            
            return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
        }

        return View(viewModel);
    }

    #endregion
    
    #region ChangeAnswers

    [HttpGet(nameof(ChangeAnswers))]
    public async Task<IActionResult> ChangeAnswers()
    {
        var response = await _sender.Send(GetBaseInformationRequest.Request);
        var model = _mapper.Map<ChangeAnswersViewModel>(response);

        return View(model);
    }

    [HttpPost(nameof(ChangeAnswers))]
    public async Task<IActionResult> ChangeAnswers(ChangeAnswersViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (model.Proceed == false)
        {
            return RedirectToAction("CheckYourAnswers");
        }

        await _sender.Send(new ResetRequest(), cancellationToken);

        return RedirectToAction("StartInformation");
    }
    #endregion
}