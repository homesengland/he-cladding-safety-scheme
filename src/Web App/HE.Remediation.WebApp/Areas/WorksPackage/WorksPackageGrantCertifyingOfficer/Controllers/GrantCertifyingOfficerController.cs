using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.BaseInformation.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AddressDetails.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AddressDetails.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AddressDetails.SetManual;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AuthorisedSignatories.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AuthorisedSignatories.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.CheckYourAnswers.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.CheckYourAnswers.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Confirm.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Confirm.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Details.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Details.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.DutyOfCareDeed.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.IsCorrectPerson.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Reset;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Select.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Select.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.StartInformation.Get;
using HE.Remediation.WebApp.ViewModels.Location;
using HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageGrantCertifyingOfficer;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.WorksPackage.WorksPackageGrantCertifyingOfficer.Controllers;

[Area("WorksPackageGrantCertifyingOfficer")]
[Route("WorksPackage/GrantCertifyingOfficer")]
public class GrantCertifyingOfficerController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GrantCertifyingOfficerController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart =>
        RedirectToAction("StartInformation", "GrantCertifyingOfficer", new { Area = "WorksPackageGrantCertifyingOfficer" });

    #region Start Information

    [HttpGet(nameof(StartInformation))]
    public async Task<IActionResult> StartInformation()
    {
        var response = await _sender.Send(GetStartInformationRequest.Request);
        var viewModel = _mapper.Map<StartInformationViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(StartInformation))]
    public IActionResult StartInformation(StartInformationViewModel viewModel, ESubmitAction submitAction)
    {
        return RedirectToAction("SelectGrantCertifyingOfficer", "GrantCertifyingOfficer", new { Area = "WorksPackageGrantCertifyingOfficer" });
    }

    #endregion

    #region Who is the Grant Certifying Officer?

    [HttpGet(nameof(SelectGrantCertifyingOfficer))]
    public async Task<IActionResult> SelectGrantCertifyingOfficer()
    {
        var response = await _sender.Send(GetSelectRequest.Request);
        var viewModel = _mapper.Map<SelectViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(SelectGrantCertifyingOfficer))]
    public async Task<IActionResult> SelectGrantCertifyingOfficer(SelectViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new SelectViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetSelectRequest>(viewModel);

            await _sender.Send(request);

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "ProgressReporting", new { Area = "ProgressReporting" });
            }

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                return RedirectToAction("ConfirmGrantCertifyingOfficer", "GrantCertifyingOfficer", new { Area = "WorksPackageGrantCertifyingOfficer" });
            }

            return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region Confirm your Grant Certifying Officer's Details

    [HttpGet(nameof(ConfirmGrantCertifyingOfficer))]
    public async Task<IActionResult> ConfirmGrantCertifyingOfficer()
    {
        var response = await _sender.Send(GetConfirmRequest.Request);
        var viewModel = _mapper.Map<ConfirmViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(ConfirmGrantCertifyingOfficer))]
    public async Task<IActionResult> ConfirmGrantCertifyingOfficer(ConfirmViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new ConfirmViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetConfirmRequest>(viewModel);

            await _sender.Send(request);

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "ProgressReporting", new { Area = "ProgressReporting" });
            }

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                return
                    viewModel.CertifyingOfficerResponse == ECertifyingOfficerResponse.Update ||
                    viewModel.CertifyingOfficerResponse == ECertifyingOfficerResponse.No
                    ? RedirectToAction("GrantCertifyingOfficerDetails", "GrantCertifyingOfficer", new { Area = "WorksPackageGrantCertifyingOfficer" })
                    : RedirectToAction("GrantCertifyingOfficerAddressDetails", "GrantCertifyingOfficer", new { Area = "WorksPackageGrantCertifyingOfficer" });
            }

            return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region Grant Certifying Officer Details

    [HttpGet(nameof(GrantCertifyingOfficerDetails))]
    public async Task<IActionResult> GrantCertifyingOfficerDetails()
    {
        var response = await _sender.Send(GetDetailsRequest.Request);
        var viewModel = _mapper.Map<DetailsViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(GrantCertifyingOfficerDetails))]
    public async Task<IActionResult> GrantCertifyingOfficerDetails(DetailsViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new DetailsViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetDetailsRequest>(viewModel);

            await _sender.Send(request);

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "ProgressReporting", new { Area = "ProgressReporting" });
            }

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                return RedirectToAction("GrantCertifyingOfficerAddressDetails", "GrantCertifyingOfficer", new { Area = "WorksPackageGrantCertifyingOfficer" });
            }

            return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region Provide grant certifying officer's company address

    [HttpGet(nameof(GrantCertifyingOfficerAddressDetails))]
    public async Task<IActionResult> GrantCertifyingOfficerAddressDetails()
    {
        var response = await _sender.Send(GetAddressDetailsRequest.Request);

        var model = _mapper.Map<PostCodeEntryViewModel>(response);

        model.ReturnUrl = string.Empty;

        var action = response.CertifyingOfficerResponse == ECertifyingOfficerResponse.No || response.CertifyingOfficerResponse == ECertifyingOfficerResponse.Update
            ? "GrantCertifyingOfficerDetails"
            : "ConfirmGrantCertifyingOfficer";
        ViewData["Backlink"] = Url.Action(action, "GrantCertifyingOfficer", new { Area = "WorksPackageGrantCertifyingOfficer" });

        return View(model);
    }

    /// <summary>
    /// When the user submits their address details for the manual entry details screen
    /// </summary>
    [HttpPost(nameof(GrantCertifyingOfficerAddressDetails))]
    public async Task<IActionResult> GrantCertifyingOfficerAddressDetails(PostCodeManualViewModel model, ESubmitAction submitAction)
    {
        var validator = new PostCodeManualViewModelValidator(false);
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View("GrantCertifyingOfficerAddressDetailsManual", model);
        }

        var request = _mapper.Map<SetAddressManualDetailsRequest>(model);
        await _sender.Send(request);

        if (submitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
        }

        return RedirectToAction("GrantCertifyingOfficerAuthorisedSignatories", "GrantCertifyingOfficer", new { Area = "WorksPackageGrantCertifyingOfficer" });
    }

    /// <summary>
    /// Called when the user enters a post code and hence this controller should ONLY receive
    /// a post code from the user. This takes us to either the manually entry screen or the list of results in a drop down.
    /// </summary>
    [HttpGet(nameof(GrantCertifyingOfficerAddressDetailsPostCodeItemEntered))]
    public async Task<IActionResult> GrantCertifyingOfficerAddressDetailsPostCodeItemEntered(string returnUrl, PostCodeEntryViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new PostCodeEntryViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        var isCorrectPersonResponse = await _sender.Send(GetIsCorrectPersonRequest.Request);
        viewModel.ApplicationReferenceNumber = isCorrectPersonResponse?.ApplicationReferenceNumber;
        viewModel.BuildingName = isCorrectPersonResponse?.BuildingName;
        ViewData["Backlink"] = isCorrectPersonResponse?.CertifyingOfficerResponse == ECertifyingOfficerResponse.No || isCorrectPersonResponse?.CertifyingOfficerResponse == ECertifyingOfficerResponse.Update
            ? Url.Action("GrantCertifyingOfficerDetails", "GrantCertifyingOfficer", new { Area = "WorksPackageGrantCertifyingOfficer" })
            : Url.Action("ConfirmGrantCertifyingOfficer", "GrantCertifyingOfficer", new { Area = "WorksPackageGrantCertifyingOfficer" });

        if (validationResult.IsValid)
        {
            if (submitAction == ESubmitAction.FindAddress)
            {
                var request = new GetPostCodeRequest { PostCode = viewModel.PostCode };
                var response = await _sender.Send(request);
                var newMappedModel = _mapper.Map<PostCodeSelectionViewModel>(response);
                newMappedModel.ApplicationReferenceNumber = viewModel.ApplicationReferenceNumber;
                newMappedModel.BuildingName = viewModel.BuildingName;

                if (!newMappedModel.HaveResults)
                {
                    var manualViewModel = _mapper.Map<PostCodeManualViewModel>(response);
                    manualViewModel.Postcode = viewModel.PostCode;
                    manualViewModel.ApplicationReferenceNumber = viewModel.ApplicationReferenceNumber;
                    manualViewModel.BuildingName = viewModel.BuildingName;

                    return View("GrantCertifyingOfficerAddressDetailsManual", manualViewModel);
                }
                return View("GrantCertifyingOfficerAddressDetailsResults", newMappedModel);
            }
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View("GrantCertifyingOfficerAddressDetails", viewModel);
    }

    /// <summary>
    /// Showed when the user selects a post code from the drop down
    /// </summary>
    [HttpPost(nameof(GrantCertifyingOfficerAddressDetailsPostCodeItemSelected))]
    public async Task<IActionResult> GrantCertifyingOfficerAddressDetailsPostCodeItemSelected(string returnUrl, PostCodeSelectionViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new PostCodeSelectionViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, String.Empty);
            // need to set these properties on the output model if there is an error
            return View("GrantCertifyingOfficerAddressDetailsResults", viewModel);
        }

        var request = _mapper.Map<SetAddressDetailsRequest>(viewModel);
        await _sender.Send(request);

        if (submitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
        }

        return RedirectToAction("GrantCertifyingOfficerAuthorisedSignatories", "GrantCertifyingOfficer", new { Area = "WorksPackageGrantCertifyingOfficer" });
    }

    /// <summary>
    /// Shows a company address entry manual entry screen
    /// </summary>
    [HttpGet(nameof(GrantCertifyingOfficerAddressDetailsManual))]
    public async Task<IActionResult> GrantCertifyingOfficerAddressDetailsManual(string returnUrl, string postCode)
    {
        var response = await _sender.Send(GetAddressDetailsRequest.Request);
        var model = _mapper.Map<PostCodeManualViewModel>(response);

        ViewData["Backlink"] = response.CertifyingOfficerResponse == ECertifyingOfficerResponse.No || response.CertifyingOfficerResponse == ECertifyingOfficerResponse.Update
            ? Url.Action("GrantCertifyingOfficerDetails", "GrantCertifyingOfficer", new { Area = "WorksPackageGrantCertifyingOfficer" })
            : Url.Action("ConfirmGrantCertifyingOfficer", "GrantCertifyingOfficer", new { Area = "WorksPackageGrantCertifyingOfficer" });

        return View(model);
    }

    #endregion

    #region Grant Certifying Officer Authorised Signatories

    [HttpGet(nameof(GrantCertifyingOfficerAuthorisedSignatories))]
    public async Task<IActionResult> GrantCertifyingOfficerAuthorisedSignatories()
    {
        var response = await _sender.Send(GetAuthorisedSignatoriesRequest.Request);
        var viewModel = _mapper.Map<AuthorisedSignatoriesViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(GrantCertifyingOfficerAuthorisedSignatories))]
    public async Task<IActionResult> GrantCertifyingOfficerAuthorisedSignatories(AuthorisedSignatoriesViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new AuthorisedSignatoriesViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetAuthorisedSignatoriesRequest>(viewModel);

            await _sender.Send(request);

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "ProgressReporting", new { Area = "ProgressReporting" });
            }

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                return RedirectToAction("CheckYourAnswers", "GrantCertifyingOfficer", new { Area = "WorksPackageGrantCertifyingOfficer" });
            }

            return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region Check Your Answers

    [HttpGet(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers()
    {
        var response = await _sender.Send(GetCheckYourAnswersRequest.Request);
        var viewModel = _mapper.Map<CheckYourAnswersViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers(CheckYourAnswersViewModel viewModel, ESubmitAction submitAction)
    {
        if (submitAction == ESubmitAction.Continue)
        {
            var useCaseRequest = SetCheckYourAnswersRequest.Request;
            await _sender.Send(useCaseRequest);

            return RedirectToAction("DutyOfCareDeed", "GrantCertifyingOfficer", new { Area = "WorksPackageGrantCertifyingOfficer" });
        }

        return View(viewModel);
    }

    #endregion

    #region ChangeAnswers

    [HttpGet(nameof(ChangeAnswers))]
    public async Task<IActionResult> ChangeAnswers()
    {
        var response = await _sender.Send(GetBaseInformationRequest.Request);
        var model = _mapper.Map<ChangeAnswersViewModel>(response);

        return View(model);
    }

    [HttpPost(nameof(ChangeAnswers))]
    public async Task<IActionResult> ChangeAnswers(ChangeAnswersViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (model.Proceed == false)
        {
            return RedirectToAction("CheckYourAnswers");
        }

        await _sender.Send(new ResetRequest(), cancellationToken);

        return RedirectToAction("SelectGrantCertifyingOfficer");
    }

    #endregion

    #region Duty of Care Deed

    [HttpGet(nameof(DutyOfCareDeed))]
    public async Task<IActionResult> DutyOfCareDeed()
    {
        var response = await _sender.Send(GetDutyOfCareDeedRequest.Request);
        var viewModel = _mapper.Map<DutyOfCareDeedViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(DutyOfCareDeed))]
    public IActionResult DutyOfCareDeed(DutyOfCareDeedViewModel viewModel, ESubmitAction submitAction)
    {
        return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
    }

    #endregion
}