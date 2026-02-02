using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.PracticalCompletionMilestone;
using HE.Remediation.WebApp.ViewModels.PracticalCompletionMilestone;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.PracticalCompletionMilestone.Controllers;

[Area("PracticalCompletionMilestone")]
[Route("Milestone/PracticalCompletion")]
public class PracticalCompletionMilestoneController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public PracticalCompletionMilestoneController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart => RedirectToAction("PracticalCompletion", "PracticalCompletionMilestone", new { Area = "PracticalCompletionMilestone" });

    [HttpGet(nameof(PracticalCompletion))]
    public async Task<IActionResult> PracticalCompletion(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetPracticalCompletionRequest.Request, cancellationToken);
        var model = _mapper.Map<PracticalCompletionViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(PracticalCompletion))]
    public async Task<IActionResult> PracticalCompletion(PracticalCompletionViewModel model, CancellationToken cancellationToken)
    {
        var validator = new PracticalCompletionViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<UpdatePracticalCompletionRequest>(model);
        await _sender.Send(request, cancellationToken);

        return model.SubmitAction == ESubmitAction.Continue
            ? RedirectToAction("CheckYourAnswers", "PracticalCompletionMilestone", new { Area = "PracticalCompletionMilestone" })
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
