using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.AboutCosts.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.BaseInformation.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.CheckYourAnswers.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Confirmation.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Confirmation.Set;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.ConfirmRemoveVariationReason.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.CostProfile.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Costs.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Costs.Set;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Declaration.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Declaration.Set;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Descriptions.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Evidence.Add;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Evidence.Delete;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Evidence.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Evidence.Set;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.IneligibleCosts.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.IneligibleCosts.Set;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.IneligibleCostsChanges.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.IneligibleCostsChanges.Set;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.InstallationOfCladdingCosts.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.InstallationOfCladdingCosts.Set;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.OtherCosts.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.OtherCosts.Set;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.PreliminaryCosts.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.PreliminaryCosts.Set;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Scope.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Scope.Set;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Submit.Set;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Submitted.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Timescale.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Timescale.Set;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.UnsafeCladdingCosts.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.UnsafeCladdingCosts.Set;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.VariationReason.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.VariationReason.Set;
using HE.Remediation.WebApp.Attributes.Routing;
using HE.Remediation.WebApp.Constants;
using HE.Remediation.WebApp.ViewModels.VariationRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.ThirdPartyContribution.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.ThirdPartyContribution.Set;
using Azure;

namespace HE.Remediation.WebApp.Areas.Variation.Controllers;

[Area("VariationRequest")]
[Route("VariationRequest")]
public class VariationRequestController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public VariationRequestController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart => RedirectToAction("StartInformation", "VariationRequest", new { Area = "VariationRequest" });

    #region "About this section"

    [HttpGet(nameof(StartInformation))]
    public async Task<IActionResult> StartInformation()
    {
        var response = await _sender.Send(GetBaseInformationRequest.Request);
        var viewModel = _mapper.Map<ViewModels.VariationRequest.StartInformationViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(StartInformation))]
    public IActionResult StartInformation(ViewModels.VariationRequest.StartInformationViewModel viewModel)
    {
        return RedirectToAction("CostProfile", "VariationRequest", new { Area = "VariationRequest" });
    }

    #endregion

    #region "Reason for variation"

    [HttpGet(nameof(VariationReason))]
    public async Task<IActionResult> VariationReason([FromQuery] string returnUrl)
    {
        var response = await _sender.Send(GetVariationReasonRequest.Request);
        var viewModel = _mapper.Map<VariationReasonViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(VariationReason))]
    public async Task<IActionResult> VariationReason(VariationReasonViewModel viewModel)
    {
        var validator = new VariationReasonViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var (adding, removing) = await IsVariationReasonBeingAddedOrRemoved(viewModel);
        if (removing)
        {
            var variationReasonJson = JsonSerializer.Serialize(viewModel);
            TempData["VariationReasons"] = variationReasonJson;
            return viewModel.ReturnUrl is not null
                ? RedirectToAction("ConfirmRemoveVariationReason", "VariationRequest", new { Area = "VariationRequest", returnUrl = viewModel.ReturnUrl })
                : RedirectToAction("ConfirmRemoveVariationReason", "VariationRequest", new { Area = "VariationRequest" });
        }

        var request = _mapper.Map<SetVariationReasonRequest>(viewModel);
        await _sender.Send(request);

        if (viewModel.SubmitAction == ESubmitAction.Continue)
        {
            if (viewModel.ReturnUrl is not null && !adding)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "VariationRequest", new { Area = "VariationRequest" });
            }

            if (viewModel.IsTimescaleVariation == true)
            {
                return RedirectToAction("AdjustEndDate", "VariationRequest", new { Area = "VariationRequest" });
            }
            else if (viewModel.IsScopeVariation == true)
            {
                return RedirectToAction("AdjustScope", "VariationRequest", new { Area = "VariationRequest" });
            }
            else if (viewModel.IsCostVariation == true)
            {
                return RedirectToAction("AboutAdjustCosts", "VariationRequest", new { Area = "VariationRequest" });
            }
            else if (viewModel.IsThirdPartyContributionVariation.GetValueOrDefault(false))
            {
                return RedirectToAction("ThirdPartyContribution", "VariationRequest", new { Area = "VariationRequest" });
            }
        }

        return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
    }

    private async Task<(bool, bool)> IsVariationReasonBeingAddedOrRemoved(VariationReasonViewModel viewModel)
    {
        var previousVariationReasons = await _sender.Send(GetVariationReasonRequest.Request);

        var adding = previousVariationReasons is not null &&
            ((previousVariationReasons.IsCostVariation is (null or false) && viewModel.IsCostVariation is true) ||
            (previousVariationReasons.IsScopeVariation is (null or false) && viewModel.IsScopeVariation is true) ||
            (previousVariationReasons.IsTimescaleVariation is (null or false) && viewModel.IsTimescaleVariation is true) ||
            (previousVariationReasons.IsThirdPartyContributionVariation is (null or false) && viewModel.IsThirdPartyContributionVariation is true));

        var removing = previousVariationReasons is not null &&
            ((previousVariationReasons.IsCostVariation is true && viewModel.IsCostVariation is (null or false)) ||
            (previousVariationReasons.IsScopeVariation is true && viewModel.IsScopeVariation is (null or false)) ||
            (previousVariationReasons.IsTimescaleVariation is true && viewModel.IsTimescaleVariation is (null or false)) ||
            (previousVariationReasons.IsThirdPartyContributionVariation is true && viewModel.IsThirdPartyContributionVariation is (null or false)));

        return (adding, removing);
    }

    #endregion

    #region "Confirm variation removal"

    [ExcludeRouteRecording]
    [HttpGet(nameof(ConfirmRemoveVariationReason))]
    public async Task<IActionResult> ConfirmRemoveVariationReason(string returnUrl)
    {
        var variationReasonsJson = TempData["VariationReasons"]?.ToString();
        var variationReasons = variationReasonsJson != null
            ? JsonSerializer.Deserialize<VariationReasonViewModel>(variationReasonsJson)
            : null;

        var response = await _sender.Send(GetConfirmRemoveVariationReasonRequest.Request);
        var viewModel = _mapper.Map<ConfirmRemoveVariationReasonViewModel>(response);
        if (variationReasons is not null)
        {
            _mapper.Map(variationReasons, viewModel);
        }

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(ConfirmRemoveVariationReason))]
    public async Task<IActionResult> ConfirmRemoveVariationReason(ConfirmRemoveVariationReasonViewModel viewModel)
    {
        var validator = new ConfirmRemoveVariationReasonViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        if (viewModel.Proceed == false)
        {
            return viewModel.ReturnUrl is not null
                ? RedirectToAction("VariationReason", "VariationRequest", new { Area = "VariationRequest", returnUrl = viewModel.ReturnUrl })
                : RedirectToAction("VariationReason", "VariationRequest", new { Area = "VariationRequest" });
        }

        var request = _mapper.Map<SetVariationReasonRequest>(viewModel);
        await _sender.Send(request);

        if (viewModel.IsTimescaleVariation == true)
        {
            return RedirectToAction("AdjustEndDate", "VariationRequest", new { Area = "VariationRequest" });
        }
        else if (viewModel.IsScopeVariation == true)
        {
            return RedirectToAction("AdjustScope", "VariationRequest", new { Area = "VariationRequest" });
        }
        else if (viewModel.IsCostVariation == true)
        {
            return RedirectToAction("AboutAdjustCosts", "VariationRequest", new { Area = "VariationRequest" });
        }
        else if (viewModel.IsThirdPartyContributionVariation == true)
        {
            return RedirectToAction("ThirdPartyContribution", "VariationRequest", new { Area = "VariationRequest" });
        }

        return RedirectToAction("VariationReason", "VariationRequest", new { Area = "VariationRequest" });
    }

    #endregion

    #region "What is your new end date?"

    [HttpGet(nameof(AdjustEndDate))]
    public async Task<IActionResult> AdjustEndDate([FromQuery] string returnUrl)
    {
        var response = await _sender.Send(GetAdjustEndDateRequest.Request);
        var viewModel = _mapper.Map<AdjustEndDateViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(AdjustEndDate))]
    public async Task<IActionResult> AdjustEndDate(AdjustEndDateViewModel viewModel)
    {
        var validator = new AdjustEndDateViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetAdjustEndDateRequest>(viewModel);
            await _sender.Send(request);

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "VariationRequest", new { Area = "VariationRequest" });
            }

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                var response = await _sender.Send(GetVariationReasonRequest.Request);

                if (response.IsScopeVariation.GetValueOrDefault(false))
                {
                    return RedirectToAction("AdjustScope", "VariationRequest", new { Area = "VariationRequest" });
                }
                else if (response.IsCostVariation.GetValueOrDefault(false))
                {
                    return RedirectToAction("AboutAdjustCosts", "VariationRequest", new { Area = "VariationRequest" });
                }
                else if (response.IsThirdPartyContributionVariation.GetValueOrDefault(false))
                {
                    return RedirectToAction("ThirdPartyContribution", "VariationRequest", new { Area = "VariationRequest" });
                }
                else
                {
                    return RedirectToAction("Confirmation", "VariationRequest", new { Area = "VariationRequest" });
                }
            }

            return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region "How has the scope of your project changed?"

    [HttpGet(nameof(AdjustScope))]
    public async Task<IActionResult> AdjustScope([FromQuery] string returnUrl)
    {
        var response = await _sender.Send(GetAdjustScopeRequest.Request);
        var viewModel = _mapper.Map<AdjustScopeViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(AdjustScope))]
    public async Task<IActionResult> AdjustScope(AdjustScopeViewModel viewModel)
    {
        var validator = new AdjustScopeViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetAdjustScopeRequest>(viewModel);
            await _sender.Send(request);

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "VariationRequest", new { Area = "VariationRequest" });
            }

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                var response = await _sender.Send(GetVariationReasonRequest.Request);

                if (response.IsCostVariation.GetValueOrDefault(false))
                {
                    return RedirectToAction("AboutAdjustCosts", "VariationRequest", new { Area = "VariationRequest" });
                }
                else if (response.IsThirdPartyContributionVariation.GetValueOrDefault(false))
                {
                    return RedirectToAction("ThirdPartyContribution", "VariationRequest", new { Area = "VariationRequest" });
                }
                else
                {
                    return RedirectToAction("Confirmation", "VariationRequest", new { Area = "VariationRequest" });
                }
            }

            return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region "About this section" - About (End Date, Scope, Budget)

    [HttpGet(nameof(AboutAdjustCosts))]
    public async Task<IActionResult> AboutAdjustCosts()
    {
        var response = await _sender.Send(GetAboutCostsRequest.Request);
        var viewModel = _mapper.Map<AboutAdjustCostsViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(AboutAdjustCosts))]
    public IActionResult AboutAdjustCosts(AboutAdjustCostsViewModel viewModel)
    {
        return viewModel.SubmitAction == ESubmitAction.Continue
            ? RedirectToAction("Costs", "VariationRequest", new { Area = "VariationRequest" })
            : RedirectToAction("Index", "StageDiagram", new { area = "Application" });
    }

    [HttpGet(nameof(ThirdPartyContribution))]
    public async Task<IActionResult> ThirdPartyContribution()
    {
        var response = await _sender.Send(GetThirdPartyContributionRequest.Request);
        var viewModel = _mapper.Map<ThirdPartyContributionViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(ThirdPartyContribution))]
    public async Task<IActionResult> ThirdPartyContribution(ThirdPartyContributionViewModel viewModel, ESubmitAction submitAction)
    {
        ModelState.ClearValidationState(nameof(viewModel.ContributionAmount));

        var validator = new ThirdPartyContributionViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetThirdPartyContributionRequest>(viewModel);
        await _sender.Send(request);

        if (viewModel.SubmitAction == ESubmitAction.Continue)
        {
            return viewModel.ReturnUrl is not null
                ? SafeRedirectToAction(viewModel.ReturnUrl, "VariationRequest", new { Area = "VariationRequest" })
                : RedirectToAction("Confirmation", "VariationRequest", new { Area = "VariationRequest" });
        }

        return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
    }


    #endregion

    #region "Current cost profile"

    [HttpGet(nameof(CostProfile))]
    public async Task<IActionResult> CostProfile()
    {
        var response = await _sender.Send(GetCostProfileRequest.Request);
        var viewModel = _mapper.Map<CostProfileViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(CostProfile))]
    public IActionResult CostProfile(CostProfileViewModel viewModel)
    {
         return RedirectToAction("VariationReason", "VariationRequest", new { Area = "VariationRequest" });
    }

    #endregion

    #region "Tell us about your costs"

    [HttpGet(nameof(Costs))]
    public async Task<IActionResult> Costs()
    {
        var response = await _sender.Send(GetCostsRequest.Request);

        if (!response.HasCostVariation)
        {
            if (response.IsThirdPartyContributionVariation.GetValueOrDefault(false))
            {
                return RedirectToAction("ThirdPartyContribution", "VariationRequest", new { Area = "VariationRequest" });
            }
            
            return RedirectToAction("VariationReason", "VariationRequest", new { Area = "VariationRequest" });
        }

        var viewModel = _mapper.Map<CostsViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(Costs))]
    public async Task<IActionResult> Costs(CostsViewModel viewModel)
    {
        if (viewModel.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
        }

        var response = await _sender.Send(SetCostsRequest.Request);

        if (response.VariationCostsValidation)
        {
            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                return RedirectToAction("Descriptions", "VariationRequest", new { Area = "VariationRequest" });
            }
        }

        viewModel.VariationCostsValidation = response.VariationCostsValidation;

        var validator = new CostsViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region "Removal of unsafe cladding"

    [HttpGet(nameof(UnsafeCladdingCosts))]
    public async Task<IActionResult> UnsafeCladdingCosts([FromQuery] string returnUrl)
    {
        var response = await _sender.Send(GetUnsafeCladdingCostsRequest.Request);

        if (!response.HasCostVariation)
        {
            if (response.IsThirdPartyContributionVariation.GetValueOrDefault(false))
            {
                return RedirectToAction("ThirdPartyContribution", "VariationRequest", new { Area = "VariationRequest" });
            }

            return RedirectToAction("VariationReason", "VariationRequest", new { Area = "VariationRequest" });
        }

        var viewModel = _mapper.Map<UnsafeCladdingCostsViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(UnsafeCladdingCosts))]
    public async Task<IActionResult> UnsafeCladdingCosts(UnsafeCladdingCostsViewModel viewModel)
    {
        var validator = new UnsafeCladdingCostsViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetUnsafeCladdingCostsRequest>(viewModel);
            await _sender.Send(request);

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "VariationRequest", new { Area = "VariationRequest" });
            }

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                return RedirectToAction("Costs", "VariationRequest", new { Area = "VariationRequest" });
            }

            return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region "Installation of new cladding"

    [HttpGet(nameof(InstallationOfCladdingCosts))]
    public async Task<IActionResult> InstallationOfCladdingCosts([FromQuery] string returnUrl)
    {
        var response = await _sender.Send(GetInstallationOfCladdingCostsRequest.Request);

        if (!response.HasCostVariation)
        {
            if (response.IsThirdPartyContributionVariation.GetValueOrDefault(false))
            {
                return RedirectToAction("ThirdPartyContribution", "VariationRequest", new { Area = "VariationRequest" });
            }

            return RedirectToAction("VariationReason", "VariationRequest", new { Area = "VariationRequest" });
        }

        var viewModel = _mapper.Map<InstallationOfCladdingCostsViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(InstallationOfCladdingCosts))]
    public async Task<IActionResult> InstallationOfCladdingCosts(InstallationOfCladdingCostsViewModel viewModel)
    {
        var validator = new InstallationOfCladdingCostsViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetInstallationOfCladdingCostsRequest>(viewModel);
            await _sender.Send(request);

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "VariationRequest", new { Area = "VariationRequest" });
            }

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                return RedirectToAction("Costs", "VariationRequest", new { Area = "VariationRequest" });
            }

            return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region "Preliminaries, access costs,  main contractor’s overheads and profit"

    [HttpGet(nameof(PreliminaryCosts))]
    public async Task<IActionResult> PreliminaryCosts([FromQuery] string returnUrl)
    {
        var response = await _sender.Send(GetPreliminaryCostsRequest.Request);

        if (!response.HasCostVariation)
        {
            if (response.IsThirdPartyContributionVariation.GetValueOrDefault(false))
            {
                return RedirectToAction("ThirdPartyContribution", "VariationRequest", new { Area = "VariationRequest" });
            }

            return RedirectToAction("VariationReason", "VariationRequest", new { Area = "VariationRequest" });
        }

        var viewModel = _mapper.Map<PreliminaryCostsViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(PreliminaryCosts))]
    public async Task<IActionResult> PreliminaryCosts(PreliminaryCostsViewModel viewModel)
    {
        var validator = new PreliminaryCostsViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetPreliminaryCostsRequest>(viewModel);
            await _sender.Send(request);

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "VariationRequest", new { Area = "VariationRequest" });
            }

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                return RedirectToAction("Costs", "VariationRequest", new { Area = "VariationRequest" });
            }

            return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region "Other costs"

    [HttpGet(nameof(OtherCosts))]
    public async Task<IActionResult> OtherCosts([FromQuery] string returnUrl)
    {
        var response = await _sender.Send(GetOtherCostsRequest.Request);

        if (!response.HasCostVariation)
        {
            if (response.IsThirdPartyContributionVariation.GetValueOrDefault(false))
            {
                return RedirectToAction("ThirdPartyContribution", "VariationRequest", new { Area = "VariationRequest" });
            }

            return RedirectToAction("VariationReason", "VariationRequest", new { Area = "VariationRequest" });
        }

        var viewModel = _mapper.Map<OtherCostsViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(OtherCosts))]
    public async Task<IActionResult> OtherCosts(OtherCostsViewModel viewModel)
    {
        var validator = new OtherCostsViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetOtherCostsRequest>(viewModel);
            await _sender.Send(request);

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "VariationRequest", new { Area = "VariationRequest" });
            }

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                return RedirectToAction("Costs", "VariationRequest", new { Area = "VariationRequest" });
            }

            return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region "Tell us about your works"

    [HttpGet(nameof(Descriptions))]
    public async Task<IActionResult> Descriptions()
    {
        var response = await _sender.Send(GetDescriptionsRequest.Request);

        if (!response.HasCostVariation)
        {
            if (response.IsThirdPartyContributionVariation.GetValueOrDefault(false))
            {
                return RedirectToAction("ThirdPartyContribution", "VariationRequest", new { Area = "VariationRequest" });
            }

            return RedirectToAction("VariationReason", "VariationRequest", new { Area = "VariationRequest" });
        }

        var viewModel = _mapper.Map<DescriptionsViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(Descriptions))]
    public IActionResult Descriptions(OtherCostsViewModel viewModel)
    {
        return RedirectToAction("IneligibleCosts", "VariationRequest", new { Area = "VariationRequest" });
    }

    #endregion

    #region "Are there any variations to the works on your building that are not eligible for funding?"

    [HttpGet(nameof(IneligibleCosts))]
    public async Task<IActionResult> IneligibleCosts()
    {
        var response = await _sender.Send(GetIneligibleCostsRequest.Request);
        var viewModel = _mapper.Map<IneligibleCostsViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(IneligibleCosts))]
    public async Task<IActionResult> IneligibleCosts(IneligibleCostsViewModel viewModel)
    {
        var validator = new IneligibleCostsViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetIneligibleCostsRequest>(viewModel);
            await _sender.Send(request);

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "VariationRequest", new { Area = "VariationRequest" });
            }

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                if (viewModel.HasVariationIneligibleCosts == ENoYes.Yes)
                {
                    return RedirectToAction("IneligibleCostsChanges", "VariationRequest", new { Area = "VariationRequest" });
                }

                return RedirectToAction("Evidence", "VariationRequest", new { Area = "VariationRequest" });
            }

            return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region "Tell us about changes to costs that are not eligible for funding"

    [HttpGet(nameof(IneligibleCostsChanges))]
    public async Task<IActionResult> IneligibleCostsChanges()
    {
        var response = await _sender.Send(GetIneligibleCostsChangesRequest.Request);

        if (!response.HasCostVariation)
        {
            if (response.IsThirdPartyContributionVariation.GetValueOrDefault(false))
            {
                return RedirectToAction("ThirdPartyContribution", "VariationRequest", new { Area = "VariationRequest" });
            }

            return RedirectToAction("VariationReason", "VariationRequest", new { Area = "VariationRequest" });
        }

        var viewModel = _mapper.Map<IneligibleCostsChangesViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(IneligibleCostsChanges))]
    public async Task<IActionResult> IneligibleCostsChanges(IneligibleCostsChangesViewModel viewModel)
    {
        var validator = new IneligibleCostsChangesViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetIneligibleCostsChangesRequest>(viewModel);
            await _sender.Send(request);

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "VariationRequest", new { Area = "VariationRequest" });
            }

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                return RedirectToAction("Evidence", "VariationRequest", new { Area = "VariationRequest" });
            }

            return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region "Evidence"

    [HttpGet(nameof(Evidence))]
    public async Task<IActionResult> Evidence()
    {
        var response = await _sender.Send(GetEvidenceRequest.Request);
        var viewModel = _mapper.Map<EvidenceViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(Evidence))]
    [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
    public async Task<IActionResult> Evidence(EvidenceViewModel viewModel)
    {
        var validator = new EvidenceViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        if (viewModel.SubmitAction == ESubmitAction.Continue || viewModel.SubmitAction == ESubmitAction.Exit)
        {
            var evidenceUploadedRequest = _mapper.Map<SetEvidenceRequest>(viewModel);
            await _sender.Send(evidenceUploadedRequest);
        }

        if (viewModel.SubmitAction == ESubmitAction.Continue)
        {
            if (viewModel.IsThirdPartyContributionVariation == true)
            {
                return RedirectToAction("ThirdPartyContribution", "VariationRequest", new { Area = "VariationRequest" });
            }

            return viewModel.ReturnUrl is not null
                ? SafeRedirectToAction(viewModel.ReturnUrl, "VariationRequest", new { Area = "VariationRequest" })
                : RedirectToAction("Confirmation", "VariationRequest", new { Area = "VariationRequest" });
        }
        else if (viewModel.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        var request = _mapper.Map<AddEvidenceRequest>(viewModel);
        try
        {
            await _sender.Send(request);
        }
        catch (InvalidFileException ex)
        {
            ModelState.AddModelError(nameof(request.File), ex.Message);
            return View(viewModel);
        }

        return viewModel.ReturnUrl is not null
            ? RedirectToAction("Evidence", "VariationRequest", new { Area = "VariationRequest", returnUrl = viewModel.ReturnUrl })
            : RedirectToAction("Evidence", "VariationRequest", new { Area = "VariationRequest" });
    }

    [HttpGet(nameof(Evidence) + "/Delete")]
    public async Task<IActionResult> DeleteEvidence([FromQuery] DeleteEvidenceRequest request, [FromQuery] string returnUrl)
    {
        await _sender.Send(request);

        return returnUrl is not null
            ? RedirectToAction("Evidence", "VariationRequest", new { Area = "VariationRequest", returnUrl })
            : RedirectToAction("Evidence", "VariationRequest", new { Area = "VariationRequest" });
    }

    #endregion

    #region "Review variation summary"

    [HttpGet(nameof(Confirmation))]
    public async Task<IActionResult> Confirmation([FromQuery] string returnUrl)
    {
        var response = await _sender.Send(GetConfirmationRequest.Request);
        var viewModel = _mapper.Map<ConfirmationViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(Confirmation))]
    public async Task<IActionResult> Confirmation(ConfirmationViewModel viewModel)
    {
        var validator = new ConfirmationViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetConfirmationRequest>(viewModel);
            await _sender.Send(request);

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "VariationRequest", new { Area = "VariationRequest" });
            }

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                return RedirectToAction("CheckYourAnswers", "VariationRequest", new { Area = "VariationRequest" });
            }

            return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region "Check your answers"

    [HttpGet(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers()
    {
        var response = await _sender.Send(GetCheckYourAnswersRequest.Request);
        var viewModel = _mapper.Map<CheckYourAnswersViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(CheckYourAnswers))]
    public IActionResult CheckYourAnswers(CheckYourAnswersViewModel viewModel)
    {
        return RedirectToAction("Declaration", "VariationRequest", new { Area = "VariationRequest" });
    }

    #endregion

    #region "Declaration"

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

        return RedirectToAction("Submitted", "VariationRequest", new { Area = "VariationRequest" });
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
