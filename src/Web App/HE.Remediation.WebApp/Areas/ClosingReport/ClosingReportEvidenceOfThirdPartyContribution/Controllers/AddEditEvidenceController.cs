using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.DeleteEvidence;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.UploadFileEvidence;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.ProceedFromAbout;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.Constants;
using HE.Remediation.WebApp.ViewModels.ClosingReport;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.ClosingReport.ClosingReportEvidenceOfThirdPartyContribution.Controllers;

[Area("ClosingReportEvidenceOfThirdPartyContribution")]
[Route("ClosingReport/AddEditEvidence")]
[CookieApplicationAuthorise]
public class AddEditEvidenceController(ISender sender, IMapper mapper, IApplicationDataProvider applicationDataProvider) : Controller()
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;
    private readonly IApplicationDataProvider _applicationDataProvider = applicationDataProvider;


    #region Add/Edit Evidence Details

    [HttpGet("Details/{id:guid?}")]
    public async Task<IActionResult> Details(Guid? id, [FromQuery] bool viaCheckAnswer = false)
    {
        if (!UserIsAuthorized(id))
        {
            return Forbid();
        }
        var applicationId = _applicationDataProvider.GetApplicationId();
        var response = await _sender.Send(new GetEvidenceDetailRequest() { EvidenceId = id, ApplicationId = applicationId });
        var viewModel = _mapper.Map<AddEditEvidenceDetailsViewModel>(response);

        viewModel.Step = 1;
        viewModel.ViaCheckAnswer = viaCheckAnswer;

        return View(viewModel);
    }

    [HttpPost("Details")]
    public async Task<IActionResult> Details(AddEditEvidenceDetailsViewModel model)
    {
        var validator = new AddEditEvidenceDetailsViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View("Details", model);
        }

        var request = _mapper.Map<SetEvidenceDetailRequest>(model);
        var applicationId = _applicationDataProvider.GetApplicationId();
        request.ApplicationId = applicationId;
        var result = await _sender.Send(request);

        if(result.ApplicationId != applicationId)
        {
            return Forbid();
        }

        if(model.ViaCheckAnswer)
        {
            return RedirectToAction("CheckYourAnswers", new { id = result.EvidenceDetailsResults.Id });
        }
        return RedirectToAction("Details2", new { id = result.EvidenceDetailsResults.Id });
    }

    [HttpGet("Details2/{id:guid?}")]
    public async Task<IActionResult> Details2(Guid? id, [FromQuery] bool viaCheckAnswer = false)
    {
        if(!UserIsAuthorized(id))
        {
            return Forbid();
        }
        var applicationId = _applicationDataProvider.GetApplicationId();
        var response = await _sender.Send(new GetEvidenceDetailRequest() { EvidenceId = id, ApplicationId = applicationId });
        var viewModel = _mapper.Map<AddEditEvidenceDetailsViewModel>(response);

        viewModel.Step = 2;
        viewModel.ViaCheckAnswer = viaCheckAnswer;

        return View(viewModel);
    }

    [HttpPost("Details2")]
    public async Task<IActionResult> Details2(AddEditEvidenceDetailsViewModel model)
    {
        var validator = new AddEditEvidenceDetailsViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View("Details2", model);
        }

        var request = _mapper.Map<SetEvidenceDetailRequest>(model);
        var applicationId = _applicationDataProvider.GetApplicationId();
        request.ApplicationId = applicationId;
        await _sender.Send(request);

        if (model.ViaCheckAnswer)
        {
            return RedirectToAction("CheckYourAnswers", new { id = model.Id });
        }
        return RedirectToAction("EvidenceSubmission", new { id = model.Id });
    }

    #endregion

    #region Evidence Submission

    [HttpGet("EvidenceSubmission/{id:guid}")]
    public async Task<IActionResult> EvidenceSubmission(Guid id, [FromQuery] bool viaCheckAnswer = false)
    {
        if (!UserIsAuthorized(id))
        {
            return Forbid();
        }
        var applicationId = _applicationDataProvider.GetApplicationId();
        var response = await _sender.Send(new GetEvidenceDetailRequest() { EvidenceId = id, ApplicationId = applicationId });
        var viewModel = _mapper.Map<UploadEvidenceSubmissionUploadViewModel>(response);
        viewModel.ViaCheckAnswer = viaCheckAnswer;
        viewModel.ReturnUrl ??= Url.Action("Details2");

        return View(viewModel);
    }

    [HttpPost(nameof(EvidenceSubmission))]
    [RequestSizeLimit(FileUploadConstants.MaxRequestSize50MB)]
    public async Task<IActionResult> EvidenceSubmission(UploadEvidenceSubmissionUploadViewModel model)
    {
        var validator = new UploadEvidenceSubmissionUploadViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        if (model.SubmitAction == ESubmitAction.Upload)
        {
            var request = _mapper.Map<AddThirdPartyEvidenceFileRequest>(model);
            request.ApplicationId = _applicationDataProvider.GetApplicationId();

            try
            {
                await _sender.Send(request);
            }
            catch (InvalidFileException ex)
            {
                ModelState.AddModelError(nameof(request.File), ex.Message);
                return View(model);
            }

            if (model.ViaCheckAnswer)
            {
                return RedirectToAction("CheckYourAnswers", new { id = model.Id });
            }
            return RedirectToAction("EvidenceSubmission", new { id = model.Id });
        }

        if (model.SubmitAction == ESubmitAction.Continue)
        {
            await _sender.Send(new UpdateTaskStatusRequest(EClosingReportTask.EvidenceOfThirdPartyContribution, ETaskStatus.Completed));
        }

        return RedirectToAction("CheckYourAnswers", new { id = model.Id });
    }

    [HttpGet("DeleteEvidenceSubmission/{id:guid}/{fileId:guid}")]
    public async Task<IActionResult> DeleteEvidenceSubmission(Guid id, Guid fileId)
    {
        if (!UserIsAuthorized(id))
        {
            return Forbid();
        }
        var applicationId = _applicationDataProvider.GetApplicationId();
        await _sender.Send(new DeleteThirdPartyEvidenceFileRequest(id, applicationId, fileId));
        return RedirectToAction("EvidenceSubmission", new { id });
    }

    #endregion

    #region Check Your Answers

    [HttpGet("CheckYourAnswers/{id:guid}")]
    public async Task<IActionResult> CheckYourAnswers(Guid id)
    {
        if (!UserIsAuthorized(id))
        {
            return Forbid();
        }
        var applicationId = _applicationDataProvider.GetApplicationId();
        var response = await _sender.Send(new GetEvidenceDetailRequest() { EvidenceId = id, ApplicationId = applicationId });
        var viewModel = _mapper.Map<AddEditEvidenceDetailsViewModel>(response);
        return View(viewModel);
    }

    [HttpPost(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers(AddEditEvidenceDetailsViewModel model)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var response = await _sender.Send(new SetEvidenceSubmitRequest() { EvidenceId = model.Id.Value, ApplicationId = applicationId });
        return RedirectToAction("EvidenceDetails", "EvidenceOfThirdPartyContribution", new { Area = "ClosingReportEvidenceOfThirdPartyContribution" });
    }

    #endregion

    #region Delete Evidence Submission

    [HttpGet("RemoveEvidence/{id:guid}")]
    public async Task<IActionResult> RemoveEvidence(Guid id)
    {
        if (!UserIsAuthorized(id))
        {
            return Forbid();
        }
        var applicationId = _applicationDataProvider.GetApplicationId();
        await _sender.Send(new DeleteEvidenceRequest() { ApplicationId = applicationId, EvidenceId = id });
        return RedirectToAction("EvidenceDetails", "EvidenceOfThirdPartyContribution", new { Area = "ClosingReportEvidenceOfThirdPartyContribution" });
    }

    #endregion

    [HttpGet("CancelAndExit/{id:guid?}")]
    public async Task<IActionResult> CancelAndExit(Guid? id)
    {
        if (!UserIsAuthorized(id))
        {
            return Forbid();
        }
        if (id != null)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();
            var response = await _sender.Send(new GetEvidenceDetailRequest() { EvidenceId = id, ApplicationId = applicationId });

            if (!response.IsSubmitted)
            {
                await _sender.Send(new DeleteEvidenceRequest() { ApplicationId = applicationId, EvidenceId = id.Value });
            }
        }

        return RedirectToAction("TaskList", "ClosingReport", new { Area = "ClosingReport" });
    }


    private bool UserIsAuthorized(Guid? id)
    {
        // applicationId is only set if the user is authorized to access the application.
        // If they are authorized to access the application, then they are also authorized to edit 3rd party evidence.
        var applicationId = _applicationDataProvider.GetApplicationId();
        return applicationId != default; 
    }
}
