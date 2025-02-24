using AutoMapper;
using HE.Remediation.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BaseInformation.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.CheckYourAnswers.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ConfirmChangeProjectDates.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Costs.Create;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Costs.Delete;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Declaration.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Declaration.Set;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ProjectDates.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ProjectDates.Set;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Submit.Set;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Submitted.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Add;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Delete;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Set;
using HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.WebApp.Constants;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using HE.Remediation.Core.Helpers;
using System.Text.Json;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks;
using HE.Remediation.WebApp.Attributes.Routing;
using HE.Remediation.WebApp.ViewModels.Shared;
using HE.Remediation.Core.UseCase.Shared.Costs.Get;
using HE.Remediation.Core.UseCase.Shared.Costs.Set;

namespace HE.Remediation.WebApp.Areas.ScheduleOfWorks.Controllers;

[Area("ScheduleOfWorks")]
[Route("ScheduleOfWorks")]
public class ScheduleOfWorksController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ScheduleOfWorksController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart => RedirectToAction("StartInformation", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" });

    #region "About this section"

    [HttpGet(nameof(StartInformation))]
    public async Task<IActionResult> StartInformation()
    {
        var response = await _sender.Send(GetBaseInformationRequest.Request);
        var viewModel = _mapper.Map<StartInformationViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(StartInformation))]
    public IActionResult StartInformation(StartInformationViewModel viewModel)
    {
        return RedirectToAction("UploadWorksContract", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" });
    }

    #endregion

    #region "Upload works contract"

    [HttpGet(nameof(UploadWorksContract))]
    public async Task<IActionResult> UploadWorksContract(string returnUrl)
    {
        var response = await _sender.Send(GetWorksContractRequest.Request);
        var viewModel = _mapper.Map<UploadWorksContractViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(UploadWorksContract))]
    [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
    public async Task<IActionResult> UploadWorksContract(UploadWorksContractViewModel viewModel)
    {
        var validator = new UploadWorksContractViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        if (viewModel.SubmitAction == ESubmitAction.Continue || viewModel.SubmitAction == ESubmitAction.Exit)
        {
            var contractsUploadedRequest = _mapper.Map<SetWorksContractRequest>(viewModel);
            await _sender.Send(contractsUploadedRequest);
        }

        if (viewModel.SubmitAction == ESubmitAction.Continue)
        {
            return viewModel.ReturnUrl is not null
                ? RedirectToAction(viewModel.ReturnUrl, "ScheduleOfWorks", new { Area = "ScheduleOfWorks" })
                : RedirectToAction("UploadBuildingControl", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" });
        }

        if (viewModel.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        var request = _mapper.Map<AddWorksContractRequest>(viewModel);
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
            ? RedirectToAction("UploadWorksContract", "ScheduleOfWorks", new { Area = "ScheduleOfWorks", returnUrl = viewModel.ReturnUrl })
            : RedirectToAction("UploadWorksContract", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" });
    }

    [HttpGet(nameof(UploadWorksContract) + "/Delete")]
    public async Task<IActionResult> DeleteWorksContract([FromQuery] DeleteWorksContractRequest request, [FromQuery]string returnUrl)
    {
        await _sender.Send(request);

        return returnUrl is not null
            ? RedirectToAction("UploadWorksContract", "ScheduleOfWorks", new { Area = "ScheduleOfWorks", returnUrl })
            : RedirectToAction("UploadWorksContract", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" });
    }

    #endregion

    #region Upload Building Control

    [HttpGet(nameof(UploadBuildingControl))]
    public async Task<IActionResult> UploadBuildingControl(string returnUrl, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetBuildingControlRequest.Request, cancellationToken);
        var model = _mapper.Map<UploadBuildingControlViewModel>(response);
        model.ReturnUrl = returnUrl;
        return View(model);
    }

    [HttpPost(nameof(UploadBuildingControl))]
    [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
    public async Task<IActionResult> UploadBuildingControl(UploadBuildingControlViewModel model, CancellationToken cancellationToken)
    {
        var validator = new UploadBuildingControlViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        if (model.SubmitAction == ESubmitAction.Upload)
        {
            try
            {
                var request = _mapper.Map<AddBuildingControlFileRequest>(model);
                await _sender.Send(request, cancellationToken);
                return RedirectToAction("UploadBuildingControl", "ScheduleOfWorks", new { Area = "ScheduleOfWorks", model.ReturnUrl });
            }
            catch (InvalidFileException ex)
            {
                ModelState.AddModelError(nameof(model.File), ex.Message);
                return View(model);
            }
        }
        
        if (model.SubmitAction == ESubmitAction.Continue)
        {
            return !string.IsNullOrWhiteSpace(model.ReturnUrl)
                ? RedirectToAction(model.ReturnUrl, "ScheduleOfWorks", new { Area = "ScheduleOfWorks" })
                : RedirectToAction("UploadLeaseholderEngagement", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" });
        }

        return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
    }

    [HttpGet(nameof(UploadBuildingControl) + "/Delete")]
    public async Task<IActionResult> DeleteBuildingControl(
        [FromQuery] DeleteBuildingControlFileRequest request,
        [FromQuery] string returnUrl,
        CancellationToken cancellationToken)
    {
        await _sender.Send(request, cancellationToken);
        return !string.IsNullOrWhiteSpace(returnUrl)
            ? RedirectToAction("UploadBuildingControl", "ScheduleOfWorks", new { Area = "ScheduleOfWorks", returnUrl })
            : RedirectToAction("UploadBuildingControl", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" });
    }

    #endregion

    #region Upload Leaseholder Engagement

    [HttpGet(nameof(UploadLeaseholderEngagement))]
    public async Task<IActionResult> UploadLeaseholderEngagement(string returnUrl, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetLeaseholderEngagementRequest.Request, cancellationToken);
        var model = _mapper.Map<UploadLeaseholderEngagementViewModel>(response);
        model.ReturnUrl = returnUrl;
        return View(model);
    }

    [HttpPost(nameof(UploadLeaseholderEngagement))]
    [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
    public async Task<IActionResult> UploadLeaseholderEngagement(UploadLeaseholderEngagementViewModel model, CancellationToken cancellationToken)
    {
        var validator = new UploadLeaseholderEngagementViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        if (model.SubmitAction == ESubmitAction.Upload)
        {
            try
            {
                var request = _mapper.Map<AddLeaseholderEngagementFileRequest>(model);
                await _sender.Send(request, cancellationToken);
                return RedirectToAction("UploadLeaseholderEngagement", "ScheduleOfWorks",
                    new { Area = "ScheduleOfWorks", model.ReturnUrl });
            }
            catch (InvalidFileException ex)
            {
                ModelState.AddModelError(nameof(model.File), ex.Message);
                return View(model);
            }
        }

        if (model.SubmitAction == ESubmitAction.Continue)
        {
            return !string.IsNullOrWhiteSpace(model.ReturnUrl)
                ? RedirectToAction(model.ReturnUrl, "ScheduleOfWorks", new { Area = "ScheduleOfWorks" })
                : RedirectToAction("ProjectDates", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" });
        }

        return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
    }

    [HttpGet(nameof(UploadLeaseholderEngagement) + "/Delete")]
    public async Task<IActionResult> DeletedLeaseHolderEngagement(
        DeleteLeaseholderEngagementFileRequest request,
        string returnUrl,
        CancellationToken cancellationToken)
    {
        await _sender.Send(request, cancellationToken);
        return !string.IsNullOrWhiteSpace(returnUrl)
            ? RedirectToAction("UploadLeaseholderEngagement", "ScheduleOfWorks", new { Area = "ScheduleOfWorks", returnUrl })
            : RedirectToAction("UploadLeaseholderEngagement", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" });
    }
    
    #endregion

    #region "What are your project start and end dates?"

    [HttpGet(nameof(ProjectDates))]
    public async Task<IActionResult> ProjectDates(string returnUrl)
    {
        var response = await _sender.Send(GetProjectDatesRequest.Request);
        var viewModel = _mapper.Map<ProjectDatesViewModel>(response);

        if (viewModel.ProjectStartDateMonth.HasValue && viewModel.ProjectStartDateYear.HasValue)
        {
            var validator = new ProjectDatesViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
            }
        }

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(ProjectDates))]
    public async Task<IActionResult> ProjectDates(ProjectDatesViewModel viewModel)
    {
        var validator = new ProjectDatesViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var willChangeCostsProfile = await WillProjectDatesChangeAffectCostsProfile(viewModel);
        if (willChangeCostsProfile)
        {
            var projectDatesJson = JsonSerializer.Serialize(viewModel);
            TempData["ProjectDates"] = projectDatesJson;
            return viewModel.ReturnUrl is not null
                ? RedirectToAction("ConfirmChangeProjectDates", "ScheduleOfWorks", new { Area = "ScheduleOfWorks", returnUrl = viewModel.ReturnUrl })
                : RedirectToAction("ConfirmChangeProjectDates", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" });
        }

        var request = _mapper.Map<SetProjectDatesRequest>(viewModel);
        await _sender.Send(request);

        if (viewModel.ReturnUrl is not null)
        {
            return RedirectToAction(viewModel.ReturnUrl, "ScheduleOfWorks", new { Area = "ScheduleOfWorks" });
        }

        return viewModel.SubmitAction == ESubmitAction.Continue
            ? RedirectToAction("FundingInformation", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" })
            : RedirectToAction("Index", "StageDiagram", new { area = "Application" });
    }

    private async Task<bool> WillProjectDatesChangeAffectCostsProfile(ProjectDatesViewModel viewModel)
    {
        var existingCosts = await _sender.Send(GetCostsRequest.Request);
        var previousProjectDates = await _sender.Send(GetProjectDatesRequest.Request);

        return existingCosts is not null &&
               existingCosts.MonthlyCosts != null &&
               existingCosts.MonthlyCosts.Any() &&
               previousProjectDates is not null &&
               (previousProjectDates.ProjectStartDateMonth != viewModel.ProjectStartDateMonth ||
                previousProjectDates.ProjectStartDateYear != viewModel.ProjectStartDateYear ||
                previousProjectDates.ProjectEndDateMonth != viewModel.ProjectEndDateMonth ||
                previousProjectDates.ProjectEndDateYear != viewModel.ProjectEndDateYear);
    }

    #endregion

    #region "Clear your answers to this section"

    [ExcludeRouteRecording]
    [HttpGet(nameof(ConfirmChangeProjectDates))]
    public async Task<IActionResult> ConfirmChangeProjectDates(string returnUrl)
    {
        var projectDatesJson = TempData["ProjectDates"]?.ToString();
        var projectDates = projectDatesJson != null
            ? JsonSerializer.Deserialize<ProjectDatesViewModel>(projectDatesJson)
            : null;

        var response = await _sender.Send(GetConfirmChangeProjectDatesRequest.Request);
        var viewModel = _mapper.Map<ConfirmChangeProjectDatesViewModel>(response);
        if (projectDates is not null)
        {
            _mapper.Map(projectDates, viewModel);
        }

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(ConfirmChangeProjectDates))]
    public async Task<IActionResult> ConfirmChangeProjectDates(ConfirmChangeProjectDatesViewModel viewModel, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        if (viewModel.Proceed == false)
        {
            return viewModel.ReturnUrl is not null
                ? RedirectToAction("ProjectDates", "ScheduleOfWorks", new { Area = "ScheduleOfWorks", returnUrl = viewModel.ReturnUrl })
                : RedirectToAction("ProjectDates", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" });
        }

        await _sender.Send(DeleteCostsRequest.Request);

        var request = _mapper.Map<SetProjectDatesRequest>(viewModel);
        await _sender.Send(request);

        return viewModel.ReturnUrl is not null
            ? RedirectToAction("FundingInformation", "ScheduleOfWorks", new { Area = "ScheduleOfWorks", returnUrl = viewModel.ReturnUrl })
            : RedirectToAction("FundingInformation", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" });
    }

    #endregion

    #region Funding Information Page

    [HttpGet(nameof(FundingInformation))]
    public async Task<IActionResult> FundingInformation(string returnUrl)
    {
        var response = await _sender.Send(GetBaseInformationRequest.Request);
        var viewModel = _mapper.Map<FundingInformationViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(FundingInformation))]
    public async Task<IActionResult> FundingInformation(FundingInformationViewModel viewModel)
    {
        var request = CreateCostsRequest.Request;
        await _sender.Send(request);

        return viewModel.ReturnUrl is not null
            ? RedirectToAction("Milestones", "ScheduleOfWorks", new { Area = "ScheduleOfWorks", returnUrl = viewModel.ReturnUrl })
            : RedirectToAction("Milestones", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" });
    }

    #endregion

    #region "Profile your schedule of works"

    [HttpGet(nameof(Milestones))]
    public async Task<IActionResult> Milestones(string returnUrl)
    {
        var response = await _sender.Send(GetCostsRequest.Request);
        var viewModel = _mapper.Map<MilestonesViewModel>(response);

        viewModel.ReturnUrl = returnUrl;        
        if (viewModel.Costs is not null) viewModel.Costs.IsPaymentRequest = false;
        return View(viewModel);
    }

    [HttpPost(nameof(Milestones))]
    public async Task<IActionResult> Milestones(MilestonesViewModel viewModel)
    {
        var validator = new MilestonesViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        UpdateViewModelCosts(viewModel.Costs, validationResult);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetCostsRequest>(viewModel.Costs);
        await _sender.Send(request);

        return viewModel.SubmitAction == ESubmitAction.Continue
            ? RedirectToAction("PaymentsSummary", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" })
            : RedirectToAction("Index", "StageDiagram", new { area = "Application" });
    }

    [HttpPost(nameof(RecalculateMilestones))]
    public async Task<IActionResult> RecalculateMilestones(MilestonesViewModel viewModel)
    {
        var validator = new MilestonesViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
        }

        UpdateViewModelCosts(viewModel.Costs, validationResult);

        return View("Milestones", viewModel);
    }

    private void UpdateViewModelCosts(CostsViewModel viewModel, ValidationResult validationResult)
    {
        if (viewModel is null) return;

        var calculatedCosts = CostsCalculationHelper.CalculateMonthlyCosts(new MonthlyCostsCalculationRequest
        {
            ApprovedGrantFunding = viewModel.ApprovedGrantFunding,
            GrantPaidToDate = viewModel.GrantPaidToDate,
            MonthlyCosts = viewModel.MonthlyCosts.Select(x => x.Amount ?? 0)
        });

        viewModel.MonthlyCostsTotal = calculatedCosts.TotalMonthlyCosts;
        viewModel.UnprofiledGrantFunding = calculatedCosts.UnprofiledAmount;

        var validationErrors = validationResult.ToDictionary();
        for (var i = 0; i < viewModel.MonthlyCosts.Count; i++)
        {
            var costItem = viewModel.MonthlyCosts[i];
            if (!validationErrors.TryGetValue($"Costs.MonthlyCosts[{i}].AmountText", out _))
            {
                costItem.AmountText = costItem.Amount?.ToString("N0");
            }
        }
    }

    #endregion

    #region "Review your schedule of works"

    [HttpGet(nameof(PaymentsSummary))]
    public async Task<IActionResult> PaymentsSummary()
    {
        var response = await _sender.Send(GetCostsRequest.Request);
        var viewModel = _mapper.Map<PaymentsSummaryViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        if (viewModel.Costs is not null) viewModel.Costs.IsPaymentRequest = false;
        return View(viewModel);
    }

    [HttpPost(nameof(PaymentsSummary))]
    public async Task<IActionResult> PaymentsSummary(PaymentsSummaryViewModel viewModel)
    {
        var validator = new PaymentsSummaryViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            var response = await _sender.Send(GetCostsRequest.Request);
            viewModel = _mapper.Map<PaymentsSummaryViewModel>(response);
            return View(viewModel);
        }

        return viewModel.SubmitAction == ESubmitAction.Continue
            ? RedirectToAction("CheckYourAnswers", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" })
            : RedirectToAction("Index", "StageDiagram", new { area = "Application" });
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
        return RedirectToAction("Declaration", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" });
    }

    #endregion

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
        return RedirectToAction("Submitted", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" });
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
