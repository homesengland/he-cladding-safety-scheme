using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.AddFile;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.DeleteFile;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetClosingReportInformation;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetConfirmation;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetReviewPayment;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetDeclaration;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubContractorCheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubContractorRating;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubContractors;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubmitPayment;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubmitted;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetUpload;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.SetConfirmation;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.SetReviewPayment;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.SetSubContractorRatings;
using HE.Remediation.WebApp.Constants;
using HE.Remediation.WebApp.ViewModels.ClosingReport;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CheckYourAnswersViewModel = HE.Remediation.WebApp.ViewModels.ClosingReport.CheckYourAnswersViewModel;
using CostsViewModel = HE.Remediation.WebApp.ViewModels.ClosingReport.CostsViewModel;
using ReviewPaymentRequestViewModel = HE.Remediation.WebApp.ViewModels.ClosingReport.ReviewPaymentRequestViewModel;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetFinalCheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.SetSubmitPayment;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.SetDeclaration;
using System.Text.Json;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.CheckStatusRequest;

namespace HE.Remediation.WebApp.Areas.ClosingReport.Controller;

[Area("ClosingReport")]
[Route("ClosingReport")]
public class ClosingReportController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ClosingReportController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart =>
        RedirectToAction("Information", "ClosingReport", new {Area = "ClosingReport"});

    [HttpGet("Start")]
    public new async Task<IActionResult> Start()
    {
        var area = await GetArea();

        var response = await _sender.Send(new CheckStatusRequest());

        if (response is not null)
        {
            if (response.IsSubmitted)
            {
                return RedirectToAction("FinalCheckYourAnswers");
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
        var response = await _sender.Send(GetClosingReportInformationRequest.Request);
        var model = new ClosingReportInformationViewModel
        {
            IsSubmitted = response.IsSubmitted,
            ApplicationReferenceNumber = response.ApplicationReferenceNumber,
            BuildingName = response.BuildingName
        };
        return View(model);
    }


    #endregion

    #region Confirmation

    [HttpGet(nameof(Confirmation))]
    public async Task<IActionResult> Confirmation()
    {
        var response = await _sender.Send(GetConfirmationRequest.Request);
        var viewModel = _mapper.Map<ConfirmationViewModel>(response);
        return View(viewModel);
    }

    [HttpPost(nameof(Confirmation))]
    public async Task<IActionResult> Confirmation(ConfirmationViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new ConfirmationViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetConfirmationRequest>(viewModel);
        await _sender.Send(request);

        return submitAction == ESubmitAction.Continue
            ? RedirectToAction("Upload", "ClosingReport",
                new
                {
                    Area = "ClosingReport", UploadType = EClosingReportFileType.ExitFraew
                })
            : RedirectToAction("Index", "StageDiagram", new {Area = "Application"});
    }

    #endregion

    #region Upload

    [HttpGet("upload/{uploadType}")]
    public async Task<ActionResult> Upload(EClosingReportFileType uploadType, string returnUrl)
    {
        var response = await _sender.Send(new GetUploadRequest(uploadType));
        var viewModel = _mapper.Map<UploadViewModel>(response);

        if (string.IsNullOrEmpty(returnUrl))
        {
            switch (uploadType)
            {
                case EClosingReportFileType.ExitFraew:
                    viewModel.ReturnUrl = Url.Action("Confirmation", "ClosingReport");
                    break;
                case EClosingReportFileType.PracticalCompletionCertificate:
                    viewModel.ReturnUrl = Url.Action("Upload", new {uploadType = EClosingReportFileType.ExitFraew});
                    break;
                case EClosingReportFileType.BuildingRegulations:
                    viewModel.ReturnUrl = Url.Action("Upload",
                        new {uploadType = EClosingReportFileType.PracticalCompletionCertificate});
                    break;
                case EClosingReportFileType.LeaseholderResidentCommunication:
                    viewModel.ReturnUrl = Url.Action("Upload",
                        new {uploadType = EClosingReportFileType.BuildingRegulations});
                    break;
                case EClosingReportFileType.FinalCost:
                    viewModel.ReturnUrl = Url.Action("ReviewPaymentRequest");
                    break;
            }
        }
        else
        {
            viewModel.ReturnUrl = returnUrl;
        }

        return View(viewModel);
    }

    [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
    [HttpPost("upload/{uploadType}")]
    public async Task<ActionResult> Upload(UploadViewModel model)
    {
        var validator = new UploadViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        if (model.SubmitAction == ESubmitAction.Upload)
        {
            var request = _mapper.Map<AddFileRequest>(model);
            try
            {
                await _sender.Send(request);
            }
            catch (InvalidFileException ex)
            {
                ModelState.AddModelError(nameof(request.File), ex.Message);
                return View(model);
            }
        }

        if (model.SubmitAction == ESubmitAction.Continue)
        {
            switch (model.UploadType)
            {
                case EClosingReportFileType.ExitFraew:
                    return RedirectToAction("Upload", "ClosingReport",
                        new
                        {
                            Area = "ClosingReport", UploadType = EClosingReportFileType.PracticalCompletionCertificate
                        });
                case EClosingReportFileType.PracticalCompletionCertificate:
                    return RedirectToAction("Upload", "ClosingReport",
                        new {Area = "ClosingReport", UploadType = EClosingReportFileType.BuildingRegulations});
                case EClosingReportFileType.BuildingRegulations:
                    return RedirectToAction("Upload", "ClosingReport",
                        new
                        {
                            Area = "ClosingReport", UploadType = EClosingReportFileType.LeaseholderResidentCommunication
                        });
                case EClosingReportFileType.LeaseholderResidentCommunication:
                    return RedirectToAction("SubContractors", "ClosingReport", new {Area = "ClosingReport"});
                case EClosingReportFileType.FinalCost:
                    return RedirectToAction("FinalCheckYourAnswers", "ClosingReport", new {Area = "ClosingReport"});
            }
        }

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new {area = "Application"});
        }

        return RedirectToAction("Upload", "ClosingReport",
            new {Area = "ClosingReport", model.UploadType, model.ReturnUrl});
    }

    [HttpGet("delete/{uploadType}")]
    public async Task<IActionResult> Delete([FromQuery] DeleteFileRequest request, EClosingReportFileType uploadType,
        [FromQuery] string returnUrl)
    {
        await _sender.Send(request);

        return RedirectToAction("Upload", "ClosingReport",
            new {Area = "ClosingReport", uploadType, returnUrl});
    }

    #endregion

    #region Sub-Contractors

    [HttpGet(nameof(SubContractors))]
    public async Task<IActionResult> SubContractors()
    {
        var response = await _sender.Send(GetSubContractorsRequest.Request);
        var model = _mapper.Map<SubContractorsViewModel>(response);

        if (!response.SubContractors.Any())
        {
            return RedirectToAction("SubmitPayment");
        }

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
                : RedirectToAction(model.ReturnUrl)
            : RedirectToAction("Index", "StageDiagram", new {Area = "Application"});
    }

    [HttpGet(nameof(SubContractorsCheckYourAnswers))]
    public async Task<IActionResult> SubContractorsCheckYourAnswers()
    {
        var response = await _sender.Send(GetSubContractorCheckYourAnswersRequest.Request);
        var model = _mapper.Map<CheckYourAnswersViewModel>(response);
        return View(model);
    }

    #endregion

    #region Submit Payment

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
        var request = _mapper.Map<SetSubmitPaymentRequest>(viewModel);

        var response = await _sender.Send(request);

        if (!response.IsValidRequest)
        {
            ModelState.AddModelError(nameof(CostsViewModel.UnprofiledGrantFunding), response.ValidationMessage);
            return View(viewModel);
        }

        return submitAction == ESubmitAction.Continue
            ? RedirectToAction("ReviewPaymentRequest")
            : RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
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

        return submitAction == ESubmitAction.Continue ? 
                                        RedirectToAction("Upload", "ClosingReport",
                                        new
                                        {
                                            Area = "ClosingReport", UploadType = EClosingReportFileType.FinalCost
                                        }) : 
                                        RedirectToAction("Index", "StageDiagram", new {Area = "Application"});        
    }

    #endregion

    #region Check Your Answers

    [HttpGet(nameof(FinalCheckYourAnswers))]
    public async Task<IActionResult> FinalCheckYourAnswers()
    {
        var response = await _sender.Send(GetFinalCheckYourAnswersRequest.Request);
        var model = _mapper.Map<FinalCheckYourAnswersViewModel>(response);
        return View(model);
    }

    #endregion

    #region Declaration

    [HttpGet(nameof(Declaration))]
    public async Task<IActionResult> Declaration()
    {
        var response = await _sender.Send(GetDeclarationRequest.Request);
        var model = _mapper.Map<DeclarationViewModel>(response);

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
                                        RedirectToAction("Submitted", "ClosingReport",
                                        new
                                        {
                                            Area = "ClosingReport"
                                        }) : 
                                        RedirectToAction("Index", "StageDiagram", new {Area = "Application"});        
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