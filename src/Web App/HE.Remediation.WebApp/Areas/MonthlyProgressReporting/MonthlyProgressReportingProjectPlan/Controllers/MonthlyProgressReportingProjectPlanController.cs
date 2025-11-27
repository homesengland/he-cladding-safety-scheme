using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectPlan;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.Constants;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectPlan;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectPlan.CheckYourAnswers;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectPlan.Upload;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectPlan.Controllers;

[Area("MonthlyProgressReportingProjectPlan")]
[Route("MonthlyProgressReporting/ProjectPlan")]
[CookieApplicationAuthorise]
public class MonthlyProgressReportingProjectPlanController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public MonthlyProgressReportingProjectPlanController(ISender sender, IMapper mapper)
     : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart => RedirectToAction("ProjectPlan", "MonthlyProgressReportingProjectPlan", new { Area = "MonthlyProgressReportingProjectPlan" });

    #region Project Plan
    [HttpGet("ProjectPlan")]
    public async Task<IActionResult> ProjectPlan(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetProjectPlanRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<ProjectPlanViewModel>(response);

        return View(viewModel);
    }

    [HttpPost("ProjectPlan")]
    public async Task<IActionResult> ProjectPlan(ProjectPlanViewModel viewModel)
    {
        var validator = new ProjectPlanViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }
        var request = _mapper.Map<SetProjectPlanRequest>(viewModel);
        await _sender.Send(request);

        if (viewModel.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("TaskList", "MonthlyProgressReporting", new { Area = "MonthlyProgressReporting" });
        }

        return viewModel.EnoughFunds == true
            ? RedirectToAction("UploadProjectPlan", "MonthlyProgressReportingProjectPlan", new { Area = "MonthlyProgressReportingProjectPlan" })
            : RedirectToAction("PtsUplift", "MonthlyProgressReportingProjectPlan", new { Area = "MonthlyProgressReportingProjectPlan" });
    }
    #endregion

    #region PTS Uplift

    [HttpGet(nameof(PtsUplift))]
    public async Task<IActionResult> PtsUplift(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetPtsUpliftRequest.Request, cancellationToken);
        var model = _mapper.Map<PtsUpliftViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(PtsUplift))]
    [RequestSizeLimit(FileUploadConstants.MaxRequestSize50MB)]
    public async Task<IActionResult> PtsUplift(PtsUpliftViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(nameof(model.File), "File exceeds 50mb limit. Please upload an alternate file.");
            var response = await _sender.Send(GetPtsUpliftRequest.Request, cancellationToken);
            model = _mapper.Map<PtsUpliftViewModel>(response);
            return View(model);
        }

        var validator = new PtsUpliftViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        try
        {
            var request = _mapper.Map<SetPtsUpliftRequest>(model);
            await _sender.Send(request, cancellationToken);
        }
        catch (InvalidFileException ex)
        {
            ModelState.AddModelError(nameof(model.File), ex.Message);
            return View(model);
        }

        return model.SubmitAction == ESubmitAction.Continue
            ? RedirectToAction("UploadProjectPlan", "MonthlyProgressReportingProjectPlan", new { Area = "MonthlyProgressReportingProjectPlan" })
            : RedirectToAction("TaskList", "MonthlyProgressReporting", new { Area = "MonthlyProgressReporting" });
    }

    [HttpGet(nameof(PtsUplift) + "/Delete")]
    public async Task<IActionResult> DeletePtsUplift([FromQuery] DeletePtsUpliftRequest request, CancellationToken cancellationToken)
    {
        await _sender.Send(request, cancellationToken);
        return RedirectToAction("PtsUplift", "MonthlyProgressReportingProjectPlan", new { Area = "MonthlyProgressReportingProjectPlan" });
    }

    #endregion 

    #region Upload Project Plan
    [HttpGet(nameof(UploadProjectPlan))]
    public async Task<IActionResult> UploadProjectPlan()
    {
        var response = await _sender.Send(GetUploadProjectPlanRequest.Request);
        var viewModel = _mapper.Map<UploadProjectPlanViewModel>(response);
        return View(viewModel);
    }

    [HttpPost(nameof(UploadProjectPlan))]
    [RequestSizeLimit(FileUploadConstants.MaxRequestSize50MB)]
    public async Task<IActionResult> UploadProjectPlan(UploadProjectPlanViewModel viewModel, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid || viewModel == null)
        {
            ModelState.AddModelError(nameof(viewModel.File), "File exceeds 50mb limit. Please upload an alternate file.");
            var response = await _sender.Send(GetUploadProjectPlanRequest.Request, cancellationToken);
            var reloadedViewModel = _mapper.Map<UploadProjectPlanViewModel>(response);
            return View(reloadedViewModel);
        }

        var validator = new UploadProjectPlanViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetUploadProjectPlanRequest>(viewModel);
        try
        {
            await _sender.Send(request, cancellationToken);
        }
        catch (InvalidFileException ex)
        {
            ModelState.AddModelError(nameof(request.File), ex.Message);
            return View(viewModel);
        }
        
        return viewModel.SubmitAction == ESubmitAction.Continue 
            ? RedirectToAction("CheckYourAnswers", "MonthlyProgressReportingProjectPlan", new { Area = "MonthlyProgressReportingProjectPlan" }) 
            : RedirectToAction("TaskList", "MonthlyProgressReporting", new { Area = "MonthlyProgressReporting" });
    }

    [HttpGet(nameof(UploadProjectPlan) + "/Delete")]
    public async Task<IActionResult> UploadProjectPlanDelete([FromQuery] DeleteUploadProjectPlanRequest request)
    {
        await _sender.Send(request);
        return RedirectToAction("UploadProjectPlan", "MonthlyProgressReportingProjectPlan", new { Area = "MonthlyProgressReportingProjectPlan" });
    }

    #endregion

    #region CheckYourAnswers
    [HttpGet(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetMonthlyProgressReportProjectPlanCheckYourAnswersRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<MonthlyProgressReportProjectPlanCheckYourAnswersViewModel>(response);

        return View(viewModel);
    }

    [HttpPost(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers(MonthlyProgressReportProjectPlanCheckYourAnswersViewModel viewModel, CancellationToken cancellationToken)
    {
        await _sender.Send(SetMonthlyProgressReportProjectPlanCheckYourAnswersRequest.Request, cancellationToken);

        return RedirectToAction("TaskList", "MonthlyProgressReporting", new { Area = "MonthlyProgressReporting" });
    }
    #endregion
}