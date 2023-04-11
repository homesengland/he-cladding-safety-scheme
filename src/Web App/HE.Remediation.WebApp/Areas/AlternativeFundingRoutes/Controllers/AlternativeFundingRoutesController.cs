﻿using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.SetFundingStillPursuing;
using HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.SetPursuedSourcesFunding;
using HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.CheckYourAnswers.GetCheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.CheckYourAnswers.SetCheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.DeveloperPledgeStop.SetDeveloperPledgeStop;
using HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.GetFundingStillPursuing;
using HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.GetPursuedSourcesFunding;
using HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using HE.Remediation.WebApp.Authorisation;

namespace HE.Remediation.WebApp.Areas.AlternativeFundingRoutes.Controllers
{
    [Area("AlternativeFundingRoutes")]
    [Route("AlternativeFundingRoutes")]
    [CookieApplicationAuthorise]
    public class AlternativeFundingRoutesController : Controller
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public AlternativeFundingRoutesController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        #region Information
        [HttpGet(nameof(Information))]
        public IActionResult Information()
        {
            return View();
        }
        #endregion

        #region PursuedSourcesFunding
        [HttpGet(nameof(PursuedSourcesFunding))]
        public async Task<IActionResult> PursuedSourcesFunding()
        {
            var request = GetPursuedSourcesFundingRequest.Request;
            var response = await _sender.Send(request);
            var viewModel = _mapper.Map<PursuedSourcesFundingViewModel>(response);
            return View(viewModel);
        }
        #endregion

        #region SetPursuedSourcesFunding
        [HttpPost(nameof(SetPursuedSourcesFunding))]
        public async Task<IActionResult> SetPursuedSourcesFunding(PursuedSourcesFundingViewModel viewModel, ESubmitAction button)
        {
            var validator = new PursuedSourcesFundingViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View("PursuedSourcesFunding", viewModel);
            }

            var request = _mapper.Map<SetPursuedSourcesFundingRequest>(viewModel);
            await _sender.Send(request);

            if (request.PursuedSourcesFunding is EPursuedSourcesFundingType.ExhaustedAllRoutes
                or EPursuedSourcesFundingType.NotExhaustedAllRoutes)
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });

            return button == ESubmitAction.Continue ? RedirectToAction("FundingStillPursuing", "AlternativeFundingRoutes", new { Area = "AlternativeFundingRoutes" }) : RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }
        #endregion

        #region FundingStillPursuing
        [HttpGet(nameof(FundingStillPursuing))]
        public async Task<IActionResult> FundingStillPursuing()
        {
            var request = GetFundingStillPursuingRequest.Request;
            var response = await _sender.Send(request);
            var viewModel = _mapper.Map<FundingStillPursuingViewModel>(response);
            return View(viewModel);
        }
        #endregion

        #region SetFundingStillPursuing
        [HttpPost(nameof(SetFundingStillPursuing))]
        public async Task<IActionResult> SetFundingStillPursuing(FundingStillPursuingViewModel viewModel, ESubmitAction button)
        {
            var validator = new FundingStillPursuingViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View("FundingStillPursuing", viewModel);
            }

            var request = _mapper.Map<SetFundingStillPursuingRequest>(viewModel);
            await _sender.Send(request);

            var hasDeveloperPledgeAnswer =
                viewModel.FundingStillPursuing != null && viewModel.FundingStillPursuing.Any(x => x == EFundingStillPursuing.SignedUpDevelopersPledge);

            return button switch
            {
                ESubmitAction.Continue => hasDeveloperPledgeAnswer ? RedirectToAction("DeveloperPledgeStop", "AlternativeFundingRoutes",
                    new { Area = "AlternativeFundingRoutes" }) : RedirectToAction("CheckYourAnswers", "AlternativeFundingRoutes",
                    new { Area = "AlternativeFundingRoutes" }),
                ESubmitAction.Exit => RedirectToAction("Index", "TaskList", new { Area = "Application" }),
                _ => throw new ArgumentOutOfRangeException(nameof(button), button, null)
            };
        }
        #endregion

        #region CheckYourAnswers
        [HttpGet(nameof(CheckYourAnswers))]
        public async Task<IActionResult> CheckYourAnswers()
        {
            var request = GetCheckYourAnswersRequest.Request;
            var response = await _sender.Send(request);
            var viewModel = _mapper.Map<CheckYourAnswersViewModel>(response);

            return View(viewModel);
        }
        #endregion

        #region SetCheckYourAnswers
        [HttpPost(nameof(SetCheckYourAnswers))]
        public async Task<IActionResult> SetCheckYourAnswers()
        {
            var request = SetCheckYourAnswersRequest.Request;
            await _sender.Send(request);

            return RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }
        #endregion

        #region DeveloperPledgeStop
        [HttpGet(nameof(DeveloperPledgeStop))]
        public IActionResult DeveloperPledgeStop()
        {
            return View();
        }
        #endregion

        #region SetDeveloperPledgeStop
        [HttpPost(nameof(SetDeveloperPledgeStop))]
        public async Task<IActionResult> SetDeveloperPledgeStop()
        {
            var request = SetDeveloperPledgeStopRequest.Request;
            await _sender.Send(request);

            return RedirectToAction("Index", "Dashboard", new { Area = "Application" }); ;
        }
        #endregion
    }
}
