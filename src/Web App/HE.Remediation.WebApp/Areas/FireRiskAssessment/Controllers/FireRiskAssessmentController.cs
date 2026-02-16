using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;
using HE.Remediation.Core.UseCase.Areas.FireRiskAssessment.UploadFireRiskAssessmentReport.DeleteFireRiskAssessmentReport;
using HE.Remediation.Core.UseCase.Areas.FireRiskAssessment.UploadFireRiskAssessmentReport.GetFireRiskAssessmentReport;
using HE.Remediation.Core.UseCase.Areas.FireRiskAssessment.UploadFireRiskAssessmentReport.UploadFireRiskAssessmentReport;
using HE.Remediation.WebApp.Constants;
using HE.Remediation.WebApp.ViewModels.FireRiskAssessment;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.FireRiskAssessment.Controllers;

[Area("FireRiskAssessment")]
[Route("FireRiskAssessment")]
public class FireRiskAssessmentController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public FireRiskAssessmentController(ISender sender, IMapper mapper, IApplicationDataProvider applicationDataProvider) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
        _applicationDataProvider = applicationDataProvider;
    }

    protected override IActionResult DefaultStart => RedirectToAction("Information", "FireRiskAssessment", new { Area = "FireRiskAssessment" });

    [HttpGet(nameof(Information))]
    public async Task<IActionResult> Information(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetAboutThisSectionRequest.Request, cancellationToken);
        var model = _mapper.Map<AboutThisSectionViewModel>(response);
        return View(model);
    }

    #region What works does your building need

    [HttpGet(nameof(FraBuildingWorkType))]
    public async Task<IActionResult> FraBuildingWorkType(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetBuildingWorkTypeRequest.Request, cancellationToken);
        var model = _mapper.Map<FraBuildingWorkTypeViewModel>(response);
        return View(model);
    }


    [HttpPost(nameof(FraBuildingWorkType))]
    public async Task<IActionResult> FraBuildingWorkType(FraBuildingWorkTypeViewModel model, CancellationToken cancellationToken)
    {
        var validator = new FraBuildingWorkTypeViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetBuildingWorkTypeRequest>(model);

        var response = await _sender.Send(request, cancellationToken);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }

        return !model.VisitedCheckYourAnswers
            ? RedirectToAction("HasFra", "FireRiskAssessment", new { Area = "FireRiskAssessment" })
            : RedirectToAction("CheckYourAnswers", "FireRiskAssessment", new { Area = "FireRiskAssessment" });
    }

    #endregion
    [HttpGet(nameof(HasFra))]
    public async Task<IActionResult> HasFra(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetHasFraRequest.Request, cancellationToken);
        var model = _mapper.Map<HasFraViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(HasFra))]
    public async Task<IActionResult> HasFra(HasFraViewModel model, CancellationToken cancellationToken)
    {
        var validator = new HasFraViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetHasFraRequest>(model);

        var response = await _sender.Send(request, cancellationToken);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }

        if (model.HasFra == false)
        {
            return RedirectToAction("NoCurrentFra", "FireRiskAssessment", new { Area = "FireRiskAssessment" });
        }

        return !response.VisitedCheckYourAnwers
            ? RedirectToAction("UploadFireRiskAssessmentReport", "FireRiskAssessment", new { Area = "FireRiskAssessment" })
            : RedirectToAction("CheckYourAnswers", "FireRiskAssessment", new { Area = "FireRiskAssessment" });
    }

    #region Upload fire risk Assessment Report

    [HttpGet(nameof(UploadFireRiskAssessmentReport))]
    public async Task<IActionResult> UploadFireRiskAssessmentReport(string returnUrl, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetFireRiskReportAssessmentReportRequest.Request, cancellationToken);

        var model = _mapper.Map<UploadFireRiskAssessmentReportViewModel>(response);

        return View(model);
    }


    [HttpPost(nameof(UploadFireRiskAssessmentReport))]
    [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
    public async Task<IActionResult> UploadFireRiskAssessmentReport(UploadFireRiskAssessmentReportViewModel viewModel, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var response = await _sender.Send(GetFireRiskReportAssessmentReportRequest.Request, cancellationToken);
            viewModel = _mapper.Map<UploadFireRiskAssessmentReportViewModel>(response);

            ModelState.AddModelError(nameof(viewModel.FraReport), "Your file is greater than 100mb");
            return View(viewModel);
        }

        var validator = new UploadFireRiskAssessmentReportViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = new UploadFireRiskAssessmentReportRequest
        {
            FraReportFile = viewModel.SubmitAction == ESubmitAction.Upload ? viewModel.FraReport : null,
            FraAlreadyUploaded = viewModel.AddedFra is not null,
            FireRiskAssessmentType = viewModel.FireRiskAssessmentType,
            ApplicationScheme = _applicationDataProvider.GetApplicationScheme()
        };

        try
        {
            await _sender.Send(request, cancellationToken);
        }
        catch (InvalidFileException ex)
        {
            if (viewModel.SubmitAction == ESubmitAction.Upload)
            {
                ModelState.AddModelError(nameof(viewModel.FraReport), ex.Message);
            }
            else
            {
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
            }

            return View(viewModel);
        }

        if (viewModel.SubmitAction == ESubmitAction.Upload)
        {
            // Stay on the same screen with file now shown in summary list
            return RedirectToAction("UploadFireRiskAssessmentReport", "FireRiskAssessment", new { Area = "FireRiskAssessment" });
        }

        if (viewModel.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }

        return !viewModel.VisitedCheckYourAnswers
            ? RedirectToAction("Report", "FireRiskAssessment", new { area = "FireRiskAssessment" })
            : RedirectToAction("CheckYourAnswers", "FireRiskAssessment", new { Area = "FireRiskAssessment" });
    }

    [HttpGet("DeleteAssessmentReport")]
    public async Task<IActionResult> DeleteAssessmentReport([FromQuery] DeleteFireRiskAssessmentRequest request, CancellationToken cancellationToken)
    {
        await _sender.Send(request, cancellationToken);
        return RedirectToAction("UploadFireRiskAssessmentReport", "FireRiskAssessment", new { Area = "FireRiskAssessment" });
    }

    #endregion

    [HttpGet(nameof(Report))]
    public async Task<IActionResult> Report(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetReportRequest.Request, cancellationToken);
        var model = _mapper.Map<ReportViewModel>(response);

        return View(model);
    }

    [HttpPost(nameof(Report))]
    public async Task<IActionResult> Report(ReportViewModel model, CancellationToken cancellationToken)
    {
        var validator = new ReportViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetReportRequest>(model);

        await _sender.Send(request, cancellationToken);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }

        return !model.VisitedCheckYourAnswers
            ? RedirectToAction("FireRisk", "FireRiskAssessment", new { Area = "FireRiskAssessment" })
            : RedirectToAction("CheckYourAnswers", "FireRiskAssessment", new { Area = "FireRiskAssessment" });
    }

    [HttpGet(nameof(OtherAssessor))]
    public async Task<IActionResult> OtherAssessor(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetOtherAssessorRequest.Request, cancellationToken);
        var model = _mapper.Map<OtherAssessorViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(OtherAssessor))]
    public async Task<IActionResult> OtherAssessor(OtherAssessorViewModel model, CancellationToken cancellationToken)
    {
        var validator = new OtherAssessorViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetOtherAssessorRequest>(model);
        await _sender.Send(request, cancellationToken);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }

        return !model.VisitedCheckYourAnswers
            ? RedirectToAction("FraDate", "FireRiskAssessment", new { Area = "FireRiskAssessment" })
            : RedirectToAction("CheckYourAnswers", "FireRiskAssessment", new { Area = "FireRiskAssessment" });
    }

    [HttpGet(nameof(FraDate))]
    public async Task<IActionResult> FraDate(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetFraDateRequest.Request, cancellationToken);
        var model = _mapper.Map<FraDateViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(FraDate))]
    public async Task<IActionResult> FraDate(FraDateViewModel model, CancellationToken cancellationToken)
    {
        var validator = new FraDateViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetFraDateRequest>(model);
        await _sender.Send(request, cancellationToken);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }

        return !model.VisitedCheckYourAnswers
            ? RedirectToAction("FireRisk", "FireRiskAssessment", new { Area = "FireRiskAssessment" })
            : RedirectToAction("CheckYourAnswers", "FireRiskAssessment", new { Area = "FireRiskAssessment" });
    }

    [HttpGet(nameof(FireRisk))]
    public async Task<IActionResult> FireRisk(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetFireRiskRequest.Request, cancellationToken);
        var model = _mapper.Map<FireRiskViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(FireRisk))]
    public async Task<IActionResult> FireRisk(FireRiskViewModel model, CancellationToken cancellationToken)
    {
        var validator = new FireRiskViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetFireRiskRequest>(model);
        await _sender.Send(request, cancellationToken);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }

        if (model.HasInternalFireSafetyRisks == true)
        {
            return RedirectToAction("IdentifiedDefects", "FireRiskAssessment", new { Area = "FireRiskAssessment" });
        }

        return model.VisitedCheckYourAnswers || model.HasInternalFireSafetyRisks == false || model.ApplicationScheme == EApplicationScheme.ResponsibleActorsScheme
            ? RedirectToAction("CheckYourAnswers", "FireRiskAssessment", new { Area = "FireRiskAssessment" })
            : RedirectToAction("Funding", "FireRiskAssessment", new { Area = "FireRiskAssessment" });
    }

    [HttpGet(nameof(IdentifiedDefects))]
    public async Task<IActionResult> IdentifiedDefects(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetIdentifiedDefectsRequest.Request, cancellationToken);
        var model = _mapper.Map<IdentifiedDefectsViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(IdentifiedDefects))]
    public async Task<IActionResult> IdentifiedDefects(IdentifiedDefectsViewModel model, CancellationToken cancellationToken)
    {
        var validator = new IdentifiedDefectsViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetIdentifiedDefectsRequest>(model);
        await _sender.Send(request, cancellationToken);

        return model.SubmitAction == ESubmitAction.Exit
            ? RedirectToAction("Index", "TaskList", new { Area = "Application" })
            : model.ApplicationScheme == EApplicationScheme.ResponsibleActorsScheme
                ? RedirectToAction("CheckYourAnswers", "FireRiskAssessment", new { Area = "FireRiskAssessment" })
                : RedirectToAction("Funding", "FireRiskAssessment", new { Area = "FireRiskAssessment" });
    }

    [HttpGet(nameof(Funding))]
    public async Task<IActionResult> Funding(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetFundingRequest.Request, cancellationToken);
        var model = _mapper.Map<FundingViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(Funding))]
    public async Task<IActionResult> Funding(FundingViewModel model, CancellationToken cancellationToken)
    {
        var validator = new FundingViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetFundingRequest>(model);
        await _sender.Send(request, cancellationToken);

        return model.SubmitAction == ESubmitAction.Continue
            ? RedirectToAction("CheckYourAnswers", "FireRiskAssessment", new { Area = "FireRiskAssessment" })
            : RedirectToAction("Index", "TaskList", new { Area = "Application" });
    }

    [HttpGet(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetCheckYourAnswersRequest.Request, cancellationToken);
        var model = _mapper.Map<CheckYourAnswersViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers(CheckYourAnswersViewModel model, CancellationToken cancellationToken)
    {
        var validator = new CheckYourAnswersViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        await _sender.Send(SetCheckYourAnswersRequest.Request, cancellationToken);

        return RedirectToAction("Index", "TaskList", new { Area = "Application" });
    }

    [HttpGet(nameof(NoCurrentFra))]
    public Task<IActionResult> NoCurrentFra()
    {
        return Task.FromResult<IActionResult>(View());
    }
}
