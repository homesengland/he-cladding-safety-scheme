using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.GetReasonForClosing;
using HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.GetFinalCheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.SetReasonForClosing;
using HE.Remediation.WebApp.ViewModels.WithdrawalRequest;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.GetSubmitted;
using HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.SetFinalCheckYourAnswers;

namespace HE.Remediation.WebApp.Areas.WithdrawalRequest.Controllers
{
    [Area("WithdrawalRequest")]
    [Route("WithdrawApplication")]
    public class RequestToWithdrawApplicationController : StartController
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public RequestToWithdrawApplicationController(ISender sender, IMapper mapper) : base(sender)
        {
            _sender = sender;
            _mapper = mapper;
        }


        protected override IActionResult DefaultStart => RedirectToAction("ReasonForClosing", "RequestToWithdrawApplication", new { Area = "WithdrawalRequest" });

        #region Reason For Closing

        [HttpGet(nameof(ReasonForClosing))]
        public async Task<IActionResult> ReasonForClosing()
        {
            var response = await _sender.Send(GetReasonForClosingRequest.Request);
            var viewModel = _mapper.Map<ReasonForClosingViewModel>(response);
            return View(viewModel);
        }

        [HttpPost(nameof(ReasonForClosing))]
        public async Task<IActionResult> ReasonForClosing(ReasonForClosingViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new ReasonForClosingModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(viewModel);
            }


            var request = _mapper.Map<SetReasonForClosingRequest>(viewModel);
            await _sender.Send(request);

            return submitAction == ESubmitAction.Continue
                ? RedirectToAction("FinalCheckYourAnswers", "RequestToWithdrawApplication",
                    new
                    {
                        Area = "WithdrawalRequest",
                    })
                : RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
        }
        #endregion

        #region Check Your Answers

        [HttpGet(nameof(FinalCheckYourAnswers))]
        public async Task<IActionResult> FinalCheckYourAnswers()
        {
            var response = await _sender.Send(GetFinalCheckYourAnswersRequest.Request);
            var model = _mapper.Map<FinalCheckYourAnswersViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(FinalCheckYourAnswersSubmit))]
        public async Task<IActionResult> FinalCheckYourAnswersSubmit()
        {
            var response = await _sender.Send(SetWithdrawalRequestFinalCheckYourAnswersRequest.Request);

            return RedirectToAction("Submitted", "RequestToWithdrawApplication", new { Area = "WithdrawalRequest" });
        }

        #endregion

        #region Submitted

        [HttpGet(nameof(Submitted))]
        public async Task<IActionResult> Submitted()
        {
            var response = await _sender.Send(GetSubmittedRequest.Request);
            var viewModel = _mapper.Map<SubmittedViewModel>(response);
            return View(viewModel);
        }

        #endregion
    }
}
