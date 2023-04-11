using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetAccountGrantPaidTo;
using HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetBankAccountDetailsRepresentative;
using HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetBankAccountDetailsResponsibleEntity;
using HE.Remediation.Core.UseCase.Areas.BankAccount.Details.SetAccountGrantPaidTo;
using HE.Remediation.Core.UseCase.Areas.BankAccount.Details.SetBankAccountDetailsRepresentative;
using HE.Remediation.Core.UseCase.Areas.BankAccount.Details.SetBankAccountDetailsResponsibleEntity;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.Representative.GetRepresentativeType;
using HE.Remediation.WebApp.Authorisation;
using HE.Remediation.WebApp.ViewModels.BankAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.BankAccount.Controllers
{
    [Area("BankAccount")]
    [Route("BankAccount")]
    [CookieApplicationAuthorise]
    public class BankAccountController : Controller
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public BankAccountController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        #region "What You'll Need"

        [HttpGet(nameof(WhatYouWillNeed))]
        public async Task<IActionResult> WhatYouWillNeed()
        {
            var response = await _sender.Send(GetRepresentativeTypeRequest.Request);

            if (response.RepresentativeType == EApplicationRepresentationType.Representative)
            {
                return View("WhatYouWillNeedRepresentative");
            }
            
            return View("WhatYouWillNeedResponsibleEntity");
        }

        [HttpGet(nameof(WhatYouWillNeedRepresentative))]
        public IActionResult WhatYouWillNeedRepresentative()
        {
            return View("WhatYouWillNeedRepresentative");
        }

        #endregion

        #region "Bank Account Details Responsible Entity"

        [HttpGet(nameof(BankAccountDetailsResponsibleEntity))]

        public async Task<IActionResult> BankAccountDetailsResponsibleEntity()
        {
            var response = await _sender.Send(GetBankAccountDetailsResponsibleEntityRequest.Request);

            var viewModel = _mapper.Map<BankAccountDetailsViewModel>(response);

            return View(viewModel);
        }

        [HttpPost(nameof(BankAccountDetailsResponsibleEntity))]
        public async Task<IActionResult> BankAccountDetailsResponsibleEntity(BankAccountDetailsViewModel viewModel)
        {
            var validator = new BankAccountDetailsViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (validationResult.IsValid)
            {
                var request = _mapper.Map<SetBankAccountDetailsResponsibleEntityRequest>(viewModel);

                await _sender.Send(request);

                return RedirectToAction("Index", "TaskList", new { area = "Application" });
            }

            validationResult.AddToModelState(ModelState, String.Empty);

            return View("BankAccountDetailsResponsibleEntity", viewModel);
        }

        #endregion

        #region "Bank Account Details Representative"

        [HttpGet(nameof(BankAccountDetailsRepresentative))]

        public async Task<IActionResult> BankAccountDetailsRepresentative()
        {
            var response = await _sender.Send(GetBankAccountDetailsRepresentativeRequest.Request);

            var viewModel = _mapper.Map<BankAccountDetailsViewModel>(response);

            return View(viewModel);
        }

        [HttpPost(nameof(BankAccountDetailsRepresentative))]
        public async Task<IActionResult> BankAccountDetailsRepresentative(BankAccountDetailsViewModel viewModel)
        {
            var validator = new BankAccountDetailsViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (validationResult.IsValid)
            {
                var request = _mapper.Map<SetBankAccountDetailsRepresentativeRequest>(viewModel);

                await _sender.Send(request);

                return RedirectToAction("Index", "TaskList", new { area = "Application" });
            }

            validationResult.AddToModelState(ModelState, String.Empty);

            return View("BankAccountDetailsRepresentative", viewModel);
        }

        #endregion

        #region "Account Grant Paid To"

        [HttpGet(nameof(AccountGrantPaidTo))]

        public async Task<IActionResult> AccountGrantPaidTo()
        {
            var response = await _sender.Send(GetAccountGrantPaidToRequest.Request);

            var viewModel = _mapper.Map<AccountGrantPaidToViewModel>(response);

            return View(viewModel);
        }

        [HttpPost(nameof(SetAccountGrantPaidTo))]
        public async Task<IActionResult> SetAccountGrantPaidTo(AccountGrantPaidToViewModel viewModel)
        {
            var validator = new AccountGrantPaidToViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (validationResult.IsValid)
            {
                var request = _mapper.Map<SetAccountGrantPaidToRequest>(viewModel);

                await _sender.Send(request);

                if (viewModel.SubmitAction == ESubmitAction.Continue)
                {
                    var action = viewModel.BankDetailsRelationship == EBankDetailsRelationship.MyAccount
                        ? nameof(BankAccountDetailsRepresentative)
                        : nameof(BankAccountDetailsResponsibleEntity);

                    return RedirectToAction(action, "BankAccount", new { Area = "BankAccount" });
                }

                return RedirectToAction("Index", "TaskList", new { area = "Application" });
            }

            validationResult.AddToModelState(ModelState, String.Empty);

            return View("AccountGrantPaidTo", viewModel);
        }

        #endregion
    }
}