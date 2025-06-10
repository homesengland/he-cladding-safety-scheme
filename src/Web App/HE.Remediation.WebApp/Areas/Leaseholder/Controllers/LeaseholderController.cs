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
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetResponsibleForCommunication;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.SetResponsibleForCommunication;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetResponsibleForCommunicationType;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.SetResponsibleForCommunicationType;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetCommunicationPartyDetails;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.SetCommunicationPartyDetails;
using FluentValidation;

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

        #region ResponsibleForCommunication

        [HttpGet(nameof(ResponsibleForCommunication))]
        public async Task<IActionResult> ResponsibleForCommunication()
        {
            var response = await _sender.Send(GetResponsibleForCommunicationRequest.Request);

            var model = _mapper.Map<LeaseHolderResponsibleForCommunicationViewModel>(response);

            return View(model);
        }

        [HttpPost(nameof(ResponsibleForCommunication))]
        public async Task<IActionResult> ResponsibleForCommunication(LeaseHolderResponsibleForCommunicationViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new LeaseHolderResponsibleForCommunicationViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View(viewModel);
            }

            var request = _mapper.Map<SetResponsibleForCommunicationRequest>(viewModel);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            return viewModel.ResponsibleForCommunication == ENoYes.No 
                ? RedirectToAction("ResponsibleForCommunicationType", "LeaseHolder", new { Area = "Leaseholder" }) 
                : RedirectToAction("CheckYourAnswers", "Leaseholder", new { Area = "Leaseholder" });
        }

        #endregion


        #region ResponsibleForCommunicationType

        [HttpGet(nameof(ResponsibleForCommunicationType))]
        public async Task<IActionResult> ResponsibleForCommunicationType()
        {
            var response = await _sender.Send(GetResponsibleForCommunicationTypeRequest.Request);
            var model = _mapper.Map<LeaseHolderResponsibleForCommunicationTypeViewModel>(response);
           
            return View(model);
        }

        [HttpPost(nameof(ResponsibleForCommunicationType))]
        public async Task<IActionResult> ResponsibleForCommunicationType(LeaseHolderResponsibleForCommunicationTypeViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new LeaseHolderResponsibleForCommunicationTypeViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View(viewModel);
            }

            var request = _mapper.Map<SetResponsibleForCommunicationTypeRequest>(viewModel);
            await _sender.Send(request);


            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            return viewModel.ResponsibleForCommunicationTypeId is EResponsibleForCommunicationType.ManagingAgent or EResponsibleForCommunicationType.Other 
                ? RedirectToAction("CommunicationPartyDetails", "LeaseHolder", new { Area = "Leaseholder" }) 
                : RedirectToAction("CheckYourAnswers", "Leaseholder", new { Area = "Leaseholder" });
        }

        #endregion


        #region CommunicationPartyDetails

        [HttpGet(nameof(CommunicationPartyDetails))]
        public async Task<IActionResult> CommunicationPartyDetails()
        {
            var response = await _sender.Send(GetCommunicationPartyDetailsRequest.Request);
            var model = _mapper.Map<LeaseHolderCommunicationPartyDetailsViewModel>(response);

            return View(model);
        }

        [HttpPost(nameof(CommunicationPartyDetails))]
        public async Task<IActionResult> CommunicationPartyDetails(LeaseHolderCommunicationPartyDetailsViewModel viewModel, ESubmitAction submitAction)
        {

            var validator = new LeaseHolderCommunicationPartyDetailsViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(viewModel);
            }

            var request = _mapper.Map<SetCommunicationPartyDetailsRequest>(viewModel);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            return SafeRedirectToAction("CheckYourAnswers", "LeaseHolder", new { Area = "Leaseholder" });
        }


        #endregion

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

            return useCaseRequest.Completed 
                ? RedirectToAction("ResponsibleForCommunication", "Leaseholder", new { Area = "Leaseholder" }) 
                : RedirectToAction("Evidence", "LeaseHolder", new { Area = "Leaseholder" });
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