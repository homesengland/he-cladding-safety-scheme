using AutoMapper;
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
using Mediator;
using Microsoft.AspNetCore.Mvc;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes;

namespace HE.Remediation.WebApp.Areas.AlternativeFundingRoutes.Controllers
{
    [Area("AlternativeFundingRoutes")]
    [Route("AlternativeFundingRoutes")]
    public class AlternativeFundingRoutesController : StartController
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public AlternativeFundingRoutesController(ISender sender, IMapper mapper, IApplicationDataProvider applicationDataProvider)
            : base(sender)
        {
            _sender = sender;
            _mapper = mapper;
            _applicationDataProvider = applicationDataProvider;
        }

        protected override IActionResult DefaultStart => RedirectToAction("Information", "AlternativeFundingRoutes", new { Area = "AlternativeFundingRoutes" });

        private IActionResult ExitAction => RedirectToAction("Index", "TaskList", new { Area = "Application" });

        #region Information
        [HttpGet(nameof(Information))]
        public async Task<IActionResult> Information(CancellationToken cancellationToken)
        {
            var response = await _sender.Send(GetAlternateFundingInformationRequest.Request, cancellationToken);
            var model = _mapper.Map<InformationViewModel>(response);
            return View(model);
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
        public async Task<IActionResult> SetPursuedSourcesFunding(PursuedSourcesFundingViewModel viewModel)
        {
            var validator = new PursuedSourcesFundingViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View("PursuedSourcesFunding", viewModel);
            }

            var request = _mapper.Map<SetPursuedSourcesFundingRequest>(viewModel);
            var response = await _sender.Send(request);

            if (viewModel.SubmitAction == ESubmitAction.Exit)
            {
                return ExitAction;
            }

            if (response.IsSocialSector)
            {
                if (response.VisitedCheckYourAnswers)
                {
                    return RedirectToAction("CheckYourAnswers", "AlternativeFundingRoutes", new { Area = "AlternativeFundingRoutes" });
                }

                if (request.PursuedSourcesFunding != EPursuedSourcesFundingType.ExhaustedAllRoutes)
                {
                    return RedirectToAction("CostRecovery", "AlternativeFundingRoutes", new { Area = "AlternativeFundingRoutes" });
                }
            }

            return request.PursuedSourcesFunding is EPursuedSourcesFundingType.PursuingOtherRoutes
                ? RedirectToAction("FundingStillPursuing", "AlternativeFundingRoutes", new { Area = "AlternativeFundingRoutes" })
                : RedirectToAction("CheckYourAnswers", "AlternativeFundingRoutes", new { Area = "AlternativeFundingRoutes" });
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

            var isCladdingSafetyScheme = _applicationDataProvider.GetApplicationScheme() == EApplicationScheme.CladdingSafetyScheme;

            var isStop = hasDeveloperPledgeAnswer && isCladdingSafetyScheme;

            if (button == ESubmitAction.Exit)
            {
                return ExitAction;
            }

            return isStop
                ? RedirectToAction("DeveloperPledgeStop", "AlternativeFundingRoutes", new { Area = "AlternativeFundingRoutes" })
                : RedirectToAction("CheckYourAnswers", "AlternativeFundingRoutes", new { Area = "AlternativeFundingRoutes" });

        }
        #endregion

        #region CheckYourAnswers
        [HttpGet(nameof(CheckYourAnswers))]
        public async Task<IActionResult> CheckYourAnswers()
        {
            var response = await _sender.Send(GetCheckYourAnswersRequest.Request);
            var viewModel = _mapper.Map<CheckYourAnswersViewModel>(response);
            viewModel.ApplicationScheme = _applicationDataProvider.GetApplicationScheme();
            return View(viewModel);
        }
        #endregion

        #region SetCheckYourAnswers
        [HttpPost(nameof(SetCheckYourAnswers))]
        public async Task<IActionResult> SetCheckYourAnswers(CheckYourAnswersViewModel model, CancellationToken cancellationToken)
        {
            var validator = new CheckYourAnswersViewModelValidator();
            var validationResult = await validator.ValidateAsync(model, cancellationToken);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View("CheckYourAnswers", model);
            }

            var request = SetCheckYourAnswersRequest.Request;
            await _sender.Send(request, cancellationToken);

            return ExitAction;
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

        #region CostRecovery

        [HttpGet(nameof(CostRecovery))]
        public async Task<IActionResult> CostRecovery(CancellationToken cancellationToken)
        {
            var response = await _sender.Send(GetCostRecoveryRequest.Request, cancellationToken);
            var model = _mapper.Map<CostRecoveryViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(CostRecovery))]
        public async Task<IActionResult> CostRecovery(CostRecoveryViewModel model, CancellationToken cancellationToken)
        {
            var validator = new CostRecoveryViewModelValidator();
            var validationResult = await validator.ValidateAsync(model, cancellationToken);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetCostRecoveryRequest>(model);
            await _sender.Send(request, cancellationToken);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return ExitAction;
            }

            return model.VisitedCheckYourAnswers
                ? RedirectToAction("CheckYourAnswers", "AlternativeFundingRoutes", new { Area = "AlternativeFundingRoutes" })
                : RedirectToAction("RoleForRemediationContribution", "AlternativeFundingRoutes", new { Area = "AlternativeFundingRoutes" });
        }

        #endregion

        #region RoleForRemediationContribution

        [HttpGet(nameof(RoleForRemediationContribution))]
        public async Task<IActionResult> RoleForRemediationContribution(CancellationToken cancellationToken)
        {
            var response = await _sender.Send(GetRoleForRemediationContributionRequest.Request, cancellationToken);
            var model = _mapper.Map<RoleForRemediationContributionViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(RoleForRemediationContribution))]
        public async Task<IActionResult> RoleForRemediationContribution(RoleForRemediationContributionViewModel model, CancellationToken cancellationToken)
        {
            var validator = new RoleForRemediationContributionViewModelValidator();
            var validationResult = await validator.ValidateAsync(model, cancellationToken);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetRoleForRemediationContributionRequest>(model);
            await _sender.Send(request, cancellationToken);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return ExitAction;
            }

            return model.Roles.Contains(EPartyPursuedRole.Other)
                ? RedirectToAction("OtherParties", "AlternativeFundingRoutes", new { Area = "AlternativeFundingRoutes" })
                : RedirectToAction("CheckYourAnswers", "AlternativeFundingRoutes", new { Area = "AlternativeFundingRoutes" });
        }

        #endregion

        #region OtherParties

        [HttpGet(nameof(OtherParties))]
        public async Task<IActionResult> OtherParties(CancellationToken cancellationToken)
        {
            var response = await _sender.Send(GetOtherPartiesRequest.Request, cancellationToken);
            var model = _mapper.Map<OtherPartiesViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(OtherParties))]
        public async Task<IActionResult> OtherParties(OtherPartiesViewModel model, CancellationToken cancellationToken)
        {
            var validator = new OtherPartiesViewModelValidator();
            var validationResult = await validator.ValidateAsync(model, cancellationToken);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetOtherPartiesRequest>(model);
            await _sender.Send(request, cancellationToken);

            return model.SubmitAction == ESubmitAction.Continue
                ? RedirectToAction("CheckYourAnswers", "AlternativeFundingRoutes", new { Area = "AlternativeFundingRoutes" })
                : ExitAction;
        }
        #endregion
    }
}
