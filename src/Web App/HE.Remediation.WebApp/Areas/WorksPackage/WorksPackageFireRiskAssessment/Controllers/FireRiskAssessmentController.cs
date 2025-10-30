using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;
using HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageFireRiskAssessment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.WorksPackage.WorksPackageFireRiskAssessment.Controllers;

[Area("WorksPackageFireRiskAssessment")]
[Route("WorksPackage/FireRiskAssessment")]
public class FireRiskAssessmentController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public FireRiskAssessmentController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }


    protected override IActionResult DefaultStart => RedirectToAction("Information", "FireRiskAssessment", new { Area = "WorksPackageFireRiskAssessment" });

    private IActionResult ExitAction => RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });

    [HttpGet(nameof(Information))]
    public async Task<IActionResult> Information(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetInformationRequest.Request, cancellationToken);
        var model = _mapper.Map<InformationViewModel>(response);
        return View(model);
    }

    #region Report Upload
    [HttpGet(nameof(UploadFireRiskAssessmentReport))]
    public async Task<IActionResult> UploadFireRiskAssessmentReport(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetUploadFireRiskAssessmentReportRequest.Request, cancellationToken);
        var model = _mapper.Map<UploadFireRiskAssessmentReportViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(UploadFireRiskAssessmentReport))]
    public async Task<IActionResult> UploadFireRiskAssessmentReport(UploadFireRiskAssessmentReportViewModel model, CancellationToken cancellationToken)
    {
        var validator = new UploadFireRiskAssessmentReportViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetUploadFireRiskAssessmentReportRequest>(model);
        await _sender.Send(request, cancellationToken);

        return model.VisitedCheckYourAnswers
            ? RedirectToAction("CheckYourAnswers", "FireRiskAssessment", new { Area = "WorksPackageFireRiskAssessment" })
            : RedirectToAction("Report", "FireRiskAssessment", new { Area = "WorksPackageFireRiskAssessment" });
    }

    [HttpGet(nameof(UploadFireRiskAssessmentReport) + "/Delete")]
    public async Task<IActionResult> DeleteFireRiskAssessmentReport([FromQuery] DeleteUploadFireRiskAssessmentReportRequest request, CancellationToken cancellationToken)
    {
        await _sender.Send(request, cancellationToken);
        return RedirectToAction("UploadFireRiskAssessmentReport", "FireRiskAssessment", new { Area = "WorksPackageFireRiskAssessment" });
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
        var validation = new ReportViewModelValidator();
        var validationResult = await validation.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetReportRequest>(model);
        await _sender.Send(request, cancellationToken);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return ExitAction;
        }

        return model.VisitedCheckYourAnswers
            ? RedirectToAction("CheckYourAnswers", "FireRiskAssessment", new { Area = "WorksPackageFireRiskAssessment" })
            : RedirectToAction("FireRisk", "FireRiskAssessment", new { Area = "WorksPackageFireRiskAssessment" });
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
            return ExitAction;
        }

        return model.VisitedCheckYourAnswers
            ? RedirectToAction("CheckYourAnswers", "FireRiskAssessment", new { Area = "WorksPackageFireRiskAssessment" })
            : RedirectToAction("FraDate", "FireRiskAssessment", new { Area = "WorksPackageFireRiskAssessment" });
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
            return ExitAction;
        }

        return model.VisitedCheckYourAnswers
            ? RedirectToAction("CheckYourAnswers", "FireRiskAssessment", new { Area = "WorksPackageFireRiskAssessment" })
            : RedirectToAction("FireRisk", "FireRiskAssessment", new { Area = "WorksPackageFireRiskAssessment" });
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
            return ExitAction;
        }

        if (model.HasInternalFireSafetyRisks == true)
        {
            return RedirectToAction("IdentifiedDefects", "FireRiskAssessment", new { Area = "WorksPackageFireRiskAssessment" });
        }

        return model.VisitedCheckYourAnswers || model.HasInternalFireSafetyRisks == false
            ? RedirectToAction("CheckYourAnswers", "FireRiskAssessment", new { Area = "WorksPackageFireRiskAssessment" })
            : RedirectToAction("Funding", "FireRiskAssessment", new { Area = "WorksPackageFireRiskAssessment" });
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
            ? ExitAction 
            : RedirectToAction("Funding", "FireRiskAssessment", new { Area = "WorksPackageFireRiskAssessment" });
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
            ? RedirectToAction("CheckYourAnswers", "FireRiskAssessment", new { Area = "WorksPackageFireRiskAssessment" })
            : ExitAction;
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

        return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
    }
}
