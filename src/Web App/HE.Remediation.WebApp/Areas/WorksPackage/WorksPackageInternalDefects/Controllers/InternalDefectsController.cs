using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageInternalDefects.CheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageInternalDefects.InternalDefectsCost.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageInternalDefects.InternalDefectsCost.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageInternalDefects.StartInformation;
using HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageInternalDefects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Threading;

namespace HE.Remediation.WebApp.Areas.WorksPackage.WorksPackageInternalDefects.Controllers
{
    [Area("WorksPackageInternalDefects")]
    [Route("WorksPackage/InternalDefects")]
    public class InternalDefectsController : StartController
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public InternalDefectsController(ISender sender, IMapper mapper) : base(sender)
        {
            _sender = sender;
            _mapper = mapper;
        }

        protected override IActionResult DefaultStart =>
            RedirectToAction("StartInformation", "InternalDefects", new { Area = "WorksPackageInternalDefects" });

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
        public IActionResult StartInformation(StartInformationViewModel viewModel,
            ESubmitAction submitAction)
        {
            return RedirectToAction("InternalDefectsCost", "InternalDefects", new { Area = "WorksPackageInternalDefects" });
        }

        #endregion

        #region Internal defects cost

        [HttpGet(nameof(InternalDefectsCost))]
        public async Task<IActionResult> InternalDefectsCost()
        {
            var response = await _sender.Send(GetInternalDefectsCostRequest.Request);
            var viewModel = _mapper.Map<InternalDefectsCostViewModel>(response);

            viewModel.ReturnUrl = string.Empty;
            return View(viewModel);
        }

        [HttpPost(nameof(InternalDefectsCost))]
        public async Task<IActionResult> InternalDefectsCost(InternalDefectsCostViewModel model,
            ESubmitAction submitAction, CancellationToken cancellationToken)
        {
            var validator = new InternalDefectsCostViewModelValidator();
            var validationResult = await validator.ValidateAsync(model, cancellationToken);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetInternalDefectsCostRequest>(model);
            await _sender.Send(request, cancellationToken);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
            }

          
             return RedirectToAction("CheckYourAnswers", "InternalDefects", new { Area = "WorksPackageInternalDefects" });
        }

        #endregion

        #region Check Your Answers

        [HttpGet(nameof(CheckYourAnswers))]
        public async Task<IActionResult> CheckYourAnswers(CancellationToken cancellationToken)
        {
            var response = await _sender.Send(GetInternalDefectsCheckYourAnswersRequest.Request, cancellationToken);
            var model = _mapper.Map<CheckYourAnswersViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(CheckYourAnswers))]
        public async Task<IActionResult> CheckYourAnswers(CheckYourAnswersViewModel model, CancellationToken cancellationToken)
        {
            if (!model.IsSubmitted)
            {
                await _sender.Send(SetCheckYourAnswersRequest.Request, cancellationToken);
            }

            return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
        }

        #endregion
    }
}
