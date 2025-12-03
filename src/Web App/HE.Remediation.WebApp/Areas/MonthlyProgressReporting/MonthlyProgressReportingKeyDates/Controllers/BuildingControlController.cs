using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.BuildingControl;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.Attributes.Routing;
using HE.Remediation.WebApp.Constants;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates.BuildingControl;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.MonthlyProgressReporting.MonthlyProgressReportingKeyDates.Controllers;

[Area("MonthlyProgressReportingKeyDates")]
[Route("MonthlyProgressReporting/BuildingControl")]
[CookieApplicationAuthorise]
[AreaRedirect]
[RecordRoute]
public class BuildingControlController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public BuildingControlController(ISender sender, IMapper mapper)
     : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var response = await _sender.Send(new GetBuildingControlRequest());
        var viewModel = _mapper.Map<BuildingControlViewModel>(response);
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Index(BuildingControlViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (model.SubmitAction == ESubmitAction.Continue || model.SubmitAction == ESubmitAction.Exit)
        {
            var validator = new BuildingControlViewModelValidator();

            var validationResult = await validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return View(model);
            }

            var request = _mapper.Map<SetBuildingControlRequest>(model);
            var response = await _sender.Send(request, cancellationToken);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "KeyDates");
            }

            if (model.BuildingControlDecisionType is EBuildingControlDecisionType.ApproveWithRecommendations
                or EBuildingControlDecisionType.Rejected)
            {
                return RedirectToAction("UploadBuildingControl", "BuildingControl", new { Area = "MonthlyProgressReportingKeyDates" });
            }

            return response.HasChangedDates
                ? RedirectToAction("DatesChanged")
                : RedirectToAction("Index", "KeyDates");
        }

        return RedirectToAction("Index", "KeyDates");
    }

    [HttpGet(nameof(DatesChanged))]
    public async Task<IActionResult> DatesChanged(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetBuildingControlDatesChangedRequest.Request, cancellationToken);
        var model = _mapper.Map<BuildingControlDatesChangedViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(DatesChanged))]
    public async Task<IActionResult> DatesChanged(BuildingControlDatesChangedViewModel model, CancellationToken cancellationToken)
    {
        var validator = new BuildingControlDatesChangedViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View(model);
        }

        var request = _mapper.Map<SetBuildingControlDatesChangedRequest>(model);
        await _sender.Send(request, cancellationToken);

        return model.SubmitAction == ESubmitAction.Exit
            ? RedirectToAction("TaskList", "MonthlyProgressReporting")
            : RedirectToAction("Index", "KeyDates");
    }

    #region UploadBuildingControl
    [HttpGet(nameof(UploadBuildingControl))]
    public async Task<IActionResult> UploadBuildingControl(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetUploadBuildingControlRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<UploadBuildingControlViewModel>(response);
        return View(viewModel);
    }

    [HttpPost(nameof(UploadBuildingControl))]
    [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
    public async Task<IActionResult> UploadBuildingControl(UploadBuildingControlViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var response = await _sender.Send(GetUploadBuildingControlRequest.Request, cancellationToken);
            var viewModel = _mapper.Map<UploadBuildingControlViewModel>(response);
            return View(viewModel);
        }

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
                var request = _mapper.Map<SetUploadBuildingControlRequest>(model);
                await _sender.Send(request, cancellationToken);
                return RedirectToAction("UploadBuildingControl", "BuildingControl", new { Area = "MonthlyProgressReportingKeyDates" });
            }
            catch (InvalidFileException e)
            {
                ModelState.AddModelError(nameof(model.File), e.Message);
                return View(model);
            }
        }

        return RedirectToAction("Index", "KeyDates", new { Area = "MonthlyProgressReportingKeyDates" });
    }

    [HttpGet(nameof(UploadBuildingControl) + "/Delete")]
    public async Task<IActionResult> UploadBuildingControlDelete([FromQuery] DeleteUploadBuildingControlRequest request)
    {
        await _sender.Send(request);
        return RedirectToAction("UploadBuildingControl");
    }
    #endregion

    protected override IActionResult DefaultStart => RedirectToAction("Index", "BuildingControl", new { Area = "MonthlyProgressReportingKeyDates" });
}