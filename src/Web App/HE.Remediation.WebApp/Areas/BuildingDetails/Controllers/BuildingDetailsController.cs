using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.GetBuildingDeveloperAddressInformation;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.GetBuildingDeveloperInformation;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.SetBuildingDeveloperAddressInformation;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.SetBuildingDeveloperInformation;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingHasSafetyRegulatorRegistrationCode.GetBuildingHasSafetyRegulatorRegistrationCode;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingHasSafetyRegulatorRegistrationCode.SetBuildingHasSafetyRegulatorRegistrationCode;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingPartOfDevelopment.GetBuildingPartOfDevelopment;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingPartOfDevelopment.SetBuildingPartOfDevelopment;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingSafetyRegulatorRegistrationCode.GetBuildingSafetyRegulatorRegistrationCode;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingSafetyRegulatorRegistrationCode.SetBuildingSafetyRegulatorRegistrationCode;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingUniqueName.GetBuildingUniqueName;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingUniqueName.SetBuildingUniqueName;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.CheckYourAnswers.GetBuildingDetailsAnswers;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.CheckYourAnswers.SetBuildingDetailsComplete;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmBuildingHeight.GetBuildingHeight;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmBuildingHeight.SetBuildingHeight;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperContacted.GetDeveloperContacted;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperContacted.SetDeveloperContacted;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperInBusiness.GetDeveloperInBusiness;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperInBusiness.SetDeveloperInBusiness;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.NameOfDevelopment.GetNameOfDevelopment;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.NameOfDevelopment.SetNameOfDevelopment;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.NonResidentialUnits.GetNonResidentialUnits;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.NonResidentialUnits.SetNonResidentialUnits;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.SetBuildingAddress;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.SetBuildingAddressManual;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ResetSection;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ResidentialUnits.GetResidentialUnits;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ResidentialUnits.SetResidentialUnits;
using HE.Remediation.Core.UseCase.Areas.Location.BuildingLookup;
using HE.Remediation.WebApp.ViewModels.BuildingDetails;
using HE.Remediation.WebApp.ViewModels.Location;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.BuildingDetails.Controllers
{
    [Area("BuildingDetails")]
    [Route("BuildingDetails")]
    public class BuildingDetailsController : StartController
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public BuildingDetailsController(ISender sender, IMapper mapper)
            : base(sender)
        {
            _sender = sender;
            _mapper = mapper;
        }

        protected override IActionResult DefaultStart => RedirectToAction("WhatYoullNeed", "BuildingDetails", new { Area = "BuildingDetails" });

        #region What You'll Need
        [HttpGet(nameof(WhatYoullNeed))]
        public IActionResult WhatYoullNeed()
        {
            return View();
        }
        #endregion

        #region Building Unique Name
        [HttpGet(nameof(BuildingUniqueName))]
        public async Task<IActionResult> BuildingUniqueName()
        {
            var response = await _sender.Send(GetBuildingUniqueNameRequest.Request);

            return View(_mapper.Map<BuildingUniqueNameViewModel>(response));
        }

        [HttpPost(nameof(BuildingUniqueName))]
        public async Task<IActionResult> BuildingUniqueName(BuildingUniqueNameViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new BuildingUniqueNameViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View(viewModel);
            }

            var request = _mapper.Map<SetBuildingUniqueNameRequest>(viewModel);
            await _sender.Send(request);

            return submitAction == ESubmitAction.Continue ?
                RedirectToAction("ResidentialUnits", "BuildingDetails", new { Area = "BuildingDetails" }) :
                RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }
        #endregion

        #region Residential Units
        [HttpGet(nameof(ResidentialUnits))]
        public async Task<IActionResult> ResidentialUnits(string returnUrl)
        {
            var response = await _sender.Send(GetResidentialUnitsRequest.Request);

            var viewModel = _mapper.Map<ResidentialUnitsViewModel>(response);

            viewModel.ReturnUrl = returnUrl;

            return View(viewModel);
        }

        [HttpPost(nameof(ResidentialUnits))]
        public async Task<IActionResult> ResidentialUnits(ResidentialUnitsViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new ResidentialUnitsViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();
                validationResult.AddToModelState(ModelState, String.Empty);
                return View(viewModel);
            }

            var request = _mapper.Map<SetResidentialUnitsRequest>(viewModel);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = request!.NonResidentialUnits == ENoYes.Yes
                ? nameof(NonResidentialUnits)
                : nameof(ProvideBuildingAddress);

            action = viewModel.ReturnUrl is null
                ? action
                : viewModel.ReturnUrl;

            return RedirectToAction(action, "BuildingDetails", new { Area = "BuildingDetails" });
        }
        #endregion

        #region Non-Residential Units
        [HttpGet(nameof(NonResidentialUnits))]
        public async Task<IActionResult> NonResidentialUnits(string returnUrl)
        {
            var response = await _sender.Send(GetNonResidentialUnitsRequest.Request);

            var viewModel = _mapper.Map<NonResidentialUnitsViewModel>(response);

            viewModel.ReturnUrl = returnUrl;

            return View(viewModel);
        }

        [HttpPost(nameof(NonResidentialUnits))]
        public async Task<IActionResult> NonResidentialUnits(NonResidentialUnitsViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new NonResidentialUnitsViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();
                validationResult.AddToModelState(ModelState, String.Empty);
                return View(viewModel);
            }

            var request = _mapper.Map<SetNonResidentialUnitsRequest>(viewModel);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = viewModel.ReturnUrl is null
                ? nameof(ProvideBuildingAddress)
                : viewModel.ReturnUrl;

            return RedirectToAction(action, "BuildingDetails", new { Area = "BuildingDetails" });
        }
        #endregion

        #region Confirm Building Height
        [HttpGet(nameof(ConfirmBuildingHeight))]
        public async Task<IActionResult> ConfirmBuildingHeight(string returnUrl)
        {
            var response = await _sender.Send(GetBuildingHeightRequest.Request);

            var viewModel = _mapper.Map<ConfirmBuildingHeightViewModel>(response);

            viewModel.ReturnUrl = returnUrl;

            return View(viewModel);
        }

        [HttpPost(nameof(ConfirmBuildingHeight))]
        public async Task<IActionResult> ConfirmBuildingHeight(ConfirmBuildingHeightViewModel model)
        {
            var validator = new ConfirmBuildingHeightViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetBuildingHeightRequest>(model);

            await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = model.ReturnUrl is null
                ? nameof(BuildingHasSafetyRegulatorRegistrationCode)
                : model.ReturnUrl;

            return RedirectToAction(action, "BuildingDetails", new { Area = "BuildingDetails" });
        }
        #endregion

        #region Building Has Safety Regulator Registration Code
        [HttpGet(nameof(BuildingHasSafetyRegulatorRegistrationCode))]
        public async Task<IActionResult> BuildingHasSafetyRegulatorRegistrationCode(string returnUrl)
        {
            var response = await _sender.Send(GetBuildingHasSafetyRegulatorRegistrationCodeRequest.Request);
            var viewModel = _mapper.Map<BuildingHasSafetyRegulatorRegistrationCodeViewModel>(response);
            viewModel.ReturnUrl = returnUrl;
            return View(viewModel);
        }

        [HttpPost(nameof(BuildingHasSafetyRegulatorRegistrationCode))]
        public async Task<IActionResult> BuildingHasSafetyRegulatorRegistrationCode(BuildingHasSafetyRegulatorRegistrationCodeViewModel model)
        {
            var validator = new BuildingHasSafetyRegulatorRegistrationCodeViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetBuildingHasSafetyRegulatorRegistrationCodeRequest>(model);
            await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = model.ReturnUrl is null
                ? (model.BuildingHasSafetyRegulatorRegistrationCode.HasValue && model.BuildingHasSafetyRegulatorRegistrationCode.Value ? nameof(BuildingSafetyRegulatorRegistrationCode) : nameof(BuildingDeveloperInformation))
                : model.ReturnUrl;

            return RedirectToAction(action, "BuildingDetails", new { Area = "BuildingDetails" });
        }
        #endregion

        #region Building Safety Regulator Registration Code
        [HttpGet(nameof(BuildingSafetyRegulatorRegistrationCode))]
        public async Task<IActionResult> BuildingSafetyRegulatorRegistrationCode()
        {
            var response = await _sender.Send(GetBuildingSafetyRegulatorRegistrationCodeRequest.Request);

            return View(_mapper.Map<BuildingSafetyRegulatorRegistrationCodeViewModel>(response));
        }

        [HttpPost(nameof(BuildingSafetyRegulatorRegistrationCode))]
        public async Task<IActionResult> BuildingSafetyRegulatorRegistrationCode(BuildingSafetyRegulatorRegistrationCodeViewModel model, ESubmitAction submitAction)
        {
            var validator = new BuildingSafetyRegulatorRegistrationCodeViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetBuildingSafetyRegulatorRegistrationCodeRequest>(model);
            await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = model.ReturnUrl is null
                ? nameof(BuildingDeveloperInformation)
                : model.ReturnUrl;

            return RedirectToAction(action, "BuildingDetails", new { Area = "BuildingDetails" });
        }
        #endregion

        #region Building Part Of Development
        [HttpGet(nameof(BuildingPartOfDevelopment))]
        public async Task<IActionResult> BuildingPartOfDevelopment(string returnUrl)
        {
            var response = await _sender.Send(GetBuildingPartOfDevelopmentRequest.Request);

            var viewModel = _mapper.Map<BuildingPartOfDevelopmentViewModel>(response);

            viewModel.ReturnUrl = returnUrl;

            return View(viewModel);
        }

        [HttpPost(nameof(BuildingPartOfDevelopment))]
        public async Task<IActionResult> BuildingPartOfDevelopment(BuildingPartOfDevelopmentViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new BuildingPartOfDevelopmentViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View(viewModel);
            }

            var request = _mapper.Map<SetBuildingPartOfDevelopmentRequest>(viewModel);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = request!.PartOfDevelopment == ENoYes.Yes
                ? nameof(NameOfDevelopment)
                : nameof(ConfirmBuildingHeight);

            action = viewModel.ReturnUrl is null
                ? action
                : viewModel.ReturnUrl;

            return RedirectToAction(action, "BuildingDetails", new { Area = "BuildingDetails" });
        }
        #endregion

        #region Post Code Item
        [HttpPost(nameof(PostCodeItemSelected))]
        public async Task<IActionResult> PostCodeItemSelected(string returnUrl, PostCodeSelectionViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new PostCodeSelectionViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                // need to set these properties on the output model if there is an error
                return View("ProvideBuildingAddressResults", viewModel);
            }

            var request = _mapper.Map<SetBuildingAddressRequest>(viewModel);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Continue)
            {
                var action = viewModel.ReturnUrl is null
                             ? nameof(BuildingPartOfDevelopment)
                             : viewModel.ReturnUrl;

                return RedirectToAction(action, "BuildingDetails", new { Area = "BuildingDetails" });
            }
            else if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            return View("ProvideBuildingAddress", viewModel);
        }

        [HttpGet(nameof(PostCodeItemEntered))]
        public async Task<IActionResult> PostCodeItemEntered(string returnUrl, PostCodeEntryViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new PostCodeEntryViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View("ProvideBuildingAddress", viewModel);
            }

            if (submitAction == ESubmitAction.FindAddress)
            {
                var response = await _sender.Send(new BuildingLookupRequest { Postcode = viewModel.PostCode });
                var newMappedModel = _mapper.Map<PostCodeSelectionViewModel>(response);

                if (!newMappedModel.HaveResults)
                {
                    var manualViewModel = _mapper.Map<PostCodeManualViewModel>(response);
                    manualViewModel.Postcode = viewModel.PostCode;
                    return View("ProvideBuildingAddressManual", manualViewModel);
                }
                return View("ProvideBuildingAddressResults", newMappedModel);
            }

            return View("ProvideBuildingAddress", viewModel);
        }
        #endregion

        #region Provide Building Address
        [HttpGet(nameof(ProvideBuildingAddressManual))]
        public async Task<IActionResult> ProvideBuildingAddressManual(string returnUrl, string postCode)
        {
            var response = await _sender.Send(GetBuildingAddressRequest.Request);

            var viewModel = _mapper.Map<PostCodeManualViewModel>(response);
            return View(viewModel);
        }

        [HttpGet(nameof(ProvideBuildingAddress))]
        public async Task<IActionResult> ProvideBuildingAddress(string returnUrl)
        {
            var response = await _sender.Send(GetBuildingAddressRequest.Request);

            var viewModel = _mapper.Map<PostCodeEntryViewModel>(response);
            viewModel.ReturnUrl = returnUrl;

            return View(viewModel);
        }

        [HttpPost(nameof(ProvideBuildingAddressManual))]
        public async Task<IActionResult> ProvideBuildingAddressManual(PostCodeManualViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new PostCodeManualViewModelValidator(true, false);
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View(viewModel);
            }

            var request = _mapper.Map<SetBuildingAddressManualRequest>(viewModel);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = viewModel.ReturnUrl is null
                ? nameof(BuildingPartOfDevelopment)
                : viewModel.ReturnUrl;

            return RedirectToAction(action, "BuildingDetails", new { Area = "BuildingDetails" });
        }
        #endregion

        #region Building Developer Information
        [HttpGet(nameof(BuildingDeveloperInformation))]
        public async Task<IActionResult> BuildingDeveloperInformation(string returnUrl)
        {
            var response = await _sender.Send(GetBuildingDeveloperInformationRequest.Request);

            var viewModel = _mapper.Map<BuildingDeveloperInformationViewModel>(response);

            viewModel.ReturnUrl = returnUrl;

            return View(viewModel);
        }

        [HttpPost(nameof(BuildingDeveloperInformation))]
        public async Task<IActionResult> BuildingDeveloperInformation(BuildingDeveloperInformationViewModel model)
        {
            var validator = new BuildingDeveloperInformationViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetBuildingDeveloperInformationRequest>(model);
            await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            if (!string.IsNullOrEmpty(model.ReturnUrl))
            {
                return RedirectToAction(model.ReturnUrl, "BuildingDetails", new { Area = "BuildingDetails" });
            }
            if (model.DoYouKnowOriginalDeveloper!.Value)
            {
                return RedirectToAction(nameof(BuildingDeveloperInformationAddress), "BuildingDetails", new { Area = "BuildingDetails" });
            }

            return RedirectToAction("CheckYourAnswers", new { returnUrl = "BuildingDeveloperInformation" });
        }

        [HttpGet(nameof(BuildingDeveloperInformationAddress))]
        public async Task<IActionResult> BuildingDeveloperInformationAddress(string returnUrl)
        {
            var response = await _sender.Send(GetBuildingDeveloperInformationAddressRequest.Request);

            var viewModel = _mapper.Map<BuildingDeveloperInformationAddressViewModel>(response);

            viewModel.ReturnUrl = returnUrl;
            return View(viewModel);
        }

        [HttpPost(nameof(BuildingDeveloperInformationAddress))]
        public async Task<IActionResult> BuildingDeveloperInformationAddress(BuildingDeveloperInformationAddressViewModel model)
        {
            var validator = new BuildingDeveloperInformationAddressViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetBuildingDeveloperInformationAddressRequest>(model);
            await _sender.Send(request);

            var action = !string.IsNullOrEmpty(model.ReturnUrl)
                ? model.ReturnUrl
                : nameof(DeveloperInBusiness);

            return RedirectToAction(action);
        }


        #endregion

        #region Developer In Business
        [HttpGet(nameof(DeveloperInBusiness))]
        public async Task<IActionResult> DeveloperInBusiness(string returnUrl)
        {
            var response = await _sender.Send(GetDeveloperInBusinessRequest.Request);
            var viewModel = _mapper.Map<DeveloperInBusinessViewModel>(response);
            viewModel.ReturnUrl = returnUrl;
            return View(viewModel);
        }

        [HttpPost(nameof(DeveloperInBusiness))]
        public async Task<IActionResult> DeveloperInBusiness(DeveloperInBusinessViewModel model)
        {
            var validator = new DeveloperInBusinessViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetDeveloperInBusinessRequest>(model);
            await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            return model.IsOriginalDeveloperStillInBusiness == EApplicationDeveloperInBusinessType.Yes
                ? RedirectToAction("DeveloperContacted", "BuildingDetails", new { Area = "BuildingDetails" })
                : RedirectToAction("CheckYourAnswers", new { returnUrl = "DeveloperInBusiness" });
        }
        #endregion

        #region Developer Contacted
        [HttpGet(nameof(DeveloperContacted))]
        public async Task<IActionResult> DeveloperContacted(string returnUrl)
        {
            var response = await _sender.Send(GetDeveloperContactedRequest.Request);
            var viewModel = _mapper.Map<DeveloperContactedViewModel>(response);
            viewModel.ReturnUrl = returnUrl;
            return View(viewModel);
        }

        [HttpPost(nameof(DeveloperContacted))]
        public async Task<IActionResult> DeveloperContacted(DeveloperContactedViewModel model)
        {
            var validator = new DeveloperContactedViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetDeveloperContactedRequest>(model);
            await _sender.Send(request);

            return model.SubmitAction == ESubmitAction.Continue
                ? RedirectToAction("CheckYourAnswers", new { returnUrl = "DeveloperContacted" })
                : RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }
        #endregion

        #region Name Of Development
        [HttpGet(nameof(NameOfDevelopment))]
        public async Task<IActionResult> NameOfDevelopment()
        {
            var response = await _sender.Send(GetNameOfDevelopmentRequest.Request);

            return View(_mapper.Map<NameOfDevelopmentViewModel>(response));
        }

        [HttpPost(nameof(NameOfDevelopment))]
        public async Task<IActionResult> NameOfDevelopment(NameOfDevelopmentViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new NameOfDevelopmentViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(viewModel);
            }

            var request = _mapper.Map<SetNameOfDevelopmentRequest>(viewModel);
            await _sender.Send(request);

            return submitAction == ESubmitAction.Continue
                ? RedirectToAction("ConfirmBuildingHeight", "BuildingDetails", new { Area = "BuildingDetails" })
                : RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }
        #endregion

        #region Check Your Answers
        [HttpGet(nameof(CheckYourAnswers))]
        public async Task<IActionResult> CheckYourAnswers(string returnURL)
        {
            var response = await _sender.Send(GetBuildingDetailsAnswersRequest.Request);

            var model = _mapper.Map<CheckYourAnswersViewModel>(response);
            model.ReturnURL = returnURL;
            return View(model);
        }

        [HttpPost(nameof(CheckYourAnswers))]
        public async Task<IActionResult> CheckYourAnswers(CheckYourAnswersViewModel viewModel)
        {
            await _sender.Send(SetBuildingDetailsCompleteRequest.Request);
            return RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }
        #endregion

        #region ChangeAnswers

        [HttpGet(nameof(ChangeAnswers))]
        public IActionResult ChangeAnswers()
        {
            return View(new ChangeAnswersViewModel());
        }

        [HttpPost(nameof(ChangeAnswers))]
        public async Task<IActionResult> ChangeAnswers(ChangeAnswersViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Proceed == false)
            {
                return RedirectToAction("CheckYourAnswers");
            }

            await _sender.Send(new ResetBuildingDetailsSectionRequest(), cancellationToken);

            return RedirectToAction("WhatYoullNeed");
        }
        #endregion
    }
}