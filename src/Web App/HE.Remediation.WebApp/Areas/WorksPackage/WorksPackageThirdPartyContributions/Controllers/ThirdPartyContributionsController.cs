using MediatR;
using Microsoft.AspNetCore.Mvc;
using HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;
using HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageThirdPartyContributions;
using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.StartInformation.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.BaseInformation.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.PursuingThirdPartyContribution.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.PursuingThirdPartyContribution.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.ThirdPartyContribution.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.ThirdPartyContribution.Set;
using CheckYourAnswersViewModel = HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageThirdPartyContributions.CheckYourAnswersViewModel;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.ChangeYourAnswers;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.CheckYourAnswers.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.CheckYourAnswers.Set;

namespace HE.Remediation.WebApp.Areas.WorksPackage.WorksPackageThirdPartyContributions.Controllers;

[Area("WorksPackageThirdPartyContributions")]
[Route("WorksPackage/ThirdPartyContributions")]
public class ThirdPartyContributionsController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ThirdPartyContributionsController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart =>
        RedirectToAction("StartInformation", "ThirdPartyContributions", new { Area = "WorksPackageThirdPartyContributions" });

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
    public IActionResult StartInformation(StartInformationViewModel viewModel, ESubmitAction submitAction)
    {
        return RedirectToAction("PursuingThirdPartyContribution", "ThirdPartyContributions", new { Area = "WorksPackageThirdPartyContributions" });
    }

    #endregion

    #region Are you pursuing any third party contribution?

    [HttpGet(nameof(PursuingThirdPartyContribution))]
    public async Task<IActionResult> PursuingThirdPartyContribution(string returnUrl)
    {
        var response = await _sender.Send(GetPursuingThirdPartyContributionRequest.Request);
        var viewModel = _mapper.Map<PursuingThirdPartyContributionViewModel>(response);

        viewModel.ReturnUrl = returnUrl;

        return View(viewModel);
    }

    [HttpPost(nameof(PursuingThirdPartyContribution))]
    public async Task<IActionResult> PursuingThirdPartyContribution(PursuingThirdPartyContributionViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new PursuingThirdPartyContributionViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetPursuingThirdPartyContributionRequest>(viewModel);
        await _sender.Send(request);

        return submitAction == ESubmitAction.Continue
            ? viewModel?.PursuingThirdPartyContribution == true
                ? RedirectToAction("ThirdPartyContribution", "ThirdPartyContributions", new { Area = "WorksPackageThirdPartyContributions" })
                : RedirectToAction("CheckYourAnswers", "ThirdPartyContributions", new { Area = "WorksPackageThirdPartyContributions" })
            : RedirectToAction("TaskList", "WorkPackage", new { area = "WorksPackage" });
    }

    #endregion

    #region Tell us about the third party contribution

    [HttpGet(nameof(ThirdPartyContribution))]
    public async Task<IActionResult> ThirdPartyContribution(string returnUrl)
    {
        var response = await _sender.Send(GetThirdPartyContributionRequest.Request);
        var viewModel = _mapper.Map<ThirdPartyContributionViewModel>(response);

        viewModel.ReturnUrl = returnUrl;

        return View(viewModel);
    }

    [HttpPost(nameof(ThirdPartyContribution))]
    public async Task<IActionResult> ThirdPartyContribution(ThirdPartyContributionViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new ThirdPartyContributionViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetThirdPartyContributionRequest>(viewModel);
        await _sender.Send(request);

        return submitAction == ESubmitAction.Continue
            ? RedirectToAction("CheckYourAnswers", "ThirdPartyContributions", new { Area = "WorksPackageThirdPartyContributions" })
            : RedirectToAction("TaskList", "WorkPackage", new { area = "WorksPackage" });
    }

    #endregion

    #region Change your answers

    [HttpGet(nameof(ChangeYourAnswers))]
    public async Task<IActionResult> ChangeYourAnswers()
    {
        var response = await _sender.Send(GetBaseInformationRequest.Request);
        var model = _mapper.Map<ChangeYourAnswersViewModel>(response);

        return View(model);
    }

    [HttpPost(nameof(ChangeYourAnswers))]
    public async Task<IActionResult> ChangeYourAnswers(ChangeYourAnswersViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (model.Proceed == false)
        {
            return RedirectToAction("CheckYourAnswers");
        }

        await _sender.Send(new GetChangeYourAnswersRequest(), cancellationToken);

        return RedirectToAction("PursuingThirdPartyContribution");
    }

    #endregion

    #region Check your answers

    [HttpGet(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers()
    {
        var response = await _sender.Send(GetCheckYourAnswersRequest.Request);

        var viewModel = _mapper.Map<CheckYourAnswersViewModel>(response);

        return View(viewModel);
    }

    [HttpPost(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers(CheckYourAnswersViewModel viewModel, ESubmitAction submitAction)
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

}