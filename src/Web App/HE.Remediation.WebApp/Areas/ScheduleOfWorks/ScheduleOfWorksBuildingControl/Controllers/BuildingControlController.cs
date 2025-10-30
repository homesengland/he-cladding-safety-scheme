using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BuildingControlCheckYourAnswers.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ApprovalDateGateWayTwoApplication.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ApprovalDateGateWayTwoApplication.Set;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BaseInformation.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BuildingSafetyRegulator.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BuildingSafetyRegulator.Set;
using HE.Remediation.WebApp.Constants;
using HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BuildingControlCheckYourAnswers.Set;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace HE.Remediation.WebApp.Areas.ScheduleOfWorks.ScheduleOfWorksBuildingControl.Controllers;

[Area("ScheduleOfWorksBuildingControl")]
[Route("ScheduleOfWorks/BuildingControl")]
public class BuildingControlController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public BuildingControlController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }
    protected override IActionResult DefaultStart =>
     RedirectToAction("StartInformation", "BuildingControl", new { Area = "ScheduleOfWorksBuildingControl" });

    #region Start Information

    [HttpGet(nameof(StartInformation))]
    public async Task<IActionResult> StartInformation()
    {
        var response = await _sender.Send(GetBaseInformationRequest.Request);
        var viewModel = _mapper.Map<BuildingControlStartInformationViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(StartInformation))]
    public IActionResult StartInformation(StartInformationViewModel viewModel)
    {
        return RedirectToAction("BuildingControlApproval", "BuildingControl", new { Area = "ScheduleOfWorksBuildingControl" });
    }

    #endregion

    #region Building Control Approval 

    [HttpGet(nameof(BuildingControlApproval))]
    public async Task<IActionResult> BuildingControlApproval(string returnUrl, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetBuildingControlApprovalRequest.Request, cancellationToken);
        var model = _mapper.Map<BuildingControlApprovalViewModel>(response);
        model.ReturnUrl = returnUrl;
        return View(model);
    }

    [HttpPost(nameof(BuildingControlApproval))]
    public async Task<IActionResult> BuildingControlApproval(BuildingControlApprovalViewModel model, CancellationToken cancellationToken)
    {
        var validator = new BuildingControlApprovalViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }


        if (model.SubmitAction == ESubmitAction.Continue)
        {
            var request = _mapper.Map<SetBuildingControlApprovalRequest>(model);
            await _sender.Send(request, cancellationToken);

            model.ReturnUrl = Url.Action("BuildingControlApproval", "BuildingControl", new { Area = "ScheduleOfWorksBuildingControl" });

            return model.IsBuildingControlApprovalApplied == ENoYes.Yes
               ? RedirectToAction("ApprovalDate", "BuildingControl", new { Area = "ScheduleOfWorksBuildingControl" })
               : RedirectToAction("UploadBuildingControl", "BuildingControl", new { Area = "ScheduleOfWorksBuildingControl"});
        }

        return RedirectToAction("TaskList", "ScheduleOfWorks", new { area = "ScheduleOfWorks" });
    }



    #endregion

    #region Building Control Approval Date

    [HttpGet(nameof(ApprovalDate))]
    public async Task<IActionResult> ApprovalDate(string returnUrl, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetApprovalDateRequest.Request, cancellationToken);
        var model = _mapper.Map<BuildingControlApprovalDateViewModel>(response);
        model.ReturnUrl = returnUrl;
        return View(model);
    }

    [HttpPost(nameof(ApprovalDate))]
    public async Task<IActionResult> ApprovalDate(BuildingControlApprovalDateViewModel model, CancellationToken cancellationToken)
    {
        var validator = new BuildingControlApprovalDateViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }


        if (model.SubmitAction == ESubmitAction.Continue)
        {

            var request = _mapper.Map<SetApprovalDateRequest>(model);
            await _sender.Send(request, cancellationToken);
            return RedirectToAction("UploadBuildingControl", "BuildingControl", new { Area = "ScheduleOfWorksBuildingControl" });
        }

        return RedirectToAction("TaskList", "ScheduleOfWorks", new { area = "ScheduleOfWorks" });
    }

    #endregion

    #region Upload Building Control

    [HttpGet(nameof(UploadBuildingControl))]
    public async Task<IActionResult> UploadBuildingControl(string returnUrl, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetBuildingControlRequest.Request, cancellationToken);
        var model = _mapper.Map<UploadBuildingControlViewModel>(response);
        model.ReturnUrl = returnUrl;
        return View(model);
    }

    [HttpPost(nameof(UploadBuildingControl))]
    [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
    public async Task<IActionResult> UploadBuildingControl(UploadBuildingControlViewModel model, CancellationToken cancellationToken)
    {
        var validator = new UploadBuildingControlViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        if (model.SubmitAction == ESubmitAction.Upload)
        {
            try
            {
                var request = _mapper.Map<AddBuildingControlFileRequest>(model);
                await _sender.Send(request, cancellationToken);
                return RedirectToAction("UploadBuildingControl", "BuildingControl", new { Area = "ScheduleOfWorksBuildingControl", model.ReturnUrl });
            }
            catch (InvalidFileException ex)
            {
                ModelState.AddModelError(nameof(model.File), ex.Message);
                return View(model);
            }
        }

        if (model.SubmitAction == ESubmitAction.Continue)
        {
            return !string.IsNullOrWhiteSpace(model.ReturnUrl)
                ? RedirectToAction(model.ReturnUrl, "BuildingControl", new { Area = "ScheduleOfWorksBuildingControl" })
                : RedirectToAction("CheckYourAnswers", "BuildingControl", new { Area = "ScheduleOfWorksBuildingControl" });
        }

        return RedirectToAction("TaskList", "ScheduleOfWorks", new { area = "ScheduleOfWorks" });
    }

    [HttpGet(nameof(UploadBuildingControl) + "/Delete")]
    public async Task<IActionResult> DeleteBuildingControl(
        [FromQuery] DeleteBuildingControlFileRequest request,
        [FromQuery] string returnUrl,
        CancellationToken cancellationToken)
    {
        await _sender.Send(request, cancellationToken);
        return !string.IsNullOrWhiteSpace(returnUrl)
            ? RedirectToAction("UploadBuildingControl", "BuildingControl", new { Area = "ScheduleOfWorksBuildingControl", returnUrl })
            : RedirectToAction("UploadBuildingControl", "BuildingControl", new { Area = "ScheduleOfWorksBuildingControl" });
    }

    #endregion

    #region Check Your Answers

    [HttpGet(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetCheckYourAnswersRequest.Request, cancellationToken);
        var model = _mapper.Map<BuildingControlCheckYourAnswersViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers(CheckYourAnswersViewModel viewModel)
    {
        await _sender.Send(SetCheckYourAnswersRequest.Request);
        return RedirectToAction("TaskList", "ScheduleOfWorks", new { area = "ScheduleOfWorks" });
    }
    #endregion
}