using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectSupport;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectSupport;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectSupport;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectSupport.Controllers;

[Area("MonthlyProgressReportingProjectSupport")]
[Route("MonthlyProgressReporting/Support")]
[CookieApplicationAuthorise]
public class MonthlyProgressReportingProjectSupportController : Controller
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    protected ETaskStatus TaskStatus => ETaskStatus.InProgress;

    public MonthlyProgressReportingProjectSupportController(ISender sender, IMapper mapper, IApplicationDataProvider applicationDataProvider)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet("ProjectSupport")]
    public async Task<IActionResult> ProjectSupport()
    {
        var response = await _sender.Send(new GetProjectSupportRequest());
        var viewModel = new ProjectSupportViewModel
        {
            Id = response?.Id,
            BuildingName = response?.BuildingName,
            ApplicationReferenceNumber = response?.ApplicationReferenceNumber,
            ApplicationId = response.ApplicationId,
            RequiresSupport = response.RequiresSupport,
            TaskStatusId = response?.TaskStatusId
        };

        return View(viewModel);
    }

    [HttpPost("ProjectSupport")]
    public async Task<IActionResult> ProjectSupport(ProjectSupportViewModel model)
    {
        var validator = new ProjectSupportViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }
        var request = _mapper.Map<SetProjectSupportRequest>(model);
        await _sender.Send(request);

        if (request.RequiresSupport == ENoYes.Yes)
        {
            return RedirectToAction("ProjectSupportType", "MonthlyProgressReportingProjectSupport", new { Area = "MonthlyProgressReportingProjectSupport" });
        }
        else
        {
            return RedirectToAction("CheckYourAnswers", "MonthlyProgressReportingProjectSupport", new { Area = "MonthlyProgressReportingProjectSupport" });
        }
    }

    #region Project Support Type
    [HttpGet("ProjectSupportType")]
    public async Task<IActionResult> ProjectSupportType()
    {
        var response = await _sender.Send(GetProgressSupportTypeRequest.Request);
        var model = _mapper.Map<ProgressSupportTypeViewModel>(response);

        return View(model);
    }

    [HttpPost("ProjectSupportType")]
    public async Task<IActionResult> ProjectSupportType(ProgressSupportTypeViewModel model)
    {
        var validator = new ProgressSupportTypeViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }
        var request = _mapper.Map<SetProgressSupportTypeRequest>(model);
        await _sender.Send(request);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("TaskList", "MonthlyProgressReporting", new { Area = "MonthlyProgressReporting" });
        }
        return RedirectToAction("CheckYourAnswers", "MonthlyProgressReportingProjectSupport", new { Area = "MonthlyProgressReportingProjectSupport" });
    }
    #endregion

    #region Check Your Answers
    [HttpGet(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetProjectSupportCheckYourAnswersRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<ProjectSupportCheckYourAnswersViewModel>(response);

        return View(viewModel);
    }

    [HttpPost(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers(ProjectSupportCheckYourAnswersViewModel viewModel, CancellationToken cancellationToken)
    {
        await _sender.Send(SetProjectSupportCheckYourAnswersRequest.Request, cancellationToken);

        return RedirectToAction("TaskList", "MonthlyProgressReporting", new { Area = "MonthlyProgressReporting" });
    }
    #endregion
}