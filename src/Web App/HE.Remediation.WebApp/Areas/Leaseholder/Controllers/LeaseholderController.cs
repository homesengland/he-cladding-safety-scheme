using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using File = HE.Remediation.WebApp.ViewModels.Shared.File;
using HE.Remediation.WebApp.Constants;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetLeaseHolderEvidence;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.DeleteLeaseHolderEvidence;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetCheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.SetLeaseholderEvidence;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.SetCheckYourAnswers;
using HE.Remediation.WebApp.ViewModels.Leaseholder;

namespace HE.Remediation.WebApp.Areas.Leaseholder.Controllers
{
    [Area("Leaseholder")]
    [Route("Leaseholder")]
    public class LeaseholderController : StartController
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public LeaseholderController(ISender sender, IMapper mapper)
        : base(sender)
        {
            _sender = sender;
            _mapper = mapper;
        }

        protected override IActionResult DefaultStart => RedirectToAction("WhatYoullNeed", "Leaseholder", new { Area = "Leaseholder" });

        [HttpGet]
        public IActionResult WhatYoullNeed()
        {
            return View();
        }

        [HttpGet(nameof(Evidence))]
        public async Task<IActionResult> Evidence()
        {
            var leaseHolderEvidence = await _sender.Send(GetLeaseHolderEvidenceRequest.Request);

            var files = _mapper.Map<List<File>>(leaseHolderEvidence);

            var model = new LeaseHolderEvidenceViewModel
            {
                AddedFiles = files,
            };

            return View(model);
        }

        [HttpPost(nameof(Evidence))]
        [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
        public async Task<IActionResult> Evidence(LeaseHolderEvidenceViewModel request)
        {
            var validator = new LeaseHolderEvidenceViewModelValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(request);
            }


            var useCaseRequest = _mapper.Map<SetLeaseHolderEvidenceRequest>(request);

            try
            {
                await _sender.Send(useCaseRequest);
            }
            catch (InvalidFileException ex)
            {
                ModelState.AddModelError(nameof(request.File), ex.Message);
                return View(request);
            }

            return useCaseRequest.Completed ? RedirectToAction("CheckYourAnswers", "Leaseholder", new { Area = "Leaseholder" }) :
                RedirectToAction("Evidence", "LeaseHolder", new { Area = "Leaseholder" });
        }

        [HttpGet(nameof(Evidence) + "/Delete")]
        public async Task<IActionResult> EvidenceDelete([FromQuery] DeleteLeaseHolderEvidenceRequest request)
        {
            await _sender.Send(request);

            return RedirectToAction("Evidence", "Leaseholder", new { Area = "Leaseholder" });
        }

        #region Check Your Answers
        [HttpGet(nameof(CheckYourAnswers))]
        public async Task<IActionResult> CheckYourAnswers()
        {
            var response = await _sender.Send(GetCheckYourAnswersRequest.Request);

            return View(_mapper.Map<CheckYourAnswersViewModel>(response));
        }

        [HttpPost(nameof(CheckYourAnswers))]
        public async Task<IActionResult> CheckYourAnswers(CheckYourAnswersViewModel viewModel)
        {
            await _sender.Send(SetCheckYourAnswersRequest.Request);
            return RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }
        #endregion
    }
}