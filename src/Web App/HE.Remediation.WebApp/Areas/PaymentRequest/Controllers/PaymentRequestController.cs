using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Helpers;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.CostReport.DeleteCostReport;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.CostReport.SetCostReport;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.DeleteTeamMember;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetAddRole;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetChangeCladdingRemovedDate;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetChangeProjectDates;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetCheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetCladdingRemoved;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetCostsChanged;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetDeclaration;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetPaymentInformation;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetProjectDates;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetProjectTeam;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetRemoveTeamMember;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetReviewPayment;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubmitPayment;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubmitted;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetTeamMember;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetUploadCostReport;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetVariationRequired;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetChangeCladdingRemovedDate;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetChangeProjectDates;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetCladdingRemoved;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetCostsChanged;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetDeclaration;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetProjectDates;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetReviewPayment;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetSelectPayment;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetSubmitPayment;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.Start;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.UpdateTeamMember;
using HE.Remediation.WebApp.Constants;
using HE.Remediation.WebApp.ViewModels.PaymentRequest;
using HE.Remediation.WebApp.ViewModels.PaymentRequest.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetPaymentRequestDetails;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubContractorCheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubContractorRating;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubContractors;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetSubContractorRatings;
using HE.Remediation.WebApp.ViewModels.PaymentRequest.SubcontractorSurvey;
using CheckYourAnswersViewModel = HE.Remediation.WebApp.ViewModels.PaymentRequest.CheckYourAnswersViewModel;
using CostsViewModel = HE.Remediation.WebApp.ViewModels.PaymentRequest.CostsViewModel;
using DeclarationViewModel = HE.Remediation.WebApp.ViewModels.PaymentRequest.DeclarationViewModel;
using DeclarationViewModelValidator = HE.Remediation.WebApp.ViewModels.PaymentRequest.DeclarationViewModelValidator;
using ProjectDatesViewModel = HE.Remediation.WebApp.ViewModels.PaymentRequest.ProjectDatesViewModel;
using ProjectDatesViewModelValidator = HE.Remediation.WebApp.ViewModels.PaymentRequest.ProjectDatesViewModelValidator;
using SubmittedViewModel = HE.Remediation.WebApp.ViewModels.PaymentRequest.SubmittedViewModel;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetThirdPartyContributionsChanged;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetThirdPartyContributionsChanged;
using HE.Remediation.Core.Attributes;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest;

namespace HE.Remediation.WebApp.Areas.PaymentRequest.Controllers;

[Area("PaymentRequest")]
[Route("PaymentRequest")]
public class PaymentRequestController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public PaymentRequestController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart =>
        RedirectToAction("Information", "PaymentRequest", new {Area = "PaymentRequest"});

    [HttpGet("Start/{id:guid}")]
    public async Task<IActionResult> Start(Guid id)
    {
        var request = new SetSelectPaymentRequest
        {
            Id = id
        };

        await _sender.Send(request);

        var area = await GetArea();

        var areaData = new Dictionary<string, Guid>
        {
            {"paymentRequestId", id}
        };

        var areaDataJson = JsonSerializer.Serialize(areaData);
        TempData["AreaDataJson"] = areaDataJson;

        var response = await _sender.Send(new CheckStatusRequest
        {
            PaymentRequestId = id
        });

                    
        if (response is not null)
        {            
            if ((response.IsExpired) || (response.IsSubmitted))
            {
                return RedirectToAction("CheckYourAnswers", "PaymentRequest", new {Area = "PaymentRequest"});
            }            
        }

        if (area is not null)
        {
            var dict = !string.IsNullOrEmpty(area.AreaDataJson)
                ? JsonSerializer.Deserialize<Dictionary<string, Guid>>(area.AreaDataJson) ??
                  new Dictionary<string, Guid>()
                : new Dictionary<string, Guid>();

            if (dict.TryGetValue("paymentRequestId", out var value) && value != id)
            {
                return DefaultStart;
            }
        }

        return area is not null
            ? BuildRedirectAction(area)
            : DefaultStart;
    }

    #region Information

    [HttpGet(nameof(Information))]
    public async Task<IActionResult> Information()
    {
        var response = await _sender.Send(GetPaymentInformationRequest.Request);
        var model = new PaymentRequestInformationViewModel
        {
            IsSubmitted = response.IsSubmitted,
            ApplicationReferenceNumber = response.ApplicationReferenceNumber,
            BuildingName = response.BuildingName
        };
        return View(model);
    }

    #endregion

    #region Profile costs

    [HttpGet(nameof(SubmitPayment))]
    public async Task<IActionResult> SubmitPayment(string returnUrl)
    {
        var response = await _sender.Send(GetSubmitPaymentRequest.Request);
        var viewModel = _mapper.Map<CostsViewModel>(response);
        viewModel.ReturnUrl = returnUrl;

        return View(viewModel);
    }

    [HttpPost(nameof(SubmitPayment))]
    public async Task<IActionResult> SubmitPayment(CostsViewModel viewModel, ESubmitAction submitAction)
    {
        UpdateViewModelCosts(viewModel);

        var validator = new CostsViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            var allowSaveAndReturnLater = submitAction == ESubmitAction.Exit
                                          && validationResult.ToDictionary().TryGetValue("UnprofiledGrantFunding",
                                              out var unprofiledGrantFundingErrors)
                                          && unprofiledGrantFundingErrors.Length == validationResult.Errors.Count;

            if (!allowSaveAndReturnLater)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(viewModel);
            }
        }

        var request = _mapper.Map<SetSubmitPaymentRequest>(viewModel);
        await _sender.Send(request);

        return submitAction == ESubmitAction.Continue
            ? RedirectToAction("ReviewPaymentRequest", "PaymentRequest",
                new {Area = "PaymentRequest", returnUrl = viewModel.ReturnUrl})
            : RedirectToAction("Index", "StageDiagram", new {area = "Application"});
    }

    [HttpPost(nameof(RecalculateMilestones))]
    public async Task<IActionResult> RecalculateMilestones(CostsViewModel viewModel)
    {
        var validator = new CostsViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
        }

        UpdateViewModelCosts(viewModel);

        return View("SubmitPayment", viewModel);
    }

    private void UpdateViewModelCosts(ViewModels.PaymentRequest.CostsViewModel viewModel)
    {
        if (viewModel is null) return;

        var calculatedCosts = CostsCalculationHelper.CalculateMonthlyCosts(new MonthlyCostsCalculationRequest
        {
            ApprovedGrantFunding = viewModel.ApprovedGrantFunding,
            GrantPaidToDate = viewModel.GrantPaidToDate,
            MonthlyCosts = viewModel.MonthlyCosts?.Select(x => x.Amount ?? 0) ?? new List<decimal>(),
            FinalCost = viewModel.FinalMonthCost?.Amount ?? 0,
            AdditionalCost = viewModel.AdditionalCost?.Amount ?? 0,
            CurrentCost = viewModel.CurrentMonth?.Amount ?? 0
        });

        viewModel.CurrentMonthTotal = calculatedCosts.TotalCurrentCost;
        viewModel.MonthlyCostsTotal = calculatedCosts.TotalMonthlyCosts;
        viewModel.UnprofiledGrantFunding = calculatedCosts.UnprofiledAmount;

        if (viewModel.MonthlyCosts != null)
        {
            foreach (var costItem in viewModel.MonthlyCosts)
            {
                if (string.IsNullOrEmpty(costItem.AmountText) || decimal.TryParse(costItem.AmountText, out _))
                {
                    costItem.AmountText = costItem.Amount?.ToString("N0");
                }
            }
        }
    }

    #endregion

    #region Review Payment Request

    [HttpGet(nameof(ReviewPaymentRequest))]
    public async Task<IActionResult> ReviewPaymentRequest(string returnUrl)
    {
        var response = await _sender.Send(GetReviewPaymentRequest.Request);
        var viewModel = _mapper.Map<ReviewPaymentRequestViewModel>(response);
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
        await _sender.Send(request);

        var action = nameof(UploadCostReport);
        action = viewModel.ReturnUrl is null ? action : viewModel.ReturnUrl;

        return submitAction == ESubmitAction.Continue
            ? SafeRedirectToAction(action, "PaymentRequest", new {Area = "PaymentRequest"})
            : RedirectToAction("Index", "StageDiagram", new {Area = "Application"});
    }

    #endregion

    #region Upload Cost Report

    [HttpGet(nameof(UploadCostReport))]
    public async Task<IActionResult> UploadCostReport(string returnUrl)
    {
        var response = await _sender.Send(GetUploadCostReportRequest.Request);
        var viewModel = _mapper.Map<UploadCostReportViewModel>(response);
        viewModel.ReturnUrl = returnUrl;
        viewModel.DeleteParameters = new Dictionary<string, string>
        {
            {"returnUrl", viewModel.ReturnUrl}
        };
        return View(viewModel);
    }

    [HttpPost(nameof(UploadCostReport))]
    [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
    public async Task<IActionResult> UploadCostReport(UploadCostReportViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // this will happen when the request size limit is exceeded, the model is null so manually add the error message
            ModelState.AddModelError("File", "One more more files are larger than 20mb");
            return View(model);
        }

        var validator = new UploadCostReportViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        if (model.SubmitAction == ESubmitAction.Continue)
        {
            var action = nameof(Invoices);
            action = model.ReturnUrl is null ? action : model.ReturnUrl;

            return SafeRedirectToAction(action, "PaymentRequest", new {area = "PaymentRequest"});
        }

        try
        {
            var request = _mapper.Map<SetCostRequest>(model);
            await _sender.Send(request);
        }
        catch (InvalidFileException ex)
        {
            if (ex.Errors is not null)
            {
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
            }
            else
            {
                ModelState.AddModelError(nameof(model.File), ex.Message);
            }

            return View(model);
        }

        return RedirectToAction("UploadCostReport", "PaymentRequest",
            new {area = "PaymentRequest", returnUrl = model.ReturnUrl});
    }

    [HttpGet(nameof(UploadCostReport) + "/Delete")]
    [UserIdentityMustBeTheApplicationUser]
    public async Task<IActionResult> UploadCostReportDelete([FromQuery] Guid fileId, [FromQuery] string returnUrl)
    {
        await _sender.Send(new DeleteCostRequest
        {
            ReturnUrl = returnUrl,
            FileId = fileId
        });
        return RedirectToAction("UploadCostReport", "PaymentRequest",
            new {Area = "PaymentRequest", returnUrl = returnUrl});
    }

    #endregion

    #region Upload Invoices

    [HttpGet(nameof(Invoices))]
    public async Task<IActionResult> Invoices(string returnUrl, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetPaymentRequestInvoicesRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<InvoicesViewModel>(response);
        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(Invoices))]
    [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
    public async Task<IActionResult> Invoices(InvoicesViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            // this will happen when the request size limit is exceeded, the model is null so manually add the error message
            ModelState.AddModelError(nameof(model.File), "One more more files are larger than 100mb");
            return View(model);
        }

        var validator = new InvoicesViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        if (model.SubmitAction == ESubmitAction.Upload)
        {
            var request = _mapper.Map<SetPaymentRequestInvoiceRequest>(model);
            await _sender.Send(request, cancellationToken);
            return RedirectToAction("Invoices", "PaymentRequest", new { Area = "PaymentRequest" });
        }

        if (model.SubmitAction == ESubmitAction.Continue)
        {
            var action = !string.IsNullOrWhiteSpace(model.ReturnUrl) ? model.ReturnUrl : nameof(ProjectDates);
            return SafeRedirectToAction(action, "PaymentRequest", new { Area = "PaymentRequest" });
        }

        return RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
    }

    [HttpGet(nameof(Invoices) + "/Delete")]
    public async Task<IActionResult> DeleteInvoice([FromQuery] DeletePaymentRequestInvoiceRequest request, CancellationToken cancellationToken)
    {
        await _sender.Send(request, cancellationToken);
        return RedirectToAction("Invoices", "PaymentRequest", new { Area = "PaymentRequest", ReturnUrl = request.ReturnUrl });
    }

    #endregion

    #region Project Dates

    [HttpGet(nameof(ProjectDates))]
    public async Task<IActionResult> ProjectDates(string returnUrl)
    {
        var response = await _sender.Send(GetProjectDatesRequest.Request);
        var viewModel = _mapper.Map<ViewModels.PaymentRequest.ProjectDatesViewModel>(response);
        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(ProjectDates))]
    public async Task<IActionResult> ProjectDates(ProjectDatesViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new ProjectDatesViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetProjectDatesRequest>(viewModel);
        var response = await _sender.Send(request);

        if (viewModel?.ProjectDatesChanged == true)
        {
            return submitAction == ESubmitAction.Continue
                ? RedirectToAction(nameof(ChangeProjectDates), "PaymentRequest",
                    new {Area = "PaymentRequest", returnUrl = viewModel.ReturnUrl})
                : RedirectToAction("Index", "StageDiagram", new {Area = "Application"});
        }

        var action = nameof(CladdingRemoved);
        if (response.UnsafeCladdingAlreadyRemoved)
        {
            action = nameof(CostsChanged);
        }

        action = viewModel.ReturnUrl is null ? action : viewModel.ReturnUrl;

        return submitAction == ESubmitAction.Continue
            ? SafeRedirectToAction(action, "PaymentRequest", new {Area = "PaymentRequest"})
            : RedirectToAction("Index", "StageDiagram", new {Area = "Application"});
    }

    #endregion

    #region Change Project Dates

    [HttpGet(nameof(ChangeProjectDates))]
    public async Task<IActionResult> ChangeProjectDates(string returnUrl)
    {
        var response = await _sender.Send(GetChangeProjectDatesRequest.Request);
        var viewModel = _mapper.Map<ChangeProjectDatesViewModel>(response);
        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(ChangeProjectDates))]
    public async Task<IActionResult> ChangeProjectDates(ChangeProjectDatesViewModel viewModel,
        ESubmitAction submitAction)
    {
        var validator = new ChangeProjectDatesViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetChangeProjectDatesRequest>(viewModel);
        var response = await _sender.Send(request);

        var action = nameof(CladdingRemoved);
        if (response.UnsafeCladdingAlreadyRemoved)
        {
            action = nameof(CostsChanged);
        }

        action = viewModel.ReturnUrl is null ? action : viewModel.ReturnUrl;

        return submitAction == ESubmitAction.Continue
            ? SafeRedirectToAction(action, "PaymentRequest", new {Area = "PaymentRequest"})
            : RedirectToAction("Index", "StageDiagram", new {Area = "Application"});
    }

    #endregion

    #region Cladding Removed

    [HttpGet(nameof(CladdingRemoved))]
    public async Task<IActionResult> CladdingRemoved(string returnUrl)
    {
        var response = await _sender.Send(GetCladdingRemovedRequest.Request);
        var viewModel = _mapper.Map<CladdingRemovedViewModel>(response);
        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(CladdingRemoved))]
    public async Task<IActionResult> CladdingRemoved(CladdingRemovedViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new CladdingRemovedViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetCladdingRemovedRequest>(viewModel);
        await _sender.Send(request);

        if (viewModel?.UnsafeCladdingRemoved == true)
        {
            return submitAction == ESubmitAction.Continue
                ? RedirectToAction("ChangeCladdingRemovedDate", "PaymentRequest",
                    new {Area = "PaymentRequest", returnUrl = viewModel.ReturnUrl})
                : RedirectToAction("Index", "StageDiagram", new {Area = "Application"});
        }

        var action = nameof(CostsChanged);
        action = viewModel.ReturnUrl is null
            ? action
            : viewModel.ReturnUrl;

        return submitAction == ESubmitAction.Continue
            ? SafeRedirectToAction(action, "PaymentRequest", new {Area = "PaymentRequest"})
            : RedirectToAction("Index", "StageDiagram", new {Area = "Application"});
    }

    #endregion

    #region Change Cladding Removed

    [HttpGet(nameof(ChangeCladdingRemovedDate))]
    public async Task<IActionResult> ChangeCladdingRemovedDate(string returnUrl)
    {
        var response = await _sender.Send(GetChangeCladdingRemovedDateRequest.Request);
        var viewModel = _mapper.Map<ChangeCladdingRemovedDateViewModel>(response);
        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(ChangeCladdingRemovedDate))]
    public async Task<IActionResult> ChangeCladdingRemovedDate(ChangeCladdingRemovedDateViewModel viewModel,
        ESubmitAction submitAction)
    {
        var validator = new ChangeCladdingRemovedDateViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetChangeCladdingRemovedDateRequest>(viewModel);
        await _sender.Send(request);

        var action = nameof(CostsChanged);
        action = viewModel.ReturnUrl is null
            ? action
            : viewModel.ReturnUrl;

        return submitAction == ESubmitAction.Continue
            ? SafeRedirectToAction(action, "PaymentRequest", new {Area = "PaymentRequest"})
            : RedirectToAction("Index", "StageDiagram", new {Area = "Application"});
    }

    #endregion

    #region Costs Changed

    [HttpGet(nameof(CostsChanged))]
    public async Task<IActionResult> CostsChanged(string returnUrl)
    {
        var response = await _sender.Send(GetCostsChangedRequest.Request);
        var viewModel = _mapper.Map<CostsChangedViewModel>(response);
        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(CostsChanged))]
    public async Task<IActionResult> CostsChanged(CostsChangedViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new CostsChangedViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetCostsChangedRequest>(viewModel);
        var response = await _sender.Send(request);

        if (viewModel.ReturnUrl is not null && response?.CostsChanged == true)
        {
            return submitAction == ESubmitAction.Continue
                ? RedirectToAction("VariationRequired", "PaymentRequest",
                    new { Area = "PaymentRequest", returnUrl = viewModel.ReturnUrl })
                : RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
        }

        var action = nameof(ThirdPartyContributionsChanged);
        action = viewModel.ReturnUrl is null
            ? action
            : viewModel.ReturnUrl;

        return submitAction == ESubmitAction.Continue
            ? SafeRedirectToAction(action, "PaymentRequest", new {Area = "PaymentRequest"})
            : RedirectToAction("Index", "StageDiagram", new {Area = "Application"});
    }

    #endregion

    #region Third Party Contributions

    [HttpGet(nameof(ThirdPartyContributionsChanged))]
    public async Task<IActionResult> ThirdPartyContributionsChanged(string returnUrl)
    {
        var response = await _sender.Send(GetThirdPartyContributionsChangedRequest.Request);
        var viewModel = _mapper.Map<ThirdPartyContributionsChangedViewModel>(response);
        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(ThirdPartyContributionsChanged))]
    public async Task<IActionResult> ThirdPartyContributionsChanged(ThirdPartyContributionsChangedViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new ThirdPartyContributionsChangedViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetThirdPartyContributionsChangedRequest>(viewModel);
        var response = await _sender.Send(request);

        if ((viewModel.ReturnUrl is null && (response?.EndDateSlipped == true || response?.CostsChanged == true)) ||
            response?.ThirdPartyContributionsChanged == true)
        {
            return submitAction == ESubmitAction.Continue
                ? RedirectToAction("VariationRequired", "PaymentRequest",
                    new { Area = "PaymentRequest", returnUrl = viewModel.ReturnUrl })
                : RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
        }

        var action = nameof(ProjectTeamOverview);
        action = viewModel.ReturnUrl is null
            ? action
            : viewModel.ReturnUrl;

        return submitAction == ESubmitAction.Continue
            ? SafeRedirectToAction(action, "PaymentRequest", new { Area = "PaymentRequest" })
            : RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
    }

    #endregion

    #region Variation required

    [HttpGet(nameof(VariationRequired))]
    public async Task<IActionResult> VariationRequired(string returnUrl)
    {
        var response = await _sender.Send(GetVariationRequiredRequest.Request);
        var viewModel = _mapper.Map<VariationRequiredViewModel>(response);
        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    #endregion

    #region Project Team Overview

    [HttpGet(nameof(ProjectTeamOverview))]
    public async Task<IActionResult> ProjectTeamOverview(string returnUrl)
    {
        var response = await _sender.Send(GetProjectTeamRequest.Request);
        var viewModel = _mapper.Map<ProjectTeamViewModel>(response);
        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(ProjectTeamOverview))]
    public async Task<IActionResult> ProjectTeamOverview(ProjectTeamViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new ProjectTeamViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        if (submitAction == ESubmitAction.Continue)
        {
            var paymentRequestDetails = await _sender.Send(GetPaymentRequestDetailsRequest.Request);

            var action = nameof(CheckYourAnswers);

            // Show subcontractor survey on payment request 4 ONLY if we have sub contractors
            if (paymentRequestDetails.Version == 4)
            {                
                if ((paymentRequestDetails.SubContractorCount != null) && (paymentRequestDetails.SubContractorCount > 0))
                {                
                    action = nameof(SubContractors);                    
                }
            }

            action = viewModel.ReturnUrl ?? action;

            return SafeRedirectToAction(action, "PaymentRequest", new {Area = "PaymentRequest"});
        }

        return View(viewModel);
    }

    #endregion

    #region Add role

    [HttpGet(nameof(AddRole))]
    public async Task<IActionResult> AddRole(string returnUrl)
    {
        var response = await _sender.Send(GetAddRoleRequest.Request);
        var viewModel = _mapper.Map<AddRoleViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(AddRole))]
    public async Task<IActionResult> AddRole(AddRoleViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new AddRoleViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            if (submitAction == ESubmitAction.Continue)
            {
                return RedirectToAction("TeamMember", "PaymentRequest",
                    new
                    {
                        Area = "PaymentRequest", TeamRole = (int) viewModel.TeamRole, returnUrl = viewModel.ReturnUrl
                    });
            }

            return RedirectToAction("Index", "StageDiagram", new {area = "Application"});
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region Team Member

    [HttpGet("TeamMember/{teamRole:int}/{teamMemberId:guid?}")]
    public async Task<IActionResult> TeamMember([FromRoute] GetTeamMemberRequest request,
        [FromQuery] string returnUrl = null)
    {
        try
        {
            var response = await _sender.Send(request);
            var model = _mapper.Map<TeamMemberViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost(nameof(TeamMember))]
    public async Task<IActionResult> TeamMember(TeamMemberViewModel model)
    {
        var validator = new TeamMemberViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<UpdateTeamMemberRequest>(model);
        var teamMemberId = await _sender.Send(request);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new {area = "Application"});
        }

        return RedirectToAction("ProjectTeamOverview", "PaymentRequest",
            new {Area = "PaymentRequest", TeamMemberId = teamMemberId, returnUrl = model.ReturnUrl});
    }

    [HttpGet(nameof(RemoveMember))]
    public async Task<IActionResult> RemoveMember(Guid teamMemberId, [FromQuery] string returnUrl = null)
    {
        var response = await _sender.Send(new GetRemoveTeamMemberRequest {TeamMemberId = teamMemberId});
        var viewModel = _mapper.Map<RemoveMemberViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(RemoveMember))]
    public async Task<IActionResult> RemoveMember(RemoveMemberViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new RemoveMemberViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            if (viewModel.Confirm == true)
            {
                var request = _mapper.Map<DeleteTeamMemberRequest>(viewModel);
                await _sender.Send(request);
            }

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                return RedirectToAction("ProjectTeamOverview", "PaymentRequest",
                    new {Area = "PaymentRequest", returnUrl = viewModel.ReturnUrl});
            }

            return RedirectToAction("Index", "StageDiagram", new {area = "Application"});
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region Sub-Contractors Surveys

    [HttpGet(nameof(SubContractors))]
    public async Task<IActionResult> SubContractors()
    {
        var response = await _sender.Send(GetSubContractorsRequest.Request);
        var model = _mapper.Map<SubContractorsViewModel>(response);
        return View(model);
    }

    [HttpGet("SubContractorRatings/{id:guid}/{name}")]
    public async Task<IActionResult> SubContractorRatings(Guid id, string returnUrl)
    {
        var response = await _sender.Send(new GetSubContractorRatingsRequest(id));
        var model = _mapper.Map<SubContractorRatingsViewModel>(response);

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

        return model.SubmitAction == ESubmitAction.Continue
            ? string.IsNullOrEmpty(model.ReturnUrl)
                ? RedirectToAction("SubContractors")
                : SafeRedirectToAction(model.ReturnUrl, null, null)
            : RedirectToAction("Index", "StageDiagram", new {Area = "Application"});
    }

    [HttpGet(nameof(SubContractorsCheckYourAnswers))]
    public async Task<IActionResult> SubContractorsCheckYourAnswers()
    {
        var response = await _sender.Send(GetSubContractorCheckYourAnswersRequest.Request);
        var model = _mapper.Map<ViewModels.PaymentRequest.SubcontractorSurvey.CheckYourAnswersViewModel>(response);
        return View(model);
    }

    #endregion

    #region Check Your Answers

    [HttpGet(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers()
    {
        var response = await _sender.Send(GetCheckYourAnswersRequest.Request);
        var viewModel = _mapper.Map<CheckYourAnswersViewModel>(response);
        
        var paymentRequestDetails = await _sender.Send(GetPaymentRequestDetailsRequest.Request);
        
        var action = nameof(ProjectTeamOverview);
        if (paymentRequestDetails.Version == 4)
        {                
            if ((paymentRequestDetails.SubContractorCount != null) && (paymentRequestDetails.SubContractorCount > 0))
            {                
                action = nameof(SubContractorsCheckYourAnswers);                    
            }
        }
        
        viewModel.ReturnUrl = action;
        
        return View(viewModel);
    }

    #endregion

    #region Declaration

    [HttpGet(nameof(Declaration))]
    public async Task<IActionResult> Declaration()
    {
        var response = await _sender.Send(GetDeclarationRequest.Request);
        var viewModel = _mapper.Map<DeclarationViewModel>(response);
        return View(viewModel);
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

        return submitAction == ESubmitAction.Continue
            ? RedirectToAction("Submitted", "PaymentRequest", new {Area = "PaymentRequest"})
            : RedirectToAction("Index", "StageDiagram", new {Area = "Application"});
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