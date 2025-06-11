using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.BaseInformation.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageKeyDates.CheckYourAnswers.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageKeyDates.CheckYourAnswers.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageKeyDates.KeyDates.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageKeyDates.KeyDates.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageKeyDates.Reset;
using HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageKeyDates;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.WorksPackage.WorksPackageKeyDates.Controllers;

[Area("WorksPackageKeyDates")]
[Route("WorksPackage/KeyDates")]
public class KeyDatesController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public KeyDatesController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart =>
        RedirectToAction("KeyDates", "KeyDates", new { Area = "WorksPackageKeyDates" });

    #region Key Dates
    
    [HttpGet(nameof(KeyDates))]
    public async Task<IActionResult> KeyDates()
    {
        var response = await _sender.Send(GetKeyDatesRequest.Request);
        var viewModel = _mapper.Map<KeyDatesViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(KeyDates))]
    public async Task<IActionResult> KeyDates(KeyDatesViewModel viewModel,
        ESubmitAction submitAction)
    {
        var validator = new KeyDatesViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var useCaseRequest = _mapper.Map<SetKeyDatesRequest>(viewModel);
        await _sender.Send(useCaseRequest);

        return submitAction == ESubmitAction.Continue
            ? RedirectToAction("CheckYourAnswers", "KeyDates", new { Area = "WorksPackageKeyDates" })
            : RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
    }

    #endregion

    #region Check Your Answers

    [HttpGet(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers()
    {
        var response = await _sender.Send(GetCheckYourAnswersRequest.Request);
        var viewModel = _mapper.Map<CheckYourAnswersViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers(CheckYourAnswersViewModel viewModel,
        ESubmitAction submitAction)
    {
        if (submitAction == ESubmitAction.Continue)
        {
            var useCaseRequest = SetCheckYourAnswersRequest.Request;
            await _sender.Send(useCaseRequest);

            return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
        }

        return View(viewModel);
    }

    #endregion

    #region ChangeAnswers

    [HttpGet(nameof(ChangeAnswers))]
    public async Task<IActionResult> ChangeAnswers()
    {
        var response = await _sender.Send(GetBaseInformationRequest.Request);
        var model = _mapper.Map<ChangeAnswersViewModel>(response);

        return View(model);
    }

    [HttpPost(nameof(ChangeAnswers))]
    public async Task<IActionResult> ChangeAnswers(ChangeAnswersViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (model.Proceed == false)
        {
            return RedirectToAction("CheckYourAnswers");
        }

        await _sender.Send(new ResetRequest(), cancellationToken);

        return RedirectToAction("KeyDates");
    }

    #endregion
}