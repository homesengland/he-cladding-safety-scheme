using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetDeclaration;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubmitted;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.ProceedFromAbout;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.SetDeclaration;
using HE.Remediation.WebApp.ViewModels.ClosingReport;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.ClosingReport.ClosingReportSubmission.Controllers;

[Area("ClosingReportSubmission")]
[Route("ClosingReport/Submission")]
public class SubmissionController(ISender sender, IMapper mapper) : StartController(sender)
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;

    protected override IActionResult DefaultStart => RedirectToAction("Declaration");

    #region Declaration

    [HttpGet(nameof(Declaration))]
    public async Task<IActionResult> Declaration()
    {
        await _sender.Send(new UpdateTaskStatusRequest(EClosingReportTask.FinalPaymentDeclaration, ETaskStatus.InProgress));

        var response = await _sender.Send(GetDeclarationRequest.Request);
        var model = _mapper.Map<DeclarationViewModel>(response);
        model.ReturnUrl = Url.Action("TaskList", "ClosingReport", new { Area = "ClosingReport" });

        return View(model);
    }

    [HttpPost(nameof(Declaration))]
    public async Task<IActionResult> Declaration(DeclarationViewModel viewModel, ESubmitAction submitAction)
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

        return submitAction == ESubmitAction.Continue ?
                                        RedirectToAction("Submitted") :
                                        RedirectToAction("TaskList", "ClosingReport", new { Area = "ClosingReport" });
    }

    #endregion

    #region Submitted

    [HttpGet(nameof(Submitted))]
    public async Task<IActionResult> Submitted()
    {
        var response = await _sender.Send(GetSubmittedRequest.Request);
        var viewModel = _mapper.Map<SubmittedViewModel>(response);
        return View(viewModel);
    }

    #endregion
}
