using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.GetYesNoDeclaration;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.ReasonForNoContributions.Get;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.ReasonForNoContributions.Set;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.ProceedFromAbout;
using HE.Remediation.WebApp.Areas.ClosingReport.Controllers;
using HE.Remediation.WebApp.ViewModels.ClosingReport;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.ClosingReport.ClosingReportEvidenceOfThirdPartyContribution.Controllers;

[Area("ClosingReportEvidenceOfThirdPartyContribution")]
[Route("ClosingReport/EvidenceOfThirdPartyContribution")]
public class EvidenceOfThirdPartyContributionController(ISender sender, IMapper mapper) : BaseAboutController(sender)
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;

    protected EClosingReportFileType ClosingReportFileType => EClosingReportFileType.EvidenceOfThirdPartyContribution;

    protected override EClosingReportTask ClosingReportTask => EClosingReportTask.EvidenceOfThirdPartyContribution;
    protected override IActionResult NextScreenAfterAbout =>
                                        RedirectToAction("YesNoDeclaration", "EvidenceOfThirdPartyContribution",
                                            new { Area = "ClosingReportEvidenceOfThirdPartyContribution" });

    #region YesNoDeclaration

    [HttpGet(nameof(YesNoDeclaration))]
    public async Task<IActionResult> YesNoDeclaration()
    {
        var response = await _sender.Send(GetYesNoDeclarationRequest.Request);

        return View(new ThirdPartyYesNoDeclarationViewModel() { Declaration = response.Declaration, IsSubmitted = response.IsSubmitted, ApplicationReferenceNumber = response.ApplicationReferenceNumber, BuildingName = response.BuildingName });
    }

    [HttpPost(nameof(YesNoDeclaration))]
    public async Task<IActionResult> YesNoDeclaration(ThirdPartyYesNoDeclarationViewModel model)
    {
        var validator = new ThirdPartyYesNoDeclarationViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        await _sender.Send(new SetYesNoDeclarationRequest(model.Declaration.Equals(ENoYes.Yes)));

        if (model.Declaration.Equals(ENoYes.No))
        {
            return RedirectToAction("ReasonNoThirdPartyContributions", "EvidenceOfThirdPartyContribution", new { Area = "ClosingReportEvidenceOfThirdPartyContribution" });
        }

        return RedirectToAction("EvidenceDetails");
    }

    #endregion YesNoDeclaration

    #region ReasonNoThirdPartyContributions

    [HttpGet(nameof(ReasonNoThirdPartyContributions))]
    public async Task<IActionResult> ReasonNoThirdPartyContributions()
    {
        var response = await _sender.Send(GetReasonForNoContributionsRequest.Request);
        var model = _mapper.Map<ReasonNoThirdPartyContributionsViewModel>(response);

        return View(model);
    }

    [HttpPost(nameof(ReasonNoThirdPartyContributions))]
    public async Task<IActionResult> ReasonNoThirdPartyContributions(ReasonNoThirdPartyContributionsViewModel model)
    {
        var validator = new ReasonNoThirdPartyContributionsViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetReasonForNoContributionsRequest>(model);
        await _sender.Send(request);

        await _sender.Send(new UpdateTaskStatusRequest(ClosingReportTask, ETaskStatus.Completed));

        return RedirectToAction("TaskList", "ClosingReport", new { Area = "ClosingReport" });
    }

    #endregion YesNoDeclaration

    #region Evidence Details

    [HttpGet(nameof(EvidenceDetails))]
    public async Task<IActionResult> EvidenceDetails()
    {
        var response = await _sender.Send(GetEvidenceDetailsRequest.Request);
        var viewModel = _mapper.Map<EvidenceOfThirdPartyContributionViewModel>(response);
        viewModel.ReturnUrl = Url.Action("About");
        return View(viewModel);
    }

    [HttpPost(nameof(EvidenceDetails))]
    public async Task<IActionResult> ConfirmEvidenceDetails()
    {
        await _sender.Send(new UpdateTaskStatusRequest(ClosingReportTask, ETaskStatus.Completed));
        return RedirectToAction("TaskList", "ClosingReport", new { Area = "ClosingReport" });
    }
    #endregion
}
