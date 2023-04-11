using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.PreTenderSupport.ClaimPreTender.GetClaimPretenderSupport;
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

        protected override IActionResult DefaultStart => RedirectToAction("GrantFundingAgreement", "PreTenderSupport", new { Area = "PreTenderSupport" });

        #region "Grant Funding Agreement"
        [HttpGet(nameof(GrantFundingAgreement))]
        public IActionResult GrantFundingAgreement()
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

        public async Task<IActionResult> SupportRequired()
        {
            var response = await _sender.Send(GetSupportRequiredRequest.Request);

            var viewModel = _mapper.Map<SupportRequiredViewModel>(response);

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

                if (viewModel.SubmitAction == ESubmitAction.Continue)
                {
                    return RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
                }

                return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
            }

            validationResult.AddToModelState(ModelState, String.Empty);

            return View("SupportRequired", viewModel);
        }

        #endregion
    }
}