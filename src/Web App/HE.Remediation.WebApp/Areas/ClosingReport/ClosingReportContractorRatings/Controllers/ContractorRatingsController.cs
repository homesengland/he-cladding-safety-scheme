using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubContractorCheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubContractorRating;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubContractors;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.ProceedFromAbout;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.SetSubContractorRatings;
using HE.Remediation.WebApp.Areas.ClosingReport.Controllers;
using HE.Remediation.WebApp.ViewModels.ClosingReport;
using HE.Remediation.WebApp.ViewModels.ClosingReport.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.ClosingReport.ClosingReportContractorRatings.Controllers;

[Area("ClosingReportContractorRatings")]
[Route("ClosingReport/ContractorRatings")]
public class ContractorRatingsController(ISender sender, IMapper mapper) : BaseAboutController(sender)
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;

    protected override EClosingReportTask ClosingReportTask => EClosingReportTask.RatingsForContractors;
    protected override IActionResult NextScreenAfterAbout =>
                                        RedirectToAction("SubContractors", "ContractorRatings",
                                            new { Area = "ClosingReportContractorRatings" });

    #region Sub-Contractors

    [HttpGet(nameof(SubContractors))]
    public async Task<IActionResult> SubContractors()
    {
        var response = await _sender.Send(GetSubContractorsRequest.Request);
        var model = _mapper.Map<SubContractorsViewModel>(response);
        return View(model);
    }

    [HttpGet("SubContractorRatings/{id:guid}/{name}")]
    public async Task<IActionResult> SubContractorRatings(Guid id, string name, string returnUrl)
    {
        var response = await _sender.Send(new GetSubContractorRatingsRequest(id));
        var model = _mapper.Map<SubContractorRatingsViewModel>(response);
        model.Name = name;
        model.ReturnUrl = returnUrl;

        return View(model);
    }

    [HttpPost("SubContractorRatings/{id:guid}/{name}")]
    public async Task<IActionResult> SubContractorRatings(SubContractorRatingsViewModel model)
    {
        var validator = new SubContractorRatingsViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetSubContractorRatingsRequest>(model);
        request.Complete = model.SubmitAction == ESubmitAction.Continue;

        await _sender.Send(request);

        return RedirectToAction("SubContractors");
    }

    [HttpGet(nameof(SubContractorsCheckYourAnswers))]
    public async Task<IActionResult> SubContractorsCheckYourAnswers()
    {
        var response = await _sender.Send(GetSubContractorCheckYourAnswersRequest.Request);
        var model = _mapper.Map<CheckYourAnswersViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(SubContractorsCheckYourAnswers))]
    public async Task<IActionResult> SubContractorsCheckYourAnswers(ESubmitAction submitAction)
    {
        if(submitAction == ESubmitAction.Continue)
        {
            await _sender.Send(new UpdateTaskStatusRequest(ClosingReportTask, ETaskStatus.Completed));
        }

        return RedirectToAction("TaskList", "ClosingReport", new { Area = "ClosingReport" });
    }

    #endregion
}
