using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.BuildingsInsurance.GetBuildingsInsurance;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetReviewPayment;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubmitPayment;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.SetReviewPayment;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.SetSubmitPayment;
using HE.Remediation.WebApp.Areas.ClosingReport.Controllers;
using HE.Remediation.WebApp.ViewModels.ClosingReport;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.ClosingReport.ClosingReportFinalPaymentRequest.Controllers;

[Area("ClosingReportFinalPaymentRequest")]
[Route("ClosingReport/FinalPaymentRequest")]
public class FinalPaymentRequestController(ISender sender, IMapper mapper) : BaseAboutController(sender)
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;

    protected override EClosingReportTask ClosingReportTask => EClosingReportTask.SubmitPaymentRequest;
    protected override IActionResult NextScreenAfterAbout =>
                                        RedirectToAction("SubmitPayment", "FinalPaymentRequest",
                                            new { Area = "ClosingReportFinalPaymentRequest" });

    #region Submit Payment

    [HttpGet(nameof(SubmitPayment))]
    public async Task<IActionResult> SubmitPayment(string returnUrl)
    {
        var response = await _sender.Send(GetSubmitPaymentRequest.Request);
        var viewModel = _mapper.Map<CostsViewModel>(response);
        viewModel.ReturnUrl = Url.Action("About");

        return View(viewModel);
    }

    [HttpPost(nameof(SubmitPayment))]
    public async Task<IActionResult> SubmitPayment(CostsViewModel viewModel, ESubmitAction submitAction)
    {
        var request = _mapper.Map<SetSubmitPaymentRequest>(viewModel);

        var response = await _sender.Send(request);

        if (!response.IsValidRequest)
        {
            ModelState.AddModelError(nameof(CostsViewModel.UnprofiledGrantFunding), response.ValidationMessage);
            return View(viewModel);
        }

        return submitAction == ESubmitAction.Continue
            ? RedirectToAction("ReviewPaymentRequest")
            : RedirectToAction("TaskList", "ClosingReport", new { Area = "ClosingReport" });
    }

    #endregion

    #region Review Payment Request

    [HttpGet(nameof(ReviewPaymentRequest))]
    public async Task<IActionResult> ReviewPaymentRequest(string returnUrl)
    {
        var response = await _sender.Send(GetReviewPaymentRequest.Request);
        var viewModel = _mapper.Map<ReviewPaymentRequestViewModel>(response);

        var insuranceResponse = await _sender.Send(GetBuildingsInsuranceRequest.Request);
        _mapper.Map(insuranceResponse, viewModel);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(ReviewPaymentRequest))]
    public async Task<IActionResult> ReviewPaymentRequest(ReviewPaymentRequestViewModel viewModel,
        ESubmitAction submitAction)
    {
        var validator = new ReviewPaymentRequestViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetReviewPaymentRequest>(viewModel);
        request.Confirm = (submitAction == ESubmitAction.Continue);
        await _sender.Send(request);

        return RedirectToAction("TaskList", "ClosingReport", new { Area = "ClosingReport" });
    }

    #endregion
}
