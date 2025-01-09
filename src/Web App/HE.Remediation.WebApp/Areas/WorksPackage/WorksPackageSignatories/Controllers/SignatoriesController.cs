using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageSignatories.ConfirmSignatories.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageSignatories.ConfirmSignatories.Set;
using HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageSignatories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.WorksPackage.WorksPackageSignatories.Controllers;

[Area("WorksPackageSignatories")]
[Route("WorksPackage/Signatories")]
public class SignatoriesController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public SignatoriesController(ISender sender, IMapper mapper)
        : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart =>
        RedirectToAction("ConfirmSignatories", "Signatories", new { Area = "WorksPackageSignatories" });

    #region Confirm Signatories

    [HttpGet(nameof(ConfirmSignatories))]
    public async Task<IActionResult> ConfirmSignatories()
    {
        var response = await _sender.Send(GetConfirmSignatoriesRequest.Request);
        var viewModel = _mapper.Map<ConfirmSignatoriesViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(ConfirmSignatories))]
    public async Task<IActionResult> ConfirmSignatories(ConfirmSignatoriesViewModel viewModel,
        ESubmitAction submitAction)
    {
        var validator = new ConfirmSignatoriesViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var useCaseRequest = _mapper.Map<SetConfirmSignatoriesRequest>(viewModel);
        await _sender.Send(useCaseRequest);

        return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
    }

    #endregion
}