using AutoMapper;
using FluentValidation.AspNetCore;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.Constants;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.Leaseholders;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.Leaseholders;

namespace HE.Remediation.WebApp.Areas.MonthlyProgressReporting.MonthlyProgressReportingLeaseholders.Controllers;

[Area("MonthlyProgressReportingLeaseholders")]
[Route("MonthlyProgressReporting/Leaseholders")]
[CookieApplicationAuthorise]
public class LeaseholdersController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public LeaseholdersController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart => RedirectToAction("Index", "Leaseholders", new { Area = "MonthlyProgressReportingLeaseholders" });

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var response = await _sender.Send(new GetHaveYouContactedRequest());
        var viewModel = _mapper.Map<HaveYouContactedViewModel>(response);
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Index(HaveYouContactedViewModel model, CancellationToken cancellationToken)
    {
        var validator = new HaveYouContactedViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View(model);
        }

        var request = _mapper.Map<SetHaveYouContactedRequest>(model);
        await _sender.Send(request, cancellationToken);

        if(model.HasContacted == ENoYes.No)
        {
            return RedirectToAction("CheckYourAnswers");
        }

        return RedirectToAction("LastCommunicationDate");
    }

    [HttpGet("LastCommunicationDate")]
    public async Task<IActionResult> LastCommunicationDate()
    {
        var response = await _sender.Send(new GetLastCommunicationDateRequest());
        var viewModel = _mapper.Map<LastCommunicationDateViewModel>(response);
        return View(viewModel);
    }

    [HttpPost("LastCommunicationDate")]
    public async Task<IActionResult> LastCommunicationDate(LastCommunicationDateViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var validator = new LastCommunicationDateViewModelValidator();

        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetLastCommunicationDateRequest>(model);
        var response = await _sender.Send(request, cancellationToken);

        return RedirectToAction("UploadEvidence");
    }

    #region Upload Evidence

    [HttpGet("UploadEvidence")]
    public async Task<IActionResult> UploadEvidence()
    {
        var response = await _sender.Send(new GetUploadEvidenceRequest());
        var viewModel = _mapper.Map<UploadEvidenceViewModel>(response);
        return View(viewModel);
    }

    [HttpPost("UploadEvidence")]
    [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
    public async Task<IActionResult> UploadEvidence(UploadEvidenceViewModel model)
    {
        var validator = new UploadEvidenceViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        if (model.SubmitAction == ESubmitAction.Upload)
        {
            var request = _mapper.Map<SetUploadEvidenceRequest>(model);

            try
            {
                await _sender.Send(request);
            }
            catch (InvalidFileException ex)
            {
                ModelState.AddModelError(nameof(request.File), ex.Message);
                return View(model);
            }

            return RedirectToAction("UploadEvidence");
        }

        return RedirectToAction("CheckYourAnswers");
    }

    [HttpGet("DeleteEvidence")]
    public async Task<IActionResult> DeleteEvidence([FromQuery] DeleteUploadEvidenceRequest request)
    {
        await _sender.Send(request);
        return RedirectToAction("UploadEvidence");
    }

    #endregion

    [HttpGet(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(new GetCheckYourAnswersRequest(), cancellationToken);
        var viewModel = _mapper.Map<CheckYourAnswersViewModel>(response);
        return View(viewModel);
    }

    [HttpPost(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers(CheckYourAnswersViewModel viewModel, CancellationToken cancellationToken)
    {
        await _sender.Send(new SetCheckYourAnswersRequest(), cancellationToken);
        return RedirectToAction("TaskList", "MonthlyProgressReporting", new { Area = "MonthlyProgressReporting" });
    }

}