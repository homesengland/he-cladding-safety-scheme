using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageDeclaration.Declaration.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageDeclaration.Declaration.Set;
using HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageDeclaration;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.WorksPackage.WorksPackageDeclaration.Controllers;

[Area("WorksPackageDeclaration")]
[Route("WorksPackage/Declaration")]
public class DeclarationController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public DeclarationController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart =>
        RedirectToAction("Confirm", "Declaration", new { Area = "WorksPackageDeclaration" });   
    
    #region Confirm

    [HttpGet(nameof(Confirm))]
    public async Task<IActionResult> Confirm()
    {
        var response = await _sender.Send(GetDeclarationRequest.Request);
        var viewModel = _mapper.Map<ConfirmViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(Confirm))]
    public async Task<IActionResult> Confirm(ConfirmViewModel viewModel,
        ESubmitAction submitAction)
    {
        var validator = new ConfirmViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var useCaseRequest = _mapper.Map<SetDeclarationRequest>(viewModel);
        await _sender.Send(useCaseRequest);

        return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
    }

    #endregion
}