using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.UseCase.Areas.PreTenderSupport.CheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.PreTenderSupport.ClaimPreTender.GetClaimPretenderSupport;
using HE.Remediation.Core.UseCase.Areas.PreTenderSupport.ConfirmContactDetails.GetConfirmContactDetails;
using HE.Remediation.Core.UseCase.Areas.PreTenderSupport.ConfirmContactDetails.SetConfirmContactDetails;
using HE.Remediation.Core.UseCase.Areas.PreTenderSupport.EmailContactDetails;
using HE.Remediation.Core.UseCase.Areas.PreTenderSupport.Submit.GetSubmit;
using HE.Remediation.Core.UseCase.Areas.PreTenderSupport.SupportRequired.GetSupportRequired;
using HE.Remediation.Core.UseCase.Areas.PreTenderSupport.SupportRequired.SetSupportRequired;
using HE.Remediation.WebApp.ViewModels.PreTenderSupport;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.PreTenderSupport.Controllers
{
    [Area("PreTenderSupport")]
    [Route("PreTenderSupport")]
    public class PreTenderSupportController : StartController
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public PreTenderSupportController(ISender sender, IMapper mapper)
            : base(sender)
        {
            _sender = sender;
            _mapper = mapper;
        }

        protected override IActionResult DefaultStart => RedirectToAction("ClaimPreTenderSupport", "PreTenderSupport", new { Area = "PreTenderSupport" });

        #region "Grant Funding Agreement"
        [HttpGet(nameof(GrantFundingAgreement))]
        public IActionResult GrantFundingAgreement()
        {
            return View();
        }

    #endregion

    #region "Deed of trust guidance"

    // holding page
    [HttpGet(nameof(DeedOfTrustGuidance))]
    public IActionResult DeedOfTrustGuidance()
    {
        return View();
    }

    #endregion

    #region "Confirm Contact Details"

    [HttpGet(nameof(ConfirmContactDetails))]
    public async Task<IActionResult> ConfirmContactDetails()
    {
        var response = await _sender.Send(GetConfirmContactDetailsRequest.Request);
        var viewModel = _mapper.Map<ConfirmContactDetailsViewModel>(response);

        return View(viewModel);
    }

        #endregion

        #region "Edit Contact Email"

        [HttpGet("EditContactEmail/{Id?}")]
    public async Task<IActionResult> EditContactEmail(string returnUrl, Guid? Id)
    {
        var request = new GetEmailContactDetailsRequest
        {
            Id = Id
        };

        var response = await _sender.Send(request);
        var viewModel = _mapper.Map<EditContactDetailsViewModel>(response);
        viewModel.ReturnUrl = returnUrl;

        return View(viewModel);
    }

        
    [HttpPost("EditContactEmail/{Id?}")]
    public async Task<IActionResult> EditContactEmail(EditContactDetailsViewModel viewModel, ESubmitAction submitAction)           
    {
        var validator = new EditContactDetailsViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
                var request = _mapper.Map<SetEmailContactDetailsRequest>(viewModel);

                await _sender.Send(request);

                if (viewModel.ReturnUrl is not null)
                {
                    return SafeRedirectToAction(viewModel.ReturnUrl, "PreTenderSupport", new { Area = "PreTenderSupport" });
                }

                if (submitAction == ESubmitAction.Continue)
                {
                    return RedirectToAction("ConfirmContactDetails", "PreTenderSupport", new { Area = "PreTenderSupport" });
                }

                return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);
        return View("EditContactEmail", viewModel);
    }

    #endregion

    #region "PTFS Decision"

    [HttpGet(nameof(PTFSDecision))]
    public IActionResult PTFSDecision()
    {
        return View();
    }

    #endregion

        #region "Claim Pre-Tender Support"
        [HttpGet(nameof(ClaimPreTenderSupport))]
        public async Task<IActionResult> ClaimPreTenderSupport()
        {
            var response = await _sender.Send(GetClaimPretenderSupportRequest.Request);
            var model = _mapper.Map<ClaimPreTenderSupportViewModel>(response);
            return View(model);
        }
        #endregion

        #region "Support Required"

        [HttpGet(nameof(SupportRequired))]

        public async Task<IActionResult> SupportRequired(string returnUrl)
        {
            var response = await _sender.Send(GetSupportRequiredRequest.Request);

            var viewModel = _mapper.Map<SupportRequiredViewModel>(response);
            viewModel.ReturnUrl = returnUrl;

            return View(viewModel);
        }

        [HttpPost(nameof(SetSupportRequired))]
        public async Task<IActionResult> SetSupportRequired(SupportRequiredViewModel viewModel)
        {
            var validator = new SupportRequiredViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (validationResult.IsValid)
            {
                var request = _mapper.Map<SetSupportRequiredRequest>(viewModel);

                await _sender.Send(request);

                if (viewModel.ReturnUrl is not null)
                {
                    return SafeRedirectToAction(viewModel.ReturnUrl, "PreTenderSupport", new { Area = "PreTenderSupport" });
                }
                                           
                if (viewModel.SubmitAction == ESubmitAction.Continue)
                {
                    return RedirectToAction("GrantFundingAgreement", "PreTenderSupport", new { Area = "PreTenderSupport" });
                }

                return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
            }

            validationResult.AddToModelState(ModelState, String.Empty);

            return View("SupportRequired", viewModel);
        }

        #endregion

        #region "Submitted"        

        [HttpPost(nameof(Submitted))]
        public async Task<IActionResult> Submitted()
        {
            var confirmDetailsResponse = await _sender.Send(GetSubmitRequest.Request);
            var viewModel = _mapper.Map<SubmittedViewModel>(confirmDetailsResponse);
            return View(viewModel);

        }

        #endregion

        #region Check Your Answers
        [HttpGet(nameof(CheckYourAnswers))]
        public async Task<IActionResult> CheckYourAnswers()
        {
            var response = await _sender.Send(GetCheckYourAnswersRequest.Request);

            return View(_mapper.Map<CheckYourAnswersViewModel>(response));
        }

        #endregion
    }
}