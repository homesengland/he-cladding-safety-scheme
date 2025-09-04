using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.BaseInformation.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Description;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.InstallationOfCladding;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Other;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Overview;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Preliminary;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.UnsafeCladding;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CostsTemplate.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.IneligibleCost;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.IneligibleCosts;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.NoQuotes.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.NoQuotes.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.PreferredContractorLinks.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.PreferredContractorLinks.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.RequiresSubcontractors.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.RequiresSubcontractors.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.SoughtQuotes.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.SoughtQuotes.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.StartInformation.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.SubcontractorTeam.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.TotalCosts;
using HE.Remediation.WebApp.Attributes.Routing;
using HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.WorksPackage.WorksPackageCostsScheduling.Controllers;

[Area("WorksPackageCostsScheduling")]
[Route("WorksPackage/CostsScheduling")]
public class CostsSchedulingController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public CostsSchedulingController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart =>
        RedirectToAction("StartInformation", "CostsScheduling", new { Area = "WorksPackageCostsScheduling" });

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
        return RedirectToAction("SoughtQuotes", "CostsScheduling", new { Area = "WorksPackageCostsScheduling" });
    }

    #endregion

    #region Does the preferred contractor have any links to the Responsible Entity, original developer and/or project team firms?
    [HttpGet(nameof(PreferredContractorLinks))]
    public async Task<IActionResult> PreferredContractorLinks()
    {
        var response = await _sender.Send(GetPreferredContractorLinksRequest.Request);
        var viewModel = _mapper.Map<PreferredContractorLinksViewModel>(response);
        return View(viewModel);
    }

    [HttpPost(nameof(PreferredContractorLinks))]
    public async Task<IActionResult> PreferredContractorLinks(PreferredContractorLinksViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new PreferredContractorLinksViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }
        var request = _mapper.Map<SetPreferredContractorLinksRequest>(viewModel);
        await _sender.Send(request);
        return submitAction == ESubmitAction.Continue
            ? RedirectToAction("RequiresSubcontractors", "CostsScheduling", new { Area = "WorksPackageCostsScheduling" })
            : RedirectToAction("TaskList", "WorkPackage", new { area = "WorksPackage" });
    }
    #endregion

    #region Have you sought quotes?

    [HttpGet(nameof(SoughtQuotes))]
    public async Task<IActionResult> SoughtQuotes(string returnUrl)
    {
        var response = await _sender.Send(GetSoughtQuotesRequest.Request);
        var viewModel = _mapper.Map<SoughtQuotesViewModel>(response);

        viewModel.ReturnUrl = returnUrl;

        return View(viewModel);
    }

    [HttpPost(nameof(SoughtQuotes))]
    public async Task<IActionResult> SoughtQuotes(SoughtQuotesViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new SoughtQuotesViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetSoughtQuotesRequest>(viewModel);
        await _sender.Send(request);

        return submitAction == ESubmitAction.Continue
            ? viewModel?.SoughtQuotes is ENoYes.Yes
                ? RedirectToAction("PreferredContractorLinks", "CostsScheduling", new { Area = "WorksPackageCostsScheduling" })
                : RedirectToAction("NoQuotes", "CostsScheduling", new { Area = "WorksPackageCostsScheduling" })
            : RedirectToAction("TaskList", "WorkPackage", new { area = "WorksPackage" });
    }

    #endregion

    #region No quotes explanation

    [HttpGet(nameof(NoQuotes))]
    public async Task<IActionResult> NoQuotes()
    {
        var response = await _sender.Send(GetNoQuotesRequest.Request);
        var viewModel = _mapper.Map<NoQuotesViewModel>(response);

        return View(viewModel);
    }

    [HttpPost(nameof(NoQuotes))]
    public async Task<IActionResult> NoQuotes(NoQuotesViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new NoQuotesViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetNoQuotesRequest>(viewModel);
        await _sender.Send(request);

        return submitAction == ESubmitAction.Continue
            ? RedirectToAction("PreferredContractorLinks", "CostsScheduling", new { Area = "WorksPackageCostsScheduling" })
            : RedirectToAction("TaskList", "WorkPackage", new { area = "WorksPackage" });
    }

    #endregion

    #region Do you require subcontractors

    [HttpGet(nameof(RequiresSubcontractors))]
    public async Task<IActionResult> RequiresSubcontractors()
    {
        var response = await _sender.Send(GetRequiresSubcontractorsRequest.Request);
        var viewModel = _mapper.Map<RequiresSubcontractorsViewModel>(response);

        return View(viewModel);
    }

    [HttpPost(nameof(RequiresSubcontractors))]
    public async Task<IActionResult> RequiresSubcontractors(RequiresSubcontractorsViewModel viewModel,
        ESubmitAction submitAction)
    {
        var validator = new RequiresSubcontractorsViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetRequiresSubcontractorsRequest>(viewModel);
        await _sender.Send(request);

        return submitAction == ESubmitAction.Continue
            ? viewModel?.RequiresSubcontractors is ENoYes.Yes
                ? RedirectToAction("SubcontractorTeam", "CostsScheduling", new { Area = "WorksPackageCostsScheduling" })
                : RedirectToAction("CostsTemplate", "CostsScheduling", new { Area = "WorksPackageCostsScheduling" })
            : RedirectToAction("TaskList", "WorkPackage", new { area = "WorksPackage" });
    }

    #endregion

    #region Tell us about your Subcontractors

    [HttpGet(nameof(SubcontractorTeam))]
    public async Task<IActionResult> SubcontractorTeam(string returnUrl)
    {
        var response = await _sender.Send(GetSubcontractorTeamRequest.Request);
        var viewModel = _mapper.Map<SubcontractorTeamViewModel>(response);

        viewModel.ReturnUrl = returnUrl;

        return View(viewModel);
    }

    [HttpPost(nameof(SubcontractorTeam))]
    public async Task<IActionResult> SubcontractorTeam(SubcontractorTeamViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new SubcontractorTeamViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        return submitAction == ESubmitAction.Exit
            ? RedirectToAction("TaskList", "WorkPackage", new { area = "WorksPackage" })
            : RedirectToAction("CostsTemplate", "CostsScheduling", new { Area = "WorksPackageCostsScheduling" });
    }

    #endregion


    #region Tell us about your costs - Costs template

    [HttpGet(nameof(CostsTemplate))]
    public async Task<IActionResult> CostsTemplate()
    {
        var response = await _sender.Send(GetCostsTemplateRequest.Request);
        var viewModel = _mapper.Map<CostsTemplateViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(CostsTemplate))]
    public IActionResult CostsTemplate(CostsTemplateViewModel viewModel, ESubmitAction submitAction)
    {
        if (submitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
        }

        return RedirectToAction("Costs", "CostsScheduling", new { Area = "WorksPackageCostsScheduling" });
    }

    #endregion

    #region Tell us about your costs

    [HttpGet(nameof(Costs))]
    public async Task<IActionResult> Costs()
    {
        var response = await _sender.Send(GetCostsRequest.Request);
        var model = _mapper.Map<CostsViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(Costs))]
    public async Task<IActionResult> Costs(CostsViewModel viewModel)
    {
        var validator = new CostsViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        return RedirectToAction("CostsDescription", "CostsScheduling", new { Area = "WorksPackageCostsScheduling" });
    }

    [HttpGet(nameof(CostsDescription))]
    public async Task<IActionResult> CostsDescription()
    {
        var response = await _sender.Send(GetCostDescriptionRequest.Request);
        var model = _mapper.Map<CostsDescriptionViewModel>(response);
        return View(model);
    }

    #endregion

    #region Enter Costs

    [HttpGet(nameof(UnsafeCladdingCosts))]
    public async Task<IActionResult> UnsafeCladdingCosts([FromQuery] string returnUrl)
    {
        var response = await _sender.Send(GetUnsafeCladdingCostsRequest.Request);
        var model = _mapper.Map<UnsafeCladdingCostsViewModel>(response);
        model.ReturnUrl = returnUrl;
        return View(model);
    }

    [HttpPost(nameof(UnsafeCladdingCosts))]
    public async Task<IActionResult> UnsafeCladdingCosts(UnsafeCladdingCostsViewModel viewModel)
    {
        var validator = new UnsafeCladdingCostsViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetUnsafeCladdingCostsRequest>(viewModel);
        await _sender.Send(request);

        return viewModel.SubmitAction == ESubmitAction.Continue
            ? SafeRedirectToAction(viewModel.ReturnUrl, "CostsScheduling", new { Area = "WorksPackageCostsScheduling" })
            : RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
    }

    [HttpGet(nameof(InstallationOfCladdingCosts))]
    public async Task<IActionResult> InstallationOfCladdingCosts([FromQuery] string returnUrl)
    {
        var response = await _sender.Send(GetInstallationOfCladdingRequest.Request);
        var model = _mapper.Map<InstallationOfCladdingCostsViewModel>(response);
        model.ReturnUrl = returnUrl;
        return View(model);
    }

    [HttpPost(nameof(InstallationOfCladdingCosts))]
    public async Task<IActionResult> InstallationOfCladdingCosts(InstallationOfCladdingCostsViewModel viewModel)
    {
        var validator = new InstallationOfCladdingCostsViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetInstallationOfCladdingRequest>(viewModel);
        await _sender.Send(request);

        return viewModel.SubmitAction == ESubmitAction.Continue
            ? SafeRedirectToAction(viewModel.ReturnUrl, "CostsScheduling", new { Area = "WorksPackageCostsScheduling" })
            : RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
    }

    [HttpGet(nameof(PreliminaryCosts))]
    public async Task<IActionResult> PreliminaryCosts([FromQuery] string returnUrl)
    {
        var response = await _sender.Send(GetPreliminaryRequest.Request);
        var model = _mapper.Map<PreliminaryCostsViewModel>(response);
        model.ReturnUrl = returnUrl;
        return View(model);
    }

    [HttpPost(nameof(PreliminaryCosts))]
    public async Task<IActionResult> PreliminaryCosts(PreliminaryCostsViewModel viewModel)
    {
        var validator = new PreliminaryCostsViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetPreliminaryRequest>(viewModel);
        await _sender.Send(request);

        return viewModel.SubmitAction == ESubmitAction.Continue
            ? SafeRedirectToAction(viewModel.ReturnUrl, "CostsScheduling", new { Area = "WorksPackageCostsScheduling" })
            : RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
    }

    [HttpGet(nameof(OtherCosts))]
    public async Task<IActionResult> OtherCosts([FromQuery] string returnUrl)
    {
        var response = await _sender.Send(GetOtherCostsRequest.Request);
        var model = _mapper.Map<OtherCostsViewModel>(response);
        model.ReturnUrl = returnUrl;
        return View(model);
    }

    [HttpPost(nameof(OtherCosts))]
    public async Task<IActionResult> OtherCosts(OtherCostsViewModel viewModel)
    {
        var validator = new OtherCostsViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetOtherCostsRequest>(viewModel);
        await _sender.Send(request);

        return viewModel.SubmitAction == ESubmitAction.Continue
            ? SafeRedirectToAction(viewModel.ReturnUrl, "CostsScheduling", new { Area = "WorksPackageCostsScheduling" })
            : RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
    }

    #endregion

    #region IneligibleCosts

    [HttpGet(nameof(IneligibleCost))]
    public async Task<IActionResult> IneligibleCost()
    {
        var response = await _sender.Send(GetIneligibleCostRequest.Request);
        var viewModel = _mapper.Map<IneligibleCostViewModel>(response);

        return View(viewModel);
    }

    [HttpPost(nameof(IneligibleCost))]
    public async Task<IActionResult> IneligibleCost(IneligibleCostViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new IneligibleCostViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetIneligibleCostRequest>(viewModel);
        await _sender.Send(request);

        return submitAction == ESubmitAction.Continue && viewModel?.IneligibleCosts is ENoYes.Yes
            ? RedirectToAction("IneligibleCosts", "CostsScheduling", new { Area = "WorksPackageCostsScheduling" })
            : RedirectToAction("CheckYourAnswers", "CostsScheduling", new { area = "WorksPackage" });
    }

    [HttpGet(nameof(IneligibleCosts))]
    public async Task<IActionResult> IneligibleCosts()
    {
        var response = await _sender.Send(GetIneligibleCostsRequest.Request);
        var model = _mapper.Map<IneligibleCostsViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(IneligibleCosts))]
    public async Task<IActionResult> IneligibleCosts(IneligibleCostsViewModel viewModel)
    {
        var validator = new IneligibleCostsViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetIneligibleCostsRequest>(viewModel);
        await _sender.Send(request);

        return viewModel.SubmitAction == ESubmitAction.Continue
            ? RedirectToAction("TotalCosts", "CostsScheduling", new { Area = "WorksPackageCostsScheduling" })
            : RedirectToAction("CheckYourAnswers", "CostsScheduling", new { Area = "WorksPackage" });
    }

    [HttpGet(nameof(TotalCosts))]
    public async Task<IActionResult> TotalCosts()
    {
        var response = await _sender.Send(GetTotalCostsRequest.Request);
        var model = _mapper.Map<TotalCostsViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(TotalCosts))]
    public async Task<IActionResult> TotalCosts(TotalCostsViewModel viewModel)
    {
        await _sender.Send(SetTotalCostRequest.Request);

        return RedirectToAction("CheckYourAnswers", "CostsScheduling", new { Area = "WorksPackage" });
    }

    [HttpGet(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers()
    {
        var response = await _sender.Send(GetCheckYourAnswersRequest.Request);

        var viewModel = _mapper.Map<CheckYourAnswersViewModel>(response);

        return View(viewModel);
    }

    #endregion
}