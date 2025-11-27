using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.Attributes.Authorisation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using HE.Remediation.WebApp.Attributes.Routing;
using HE.Remediation.WebApp.ViewModels.Location;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam;

namespace HE.Remediation.WebApp.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectTeam.Controllers;
[Area("MonthlyProgressReportingProjectTeam")]
[Route("MonthlyProgressReporting/GrantCertifyingOfficer")]
[CookieApplicationAuthorise]
public class GrantCertifyingOfficerController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GrantCertifyingOfficerController(ISender sender, IMapper mapper)
        : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart => RedirectToAction("HasGrantCertifyingOfficer");

    [ExcludeRouteRecording]
    [HttpGet(nameof(SaveProjectTeam))]
    public async Task<IActionResult> SaveProjectTeam(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetDetailsForSaveProjectTeamRequest.Request, cancellationToken);

        if(response.HasZeroTeamMembers)
        {
            return RedirectToAction("NoTeam", "MonthlyProgressReportingProjectTeam", new { Area = "MonthlyProgressReportingProjectTeam" });
        }

        if (response.IsGcoComplete || response.HasTeamMembersButNoGcoRoles)
        {
            // complete Team Members
            await _sender.Send(new SetProjectTeamStatusRequest(ETaskStatus.Completed), cancellationToken);
            return RedirectToAction("TaskList", "MonthlyProgressReporting", new { Area = "MonthlyProgressReporting" });
        }

        // progress with GCO flow
        return RedirectToAction("Start");
    }

    [HttpGet(nameof(HasGrantCertifyingOfficer))]
    public async Task<IActionResult> HasGrantCertifyingOfficer(CancellationToken cancellationToken, string returnUrl = "")
    {
        var response = await _sender.Send(GetHasGrantCertifyingOfficerRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<HasGrantCertifyingOfficerViewModel>(response);
        viewModel.PreviousResponse = response.DoYouHaveAGrantCertifyingOfficer;
        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(HasGrantCertifyingOfficer))]
    public async Task<IActionResult> HasGrantCertifyingOfficer(HasGrantCertifyingOfficerViewModel viewModel, CancellationToken cancellationToken)
    {
        var validator = new HasGrantCertifyingOfficerViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetHasGrantCertifyingOfficerRequest>(viewModel);
        await _sender.Send(request, cancellationToken);

        if (viewModel.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("ProjectTeam", "MonthlyProgressReportingProjectTeam", new { Area = "MonthlyProgressReportingProjectTeam" });
        }

        if (viewModel.DoYouHaveAGrantCertifyingOfficer == true)
        {
            if (!string.IsNullOrEmpty(viewModel.ReturnUrl) && viewModel.PreviousResponse == viewModel.DoYouHaveAGrantCertifyingOfficer)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl);
            }

            return RedirectToAction("WhoIsTheGrantCertifyingOfficer");
        }

        return RedirectToAction("GrantCertifyingOfficerCheckYourAnswers");
    }

    [HttpGet(nameof(WhoIsTheGrantCertifyingOfficer))]
    public async Task<IActionResult> WhoIsTheGrantCertifyingOfficer(CancellationToken cancellationToken, string returnUrl = "")
    {
        var response = await _sender.Send(GetWhoIsTheGrantCertifyingOfficerRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<WhoIsTheGrantCertifyingOfficerViewModel>(response);

        viewModel.PreviousProjectTeamMemberId = viewModel.ProjectTeamMemberId;
        viewModel.ReturnUrl = returnUrl;

        return View(viewModel);
    }

    [HttpPost(nameof(WhoIsTheGrantCertifyingOfficer))]
    public async Task<IActionResult> WhoIsTheGrantCertifyingOfficer(WhoIsTheGrantCertifyingOfficerViewModel viewModel, CancellationToken cancellationToken)
    {
        var validator = new WhoIsTheGrantCertifyingOfficerViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetWhoIsTheGrantCertifyingOfficerRequest>(viewModel);
        await _sender.Send(request, cancellationToken);

        if (viewModel.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("ProjectTeam", "MonthlyProgressReportingProjectTeam", new { Area = "MonthlyProgressReportingProjectTeam" });
        }

        if (!string.IsNullOrEmpty(viewModel.ReturnUrl) && viewModel.PreviousProjectTeamMemberId == viewModel.ProjectTeamMemberId)
        {
            return RedirectToAction("ConfirmGcoDetails", null, new { Area = "MonthlyProgressReportingProjectTeam", returnUrl = viewModel.ReturnUrl });
        }

        return RedirectToAction("ConfirmGcoDetails");
    }

    [HttpGet(nameof(ConfirmGcoDetails))]
    public async Task<IActionResult> ConfirmGcoDetails(CancellationToken cancellationToken, string returnUrl = "")
    {
        var response = await _sender.Send(GetGcoDetailsRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<ConfirmGcoDetailsViewModel>(response);
        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(ConfirmGcoDetails))]
    public async Task<IActionResult> ConfirmGcoDetails(ConfirmGcoDetailsViewModel viewModel, CancellationToken cancellationToken)
    {
        var validator = new ConfirmGcoDetailsViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetGcoDetailsRequest>(viewModel);
        await _sender.Send(request, cancellationToken);

        if (viewModel.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("ProjectTeam", "MonthlyProgressReportingProjectTeam", new { Area = "MonthlyProgressReportingProjectTeam" });
        }

        switch (viewModel.CertifyingOfficerResponse)
        {
            case ECertifyingOfficerResponse.Yes:
                return string.IsNullOrEmpty(viewModel.ReturnUrl) ? RedirectToAction("GrantCertifyingOfficerAddress") : SafeRedirectToAction(viewModel.ReturnUrl);
            case ECertifyingOfficerResponse.Update:
                return RedirectToAction("GrantCertifyingOfficerDetails", null, new { Area = "MonthlyProgressReportingProjectTeam", returnUrl = viewModel.ReturnUrl });
            case ECertifyingOfficerResponse.No:
                return RedirectToAction("WhoIsTheGrantCertifyingOfficer");
            default:
                return RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
        }
    }

    [HttpGet(nameof(GrantCertifyingOfficerDetails))]
    public async Task<IActionResult> GrantCertifyingOfficerDetails(CancellationToken cancellationToken, string returnUrl = "")
    {
        var response = await _sender.Send(GetGcoDetailsRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<GrantCertifyingOfficerDetailsViewModel>(response);
        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(GrantCertifyingOfficerDetails))]
    public async Task<IActionResult> GrantCertifyingOfficerDetails(GrantCertifyingOfficerDetailsViewModel viewModel, CancellationToken cancellationToken)
    {
        var validator = new GrantCertifyingOfficerDetailsViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetGrantCertifyingOfficerDetailsRequest>(viewModel);
        await _sender.Send(request, cancellationToken);

        if (viewModel.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("ProjectTeam", "MonthlyProgressReportingProjectTeam", new { Area = "MonthlyProgressReportingProjectTeam" });
        }

        return RedirectToAction("GrantCertifyingOfficerCheckYourUpdatedGco", null, new { Area = "MonthlyProgressReportingProjectTeam", returnUrl = viewModel.ReturnUrl });
    }

    [HttpGet("GrantCertifyingOfficerDetails/Check")]
    public async Task<IActionResult> GrantCertifyingOfficerCheckYourUpdatedGco(CancellationToken cancellationToken, string returnUrl = "")
    {
        var response = await _sender.Send(new GetCheckYourUpdatedGcoRequest(), cancellationToken);
        var viewModel = _mapper.Map<GetCheckYourUpdatedGcoViewModel>(response);
        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpGet(nameof(GrantCertifyingOfficerAddress))]
    public async Task<IActionResult> GrantCertifyingOfficerAddress(CancellationToken cancellationToken, string returnUrl = "")
    {
        var response = await _sender.Send(GetGrantCertifyingOfficerAddressRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<PostCodeEntryViewModel>(response);
        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(GrantCertifyingOfficerAddress))]
    public async Task<IActionResult> GrantCertifyingOfficerAddress(PostCodeManualViewModel viewModel, ESubmitAction submitAction, CancellationToken cancellationToken)
    {
        var validator = new PostCodeManualViewModelValidator(false);
        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View("GrantCertifyingOfficerAddressManual", viewModel);
        }

        var request = _mapper.Map<SetGrantCertifyingOfficerAddressRequest>(viewModel);
        await _sender.Send(request, cancellationToken);

        if (viewModel.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("ProjectTeam", "MonthlyProgressReportingProjectTeam", new { Area = "MonthlyProgressReportingProjectTeam" });
        }

        if (!string.IsNullOrEmpty(viewModel.ReturnUrl))
        {
            return SafeRedirectToAction(viewModel.ReturnUrl);
        }

        return RedirectToAction("GrantCertifyingOfficerAuthorisedSignatories");
    }

    [HttpGet(nameof(GrantCertifyingOfficerAddressManual))]
    public async Task<IActionResult> GrantCertifyingOfficerAddressManual(CancellationToken cancellationToken, string returnUrl = "")
    {
        var response = await _sender.Send(GetGrantCertifyingOfficerAddressRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<PostCodeManualViewModel>(response);
        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [ExcludeRouteRecording]
    [HttpGet(nameof(GrantCertifyingOfficerAddressPostCodeItemEntered))]
    public async Task<IActionResult> GrantCertifyingOfficerAddressPostCodeItemEntered(
        PostCodeEntryViewModel viewModel,
        ESubmitAction submitAction,
        CancellationToken cancellationToken)
    {
        var validator = new PostCodeEntryViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, String.Empty);

            return View("GrantCertifyingOfficerAddress", viewModel);
        }

        if (submitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("ProjectTeam", "MonthlyProgressReportingProjectTeam", new { Area = "MonthlyProgressReportingProjectTeam" });
        }

        if (submitAction == ESubmitAction.FindAddress)
        {
            var request = new GetPostCodeRequest { PostCode = viewModel.PostCode };
            var response = await _sender.Send(request, cancellationToken);
            var newMappedModel = _mapper.Map<PostCodeSelectionViewModel>(response);
            newMappedModel.ApplicationReferenceNumber = viewModel.ApplicationReferenceNumber;
            newMappedModel.BuildingName = viewModel.BuildingName;
            newMappedModel.ProgressReportVersion = viewModel.ProgressReportVersion;
            newMappedModel.IsProgressReportGcoComplete = viewModel.IsProgressReportGcoComplete;
            newMappedModel.ReturnUrl = viewModel.ReturnUrl;
            if (!newMappedModel.HaveResults)
            {
                var manualViewModel = _mapper.Map<PostCodeManualViewModel>(response);
                manualViewModel.Postcode = viewModel.PostCode;
                manualViewModel.ApplicationReferenceNumber = viewModel.ApplicationReferenceNumber;
                manualViewModel.BuildingName = viewModel.BuildingName;
                manualViewModel.ProgressReportVersion = viewModel.ProgressReportVersion;
                manualViewModel.IsProgressReportGcoComplete = viewModel.IsProgressReportGcoComplete;
                manualViewModel.ReturnUrl = viewModel.ReturnUrl;

                return View("GrantCertifyingOfficerAddressManual", manualViewModel);
            }
            return View("GrantCertifyingOfficerAddressResults", newMappedModel);
        }

        return RedirectToAction("ProjectTeam", "MonthlyProgressReportingProjectTeam", new { Area = "MonthlyProgressReportingProjectTeam" });
    }

    [HttpPost(nameof(GrantCertifyingOfficerAddressPostCodeItemSelected))]
    public async Task<IActionResult> GrantCertifyingOfficerAddressPostCodeItemSelected(PostCodeSelectionViewModel viewModel, ESubmitAction submitAction, CancellationToken cancellationToken)
    {
        var validator = new PostCodeSelectionViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            // need to set these properties on the output model if there is an error
            return View("GrantCertifyingOfficerAddressResults", viewModel);
        }

        var request = _mapper.Map<SetGrantCertifyingOfficerAddressResultRequest>(viewModel);
        await _sender.Send(request, cancellationToken);

        if (viewModel.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("ProjectTeam", "MonthlyProgressReportingProjectTeam", new { Area = "MonthlyProgressReportingProjectTeam" });
        }

        if (!string.IsNullOrEmpty(viewModel.ReturnUrl))
        {
            return SafeRedirectToAction(viewModel.ReturnUrl);
        }

        return RedirectToAction("GrantCertifyingOfficerAuthorisedSignatories");
    }

    [HttpGet(nameof(GrantCertifyingOfficerAuthorisedSignatories))]
    public async Task<IActionResult> GrantCertifyingOfficerAuthorisedSignatories(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetGrantCertifyingOfficerSignatoryRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<GrantCertifyingOfficerSignatoriesViewModel>(response);
        return View(viewModel);
    }

    [HttpPost(nameof(GrantCertifyingOfficerAuthorisedSignatories))]
    public async Task<IActionResult> GrantCertifyingOfficerAuthorisedSignatories(GrantCertifyingOfficerSignatoriesViewModel viewModel, CancellationToken cancellationToken)
    {
        var validator = new GrantCertifyingOfficerSignatoriesViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetGrantCertifyingOfficerSignatoryRequest>(viewModel);
        await _sender.Send(request, cancellationToken);

        if (viewModel.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("ProjectTeam", "MonthlyProgressReportingProjectTeam", new { Area = "MonthlyProgressReportingProjectTeam" });
        }

        return RedirectToAction("GrantCertifyingOfficerCheckYourAnswers");
    }

    [HttpGet("GrantCertifyingOfficerDetails/CheckYourAnswers")]
    public async Task<IActionResult> GrantCertifyingOfficerCheckYourAnswers(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(new GetGrantCertifyingOfficerCheckYourAnswersRequest(), cancellationToken);
        var viewModel = _mapper.Map<GrantCertifyingOfficerCheckYourAnswersViewModel>(response);
        return View(viewModel);
    }

    [ExcludeRouteRecording]
    [HttpGet("GrantCertifyingOfficerDetails/Confirm")]
    public async Task<IActionResult> GrantCertifyingOfficerConfirmAnswers(CancellationToken cancellationToken)
    {
        await _sender.Send(new SetConfirmGrantCertifyingOfficerRequest(), cancellationToken);
        return RedirectToAction("ProjectTeam", "MonthlyProgressReportingProjectTeam", new { Area = "MonthlyProgressReportingProjectTeam" });
    }
}
