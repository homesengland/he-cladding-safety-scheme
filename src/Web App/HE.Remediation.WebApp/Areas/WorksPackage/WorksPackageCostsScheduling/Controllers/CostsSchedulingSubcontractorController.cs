using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsSchedulingSubcontractor.Remove.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsSchedulingSubcontractor.Remove.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsSchedulingSubcontractor.Subcontractor.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsSchedulingSubcontractor.Subcontractor.Set;
using HE.Remediation.WebApp.Attributes.Routing;
using HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.WorksPackage.WorksPackageCostsScheduling.Controllers;

[Area("WorksPackageCostsScheduling")]
[Route("WorksPackage/CostsSchedulingSubcontractor")]
public class CostsSchedulingSubcontractorController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public CostsSchedulingSubcontractorController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart =>
        RedirectToAction("Subcontractor", "CostsSchedulingSubcontractor", new { Area = "WorksPackageCostsScheduling" });

    #region Subcontractor

    [ExcludeRouteRecording]
    [HttpGet("Subcontractor/{subcontractorId:guid?}")]
    public async Task<IActionResult> Subcontractor(
        GetCostsSchedulingSubcontractorRequest request,
        [FromQuery] string returnUrl = null)
    {
        try
        {
            var response = await _sender.Send(request);
            var model = _mapper.Map<SubcontractorViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }
        catch (EntityNotFoundException)
        {
            throw new EntityNotFoundException("Subcontractor not found");
        }
    }

    [HttpPost("Subcontractor/{subcontractorId:guid?}")]
    public async Task<IActionResult> Subcontractor(SubcontractorViewModel model)
    {
        var validator = new SubcontractorViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetCostsSchedulingSubcontractorRequest>(model);
        await _sender.Send(request);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("SubcontractorTeam", "CostsScheduling", new { Area = "WorksPackageCostsScheduling" });
        }

        return !string.IsNullOrEmpty(model.ReturnUrl)
            ? SafeRedirectToAction(model.ReturnUrl, null, null)
            : RedirectToAction("SubcontractorTeam", "CostsScheduling", new { Area = "WorksPackageCostsScheduling" });
    }

    #endregion

    #region Remove Member

    [ExcludeRouteRecording]
    [HttpGet(nameof(Remove))]
    public async Task<IActionResult> Remove(Guid subcontractorId)
    {
        var response = await _sender.Send(GetRemoveSubcontractorRequest.Request);
        var viewModel = _mapper.Map<RemoveSubcontractorViewModel>(response);

        viewModel.SubcontractorId = subcontractorId;

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(Remove))]
    public async Task<IActionResult> Remove(RemoveSubcontractorViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new RemoveSubcontractorViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, String.Empty);

            return View(viewModel);
        }

        if (viewModel.Confirm == true)
        {
            var request = _mapper.Map<DeleteSubcontractorRequest>(viewModel);
            await _sender.Send(request);
        }

        if (viewModel.ReturnUrl is not null)
        {
            return SafeRedirectToAction(viewModel.ReturnUrl, null, null);
        }

        return viewModel.SubmitAction == ESubmitAction.Continue
            ? RedirectToAction("SubcontractorTeam", "CostsScheduling", new { Area = "WorksPackageCostsScheduling" })
            : RedirectToAction("Index", "TaskList", new { area = "WorksPackage" });
    }

    #endregion
}