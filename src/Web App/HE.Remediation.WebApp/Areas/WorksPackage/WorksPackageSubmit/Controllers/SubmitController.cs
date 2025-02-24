using AutoMapper;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageSubmit.Submit.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageSubmit.Submit.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageSubmit.Submitted.Get;
using HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageSubmit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.WorksPackage.WorksPackageSubmit.Controllers;

[Area("WorksPackageSubmit")]
[Route("WorksPackage/Submit")]
public class SubmitController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public SubmitController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart =>
        RedirectToAction("Submit", "Submit", new { Area = "WorksPackageSubmit" });

    #region Submit

    [HttpGet(nameof(Submit))]
    public async Task<IActionResult> Submit()
    {
        var response = await _sender.Send(GetSubmitRequest.Request);
        var viewModel = _mapper.Map<SubmitViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(Submit))]
    public async Task<IActionResult> Submit(SubmitViewModel viewModel, ESubmitAction submitAction)
    {
        await _sender.Send(SetSubmitRequest.Request);

        return RedirectToAction("Submitted", "Submit", new { Area = "WorksPackageSubmit" });
    }

    #endregion

    #region Submitted

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