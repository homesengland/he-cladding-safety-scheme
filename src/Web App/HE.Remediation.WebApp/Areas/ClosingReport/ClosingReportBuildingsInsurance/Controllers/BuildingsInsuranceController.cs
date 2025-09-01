using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.BuildingsInsurance.GetBuildingsInsurance;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.BuildingsInsurance.SetBuildingsInsurance;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubContractors;
using HE.Remediation.WebApp.Areas.ClosingReport.Controllers;
using HE.Remediation.WebApp.ViewModels.BuildingsInsurance;
using HE.Remediation.WebApp.ViewModels.ClosingReport;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.ClosingReport.ClosingReportBuildingsInsurance.Controllers;

[Area("ClosingReportBuildingsInsurance")]
[Route("ClosingReport/BuildingsInsurance")]
public class BuildingsInsuranceController(ISender sender, IMapper mapper) : BaseAboutController(sender)
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;

    protected override EClosingReportTask ClosingReportTask => EClosingReportTask.BuildingInsuranceInformation;
    protected override IActionResult NextScreenAfterAbout => 
                                        RedirectToAction("BuildingsInsurance", "BuildingsInsurance", 
                                            new { Area = "ClosingReportBuildingsInsurance" });

    #region Building Insurance
    [HttpGet(nameof(BuildingsInsurance))]
    public async Task<IActionResult> BuildingsInsurance(string returnUrl)
    {
        var response = await _sender.Send(GetBuildingsInsuranceRequest.Request);

        var viewModel = _mapper.Map<BuildingsInsuranceViewModel>(response);
        viewModel.ReturnUrl = Url.Action("About");

        var subContractorResponse = await _sender.Send(GetSubContractorsRequest.Request);
        var model = _mapper.Map<SubContractorsViewModel>(subContractorResponse);
        viewModel.SubcontractorsRequired = model.SubContractors.Any();

        return View(viewModel);
    }

    [HttpPost(nameof(BuildingsInsurance))]
    public async Task<IActionResult> BuildingsInsurance(BuildingsInsuranceViewModel model)
    {
        var validator = new BuildingsInsuranceViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetBuildingsInsuranceRequest>(model);
        await _sender.Send(request);

        return RedirectToAction("TaskList", "ClosingReport", new { Area = "ClosingReport" });
    }

    #endregion
}
