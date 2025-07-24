using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeam.CheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeam.ProjectTeam.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeam.RegulatoryCompliance;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeam.StartInformation.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeam.StartInformation.Set;
using HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeam;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.WorksPackage.WorksPackageProjectTeam.Controllers;

[Area("WorksPackageProjectTeam")]
[Route("WorksPackage/ProjectTeam")]
public class ProjectTeamController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ProjectTeamController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart =>
        RedirectToAction("StartInformation", "ProjectTeam", new { Area = "WorksPackageProjectTeam" });

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
    public async Task<IActionResult> StartInformation(StartInformationViewModel viewModel, ESubmitAction submitAction)
    {
        await _sender.Send(SetStartInformationRequest.Request);
        return RedirectToAction("ProjectTeam");
    }

    #endregion

    #region Tell us about your project team

    [HttpGet(nameof(ProjectTeam))]
    public async Task<IActionResult> ProjectTeam()
    {
        var response = await _sender.Send(GetProjectTeamRequest.Request);
        var viewModel = _mapper.Map<ProjectTeamViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(ProjectTeam))]
    public async Task<IActionResult> ProjectTeam(ProjectTeamViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new ProjectTeamViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        return submitAction switch
        {
            ESubmitAction.Continue => RedirectToAction("RegulatoryCompliance", "ProjectTeam", new { Area = "WorksPackage" }),
            ESubmitAction.Exit => RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" }),
            _ => View(viewModel)
        };
    }

    #endregion

    #region Regulatory Compliance

    [HttpGet(nameof(RegulatoryCompliance))]
    public async Task<IActionResult> RegulatoryCompliance(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetRegulatoryComplianceRequest.Request, cancellationToken);
        var model = _mapper.Map<RegulatoryComplianceViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(RegulatoryCompliance))]
    public async Task<IActionResult> RegulatoryCompliance(RegulatoryComplianceViewModel model, CancellationToken cancellationToken)
    {
        var validator = new RegulatoryComplianceViewModelValidator();

        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetRegulatoryComplianceRequest>(model);
        await _sender.Send(request, cancellationToken);

        return model.SubmitAction == ESubmitAction.Continue
            ? RedirectToAction("CheckYourAnswers", "ProjectTeam", new { Area = "WorksPackageProjectTeam" })
            : RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
    }

    #endregion

    #region Check Your Answers

    [HttpGet(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetCheckYourAnswersRequest.Request, cancellationToken);
        var model = _mapper.Map<ProjectTeamCheckYourAnswersViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(SetCheckYourAnswers))]
    public async Task<IActionResult> SetCheckYourAnswers(CancellationToken cancellationToken)
    {
        await _sender.Send(SetCheckYourAnswersRequest.Request, cancellationToken);
        return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
    }
    #endregion
}