using AutoMapper;

using FluentValidation.AspNetCore;

using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Declaration.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Declaration.Set;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Submit.Set;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Submitted.Get;
using HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

using Mediator;

using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.ScheduleOfWorks.ScheduleOfWorksDeclaration.Controllers;

[Area("ScheduleOfWorksDeclaration")]
[Route("ScheduleOfWorks/Declaration")]
public class DeclarationController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public DeclarationController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }
    protected override IActionResult DefaultStart =>
     RedirectToAction("Declaration", "Declaration", new { Area = "ScheduleOfWorksDeclaration" });

    #region "Schedule of works declaration"

    [HttpGet(nameof(Declaration))]
    public async Task<IActionResult> Declaration()
    {
        var response = await _sender.Send(GetDeclarationRequest.Request);
        var viewModel = _mapper.Map<DeclarationViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(Declaration))]
    public async Task<IActionResult> Declaration(DeclarationViewModel viewModel)
    {
        var validator = new DeclarationViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetDeclarationRequest>(viewModel);
        await _sender.Send(request);

        await _sender.Send(SetSubmitRequest.Request);
        return RedirectToAction("Submitted", "Declaration", new { Area = "ScheduleOfWorksDeclaration" });
    }

    #endregion

    #region "Submitted"

    [HttpGet(nameof(Submitted))]
    public async Task<IActionResult> Submitted()
    {
        var response = await _sender.Send(GetSubmittedRequest.Request);
        var viewModel = _mapper.Map<SubmittedViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    #endregion
}