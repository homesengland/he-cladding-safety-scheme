using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.StartedOnSiteMilestone;
using HE.Remediation.WebApp.ViewModels.StartedOnSiteMilestone;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.StartedOnSiteMilestone.Controllers;

[Area("StartedOnSiteMilestone")]
[Route("Milestone/StartedOnSite")]
public class StartedOnSiteMilestoneController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public StartedOnSiteMilestoneController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart => RedirectToAction("StartedOnSite", "StartedOnSiteMilestone", new { Area = "StartedOnSiteMilestone" });

    [HttpGet(nameof(StartedOnSite))]
    public async Task<IActionResult> StartedOnSite(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetStartedOnSiteRequest.Request, cancellationToken);
        var model = _mapper.Map<StartedOnSiteViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(StartedOnSite))]
    public async Task<IActionResult> StartedOnSite(StartedOnSiteViewModel model, CancellationToken cancellationToken)
    {
        var validator = new StartedOnSiteViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<UpdateStartedOnSiteRequest>(model);
        await _sender.Send(request, cancellationToken);

        return model.SubmitAction == ESubmitAction.Continue
            ? RedirectToAction("CheckYourAnswers", "StartedOnSiteMilestone", new { Area = "StartedOnSiteMilestone" })
            : RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
    }

    [HttpGet(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetCheckYourAnswersRequest.Request, cancellationToken);
        var model = _mapper.Map<CheckYourAnswersViewModel>(response);
        ViewData["BackLinkHidden"] = model.IsSubmitted;
        return View(model);
    }

    [HttpPost(nameof(SubmitCheckYourAnswers))]
    public async Task<IActionResult> SubmitCheckYourAnswers(CancellationToken cancellationToken)
    {
        var request = SubmitCheckYourAnswersRequest.Request;
        await _sender.Send(request, cancellationToken);

        return RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
    }
}
