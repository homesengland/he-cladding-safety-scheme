using AutoMapper;
using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCladdingSystem;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.AspNetCore;
using HE.Remediation.WebApp.Attributes.Routing;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.CladdingSystemDetails.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.CladdingSystemDetails.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.CladdingSystemCheckYourAnswers.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.CladdingSystemCheckYourAnswers.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.StartInformation.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorksPackageCladdingSystem.CladdingSystem.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorksPackageCladdingSystem.CladdingSystem.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.FireRiskAppraisalToExternalWalls.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.FireRiskAppraisalToExternalWalls.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.ResetCladdingSystem;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.BaseInformation.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorksPackageCladdingSystem.ResetCladdingSystem;

namespace HE.Remediation.WebApp.Areas.WorksPackage.WorksPackageCladdingSystem.Controllers
{
    [Area("WorksPackageCladdingSystem")]
    [Route("WorksPackage/CladdingSystem")]
    public class CladdingSystemController : StartController
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public CladdingSystemController(ISender sender, IMapper mapper) : base(sender)
        {
            _sender = sender;
            _mapper = mapper;
        }

        protected override IActionResult DefaultStart =>
        RedirectToAction("StartInformation", "CladdingSystem", new { Area = "WorksPackageCladdingSystem" });

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
        public IActionResult StartInformation(StartInformationViewModel viewModel, ESubmitAction submitAction)
        {
            return RedirectToAction("FireRiskAppraisalToExternalWalls", "CladdingSystem", new { Area = "WorksPackageCladdingSystem" });
        }

        #endregion

        #region Fire Risk Appraisal to External Walls (FRAEW)

        [HttpGet(nameof(FireRiskAppraisalToExternalWalls))]
        public async Task<IActionResult> FireRiskAppraisalToExternalWalls(string returnUrl)
        {
            var response = await _sender.Send(GetFireRiskAppraisalToExternalWallsRequest.Request);
            var viewModel = _mapper.Map<FireRiskAppraisalToExternalWallsViewModel>(response);

            viewModel.ReturnUrl = returnUrl;

            return View(viewModel);
        }

        [HttpPost(nameof(FireRiskAppraisalToExternalWalls))]
        public async Task<IActionResult> FireRiskAppraisalToExternalWalls(FireRiskAppraisalToExternalWallsViewModel viewModel,
            ESubmitAction submitAction)
        {
            var validator = new FireRiskAppraisalToExternalWallsViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(viewModel);
            }

            if (submitAction == ESubmitAction.Continue)
            {
                await _sender.Send(SetFireRiskAppraisalToExternalWallsRequest.Request);
                return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
            }

            return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
        }

        #endregion

        #region Cladding system

        [ExcludeRouteRecording]
        [HttpGet("CladdingSystem/{fireRiskCladdingSystemsId:guid}/{claddingSystemIndex:int}")]
        public async Task<IActionResult> CladdingSystem(Guid fireRiskCladdingSystemsId, int claddingSystemIndex)
        {
            var response = await _sender.Send(new GetCladdingSystemRequest
            {
                FireRiskCladdingSystemsId = fireRiskCladdingSystemsId,
                CladdingSystemIndex = claddingSystemIndex
            });

            var viewModel = _mapper.Map<CladdingSystemViewModel>(response);

            return View(viewModel);
        }

        [HttpPost("CladdingSystem/{fireRiskCladdingSystemsId:guid}/{claddingSystemIndex:int}")]
        public async Task<IActionResult> CladdingSystem(CladdingSystemViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new CladdingSystemViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(viewModel);
            }

            var request = _mapper.Map<SetCladdingSystemRequest>(viewModel);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
            }

            return request.IsBeingRemoved is EReplacementCladding.Full or EReplacementCladding.Partial
                ? RedirectToAction("CladdingSystemDetails", "CladdingSystem", new { Area = "WorksPackageCladdingSystem", viewModel.FireRiskCladdingSystemsId, viewModel.CladdingSystemIndex })
                : RedirectToAction("FireRiskAppraisalToExternalWalls", "CladdingSystem", new { Area = "WorksPackageCladdingSystem", viewModel.FireRiskCladdingSystemsId, viewModel.CladdingSystemIndex });
        }

        #endregion

        #region What is replacing cladding system x?

        [ExcludeRouteRecording]
        [HttpGet("CladdingSystemDetails/{fireRiskCladdingSystemsId:guid}/{claddingSystemIndex:int}")]
        public async Task<IActionResult> CladdingSystemDetails(Guid fireRiskCladdingSystemsId, int claddingSystemIndex)
        {
            var response = await _sender.Send(new GetCladdingSystemDetailsRequest
            {
                FireRiskCladdingSystemsId = fireRiskCladdingSystemsId,
                CladdingSystemIndex = claddingSystemIndex
            });

            var viewModel = _mapper.Map<CladdingSystemDetailsViewModel>(response);

            return View(viewModel);
        }

        [HttpPost("CladdingSystemDetails/{fireRiskCladdingSystemsId:guid}/{claddingSystemIndex:int}")]
        public async Task<IActionResult> CladdingSystemDetails(CladdingSystemDetailsViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new CladdingSystemDetailsViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                var getCladdingResponse = await _sender.Send(new GetCladdingSystemDetailsRequest
                {
                    FireRiskCladdingSystemsId = viewModel.FireRiskCladdingSystemsId,
                    CladdingSystemIndex = viewModel.CladdingSystemIndex
                });
                var claddingSystem = _mapper.Map<CladdingSystemDetailsViewModel>(getCladdingResponse);

                viewModel.CladdingTypes = claddingSystem.CladdingTypes;
                viewModel.InsulationTypes = claddingSystem.InsulationTypes;
                viewModel.CladdingManufacturers = claddingSystem.CladdingManufacturers;
                viewModel.InsulationManufacturers = claddingSystem.InsulationManufacturers;

                validationResult.AddToModelState(ModelState, string.Empty);
                return View(viewModel);
            }

            var request = _mapper.Map<SetCladdingSystemDetailsRequest>(viewModel);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("FireRiskAppraisalToExternalWalls", "CladdingSystem", new { Area = "WorksPackageCladdingSystem" });
            }

            return RedirectToAction("CladdingSystemCheckYourAnswers", "CladdingSystem", new { Area = "WorksPackageCladdingSystem", viewModel.FireRiskCladdingSystemsId, viewModel.CladdingSystemIndex });
        }

        #endregion

        #region Cladding system - check your answers

        [ExcludeRouteRecording]
        [HttpGet("CladdingSystemCheckYourAnswers/{fireRiskCladdingSystemsId:guid}/{claddingSystemIndex:int}")]
        public async Task<IActionResult> CladdingSystemCheckYourAnswers(Guid fireRiskCladdingSystemsId, int claddingSystemIndex)
        {
            var response = await _sender.Send(new GetCladdingSystemCheckYourAnswersRequest
            {
                FireRiskCladdingSystemsId = fireRiskCladdingSystemsId,
                CladdingSystemIndex = claddingSystemIndex
            });
            var viewModel = _mapper.Map<CladdingSystemCheckYourAnswersViewModel>(response);

            return View(viewModel);
        }

        [HttpPost("CladdingSystemCheckYourAnswers/{fireRiskCladdingSystemsId:guid}/{claddingSystemIndex:int}")]
        public async Task<IActionResult> CladdingSystemCheckYourAnswers(CladdingSystemCheckYourAnswersViewModel viewModel, ESubmitAction submitAction)
        {
            if (submitAction == ESubmitAction.Continue)
            {
                var request = _mapper.Map<SetCladdingSystemCheckYourAnswersRequest>(viewModel);
                await _sender.Send(request);

                return RedirectToAction("FireRiskAppraisalToExternalWalls", "CladdingSystem", new { Area = "WorksPackageCladdingSystem" });
            }

            return View(viewModel);
        }

        #endregion

        #region Cladding system - Change answers

        [ExcludeRouteRecording]
        [HttpGet("CladdingSystemChangeAnswers/{fireRiskCladdingSystemsId:guid}/{claddingSystemIndex:int}")]
        public async Task<IActionResult> CladdingSystemChangeAnswers([FromRoute] CladdingSystemChangeAnswersRequest request)
        {
            var response = await _sender.Send(GetBaseInformationRequest.Request);
            var model = _mapper.Map<CladdingSystemChangeAnswersViewModel>(response);
            model.FireRiskCladdingSystemsId = request.FireRiskCladdingSystemsId;
            model.CladdingSystemIndex = request.CladdingSystemIndex;

            return View(model);
        }

        [HttpPost("CladdingSystemChangeAnswers/{fireRiskCladdingSystemsId:guid}/{claddingSystemIndex:int}")]
        public async Task<IActionResult> CladdingSystemChangeAnswers(CladdingSystemChangeAnswersViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (viewModel.Proceed == false)
            {
                return RedirectToAction("CladdingSystemCheckYourAnswers", "CladdingSystem", new { Area = "WorksPackageCladdingSystem", viewModel.FireRiskCladdingSystemsId, viewModel.CladdingSystemIndex });
            }

            var request = _mapper.Map<ResetCladdingSystemRequest>(viewModel);
            await _sender.Send(request, cancellationToken);

            return RedirectToAction("CladdingSystem", "CladdingSystem", new { Area = "WorksPackageCladdingSystem", viewModel.FireRiskCladdingSystemsId, viewModel.CladdingSystemIndex });
        }

        #endregion
    }
}
