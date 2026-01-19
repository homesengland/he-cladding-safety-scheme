using AutoMapper;

using FluentValidation.AspNetCore;

using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.GetBuildingDeveloperAddressInformation;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.GetBuildingDeveloperInformation;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.SetBuildingDeveloperAddressInformation;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.SetBuildingDeveloperInformation;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingHasSafetyRegulatorRegistrationCode.GetBuildingHasSafetyRegulatorRegistrationCode;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingHasSafetyRegulatorRegistrationCode.SetBuildingHasSafetyRegulatorRegistrationCode;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingPartOfDevelopment.GetBuildingPartOfDevelopment;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingPartOfDevelopment.SetBuildingPartOfDevelopment;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingRemediation.GetBuildingRemediationResponsibilityReason;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingRemediation.SetBuildingRemediationResponsibilityReason;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingSafetyRegulatorRegistrationCode.GetBuildingSafetyRegulatorRegistrationCode;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingSafetyRegulatorRegistrationCode.SetBuildingSafetyRegulatorRegistrationCode;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingsInsurance.GetBuildingsInsurance;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingsInsurance.SetBuildingsInsurance;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingUniqueName.GetBuildingUniqueName;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingUniqueName.SetBuildingUniqueName;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.CheckYourAnswers.GetBuildingDetailsAnswers;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.CheckYourAnswers.SetBuildingDetailsComplete;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmBuildingHeight.GetBuildingHeight;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmBuildingHeight.SetBuildingHeight;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmKeyDates.Get;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmKeyDates.Set;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConstructionCompletionDate.Get;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConstructionCompletionDate.Set;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperContacted.GetDeveloperContacted;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperContacted.SetDeveloperContacted;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperInBusiness.GetDeveloperInBusiness;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperInBusiness.SetDeveloperInBusiness;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.LocalAuthority.GetLocalAuthorityCostCentre;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.LocalAuthority.SetLocalAuthorityCostCentre;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.NameOfDevelopment.GetNameOfDevelopment;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.NameOfDevelopment.SetNameOfDevelopment;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.NonResidentialUnits.GetNonResidentialUnits;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.NonResidentialUnits.SetNonResidentialUnits;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.SetBuildingAddress;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.SetBuildingAddressManual;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.RefurbishmentCompletionDate.Get;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.RefurbishmentCompletionDate.Set;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ResetSection;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ResidentialUnits.GetResidentialUnits;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ResidentialUnits.SetResidentialUnits;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.WorksAlreadyCompleted.GetWorksAlreadyCompleted;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.WorksAlreadyCompleted.SetWorksAlreadyCompleted;
using HE.Remediation.Core.UseCase.Areas.Location.BuildingLookup;
using HE.Remediation.WebApp.ViewModels.BuildingDetails;
using HE.Remediation.WebApp.ViewModels.BuildingsInsurance;
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
        private readonly IApplicationDataProvider _applicationDataProvider;

        public BuildingDetailsController(ISender sender, IMapper mapper, IApplicationDataProvider applicationDataProvider)
            : base(sender)
        {
            _sender = sender;
            _mapper = mapper;
            _applicationDataProvider = applicationDataProvider;
        }

        protected override IActionResult DefaultStart => RedirectToAction("WhatYoullNeed", "BuildingDetails", new { Area = "BuildingDetails" });

        #region What You'll Need
        [HttpGet(nameof(WhatYoullNeed))]
        public IActionResult WhatYoullNeed()
        {
            var model = new WhatYoullNeedViewModel{ ApplicationScheme = _applicationDataProvider.GetApplicationScheme() };
            return View(model);
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
            viewModel.ApplicationScheme = _applicationDataProvider.GetApplicationScheme();

            var request = _mapper.Map<SetBuildingUniqueNameRequest>(viewModel);
            await _sender.Send(request);

            if (viewModel.ApplicationScheme == EApplicationScheme.CladdingSafetyScheme || viewModel.ApplicationScheme == EApplicationScheme.SocialSector || viewModel.ApplicationScheme == EApplicationScheme.ResponsibleActorsScheme)
            {
                return submitAction == ESubmitAction.Continue ?
                    RedirectToAction("WorksAlreadyCompleted", "BuildingDetails", new { Area = "BuildingDetails" }) :
                    RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }
            else
            {
                return submitAction == ESubmitAction.Continue ?
                    RedirectToAction("ResidentialUnits", "BuildingDetails", new { Area = "BuildingDetails" }) :
                    RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }
        }
        #endregion

        #region Works Already Completed
        [HttpGet(nameof(WorksAlreadyCompleted))]
        public async Task<IActionResult> WorksAlreadyCompleted(string returnUrl)
        {
            var response = await _sender.Send(GetWorksAlreadyCompletedRequest.Request);
            var viewModel = _mapper.Map<WorksAlreadyCompletedViewModel>(response);
            viewModel.ReturnUrl = returnUrl;
            return View(viewModel);
        }

        [HttpPost(nameof(WorksAlreadyCompleted))]
        public async Task<IActionResult> WorksAlreadyCompleted(WorksAlreadyCompletedViewModel model)
        {
            var validator = new WorksAlreadyCompletedViewModelValidator();

            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            model.ApplicationScheme = _applicationDataProvider.GetApplicationScheme();

            var request = _mapper.Map<SetWorksAlreadyCompletedRequest>(model);
            await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Continue)
            {
                if ((model.ApplicationScheme == EApplicationScheme.SocialSector || model.ApplicationScheme == EApplicationScheme.ResponsibleActorsScheme)
                    && model.WorksAlreadyCompleted == true)
                {
                    return RedirectToAction("ConfirmKeyDates", "BuildingDetails", new { Area = "BuildingDetails" });
                }

                return RedirectToAction("ResidentialUnits", "BuildingDetails", new { Area = "BuildingDetails" });
            }

            return RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }
        #endregion

        #region Confirm Key Dates
        [HttpGet(nameof(ConfirmKeyDates))]
        public async Task<IActionResult> ConfirmKeyDates(string returnUrl)
        {
            var response = await _sender.Send(GetConfirmKeyDatesRequest.Request);
            var viewModel = _mapper.Map<ConfirmKeyDatesViewModel>(response);
            viewModel.ReturnUrl = returnUrl;
            return View(viewModel);
        }

        [HttpPost(nameof(ConfirmKeyDates))]
        public async Task<IActionResult> ConfirmKeyDates(ConfirmKeyDatesViewModel model)
        {
            var validator = new ConfirmKeyDatesViewModelValidator();

            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetConfirmKeyDatesRequest>(model);
            await _sender.Send(request);

            return model.SubmitAction == ESubmitAction.Continue ?
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
            viewModel.ApplicationScheme = _applicationDataProvider.GetApplicationScheme();
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

            viewModel.ApplicationScheme = _applicationDataProvider.GetApplicationScheme();
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

            return SafeRedirectToAction(action, "BuildingDetails", new { Area = "BuildingDetails" });
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

            return SafeRedirectToAction(action, "BuildingDetails", new { Area = "BuildingDetails" });
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

            return SafeRedirectToAction(action, "BuildingDetails", new { Area = "BuildingDetails" });
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
                             ? (model.BuildingHasSafetyRegulatorRegistrationCode.HasValue &&
                                model.BuildingHasSafetyRegulatorRegistrationCode.Value
                                     ? nameof(BuildingSafetyRegulatorRegistrationCode)
                                     : model.ApplicationScheme == EApplicationScheme.ResponsibleActorsScheme
                                         ? nameof(ResponsibleForTheRemediation)
                                         : nameof(BuildingDeveloperInformation))
                             : model.ReturnUrl;

            // override ReturnUrl ONLY when action is ResponsibleForTheRemediation
            var returnUrl =
                action == nameof(ResponsibleForTheRemediation) && (model.BuildingHasSafetyRegulatorRegistrationCode.HasValue &&
                                !model.BuildingHasSafetyRegulatorRegistrationCode.Value)
                    ? nameof(BuildingHasSafetyRegulatorRegistrationCode)
                    : model.ReturnUrl;

            return SafeRedirectToAction(action, "BuildingDetails", new { Area = "BuildingDetails", returnUrl = returnUrl });
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
                ? model.ApplicationScheme == EApplicationScheme.ResponsibleActorsScheme ? nameof(ResponsibleForTheRemediation) : nameof(BuildingDeveloperInformation)
                : model.ReturnUrl;

            return SafeRedirectToAction(action, "BuildingDetails", new { Area = "BuildingDetails" });
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

            return SafeRedirectToAction(action, "BuildingDetails", new { Area = "BuildingDetails" });
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
                             ? nameof(ProvideLocalAuthority)
                             : viewModel.ReturnUrl;

                return SafeRedirectToAction(action, "BuildingDetails", new { Area = "BuildingDetails" });
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
            var validator = new PostCodeManualViewModelValidator(false);
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
                ? nameof(ProvideLocalAuthority)
                : viewModel.ReturnUrl;

            return SafeRedirectToAction(action, "BuildingDetails", new { Area = "BuildingDetails" });
        }
        #endregion

        #region Local Authority

        [HttpGet(nameof(ProvideLocalAuthority))]
        public async Task<IActionResult> ProvideLocalAuthority(string returnUrl)
        {
            var response = await _sender.Send(GetLocalAuthorityCostCentreRequest.Request);

            var viewModel = _mapper.Map<LocalAuthorityCostCentreViewModel>(response);
            viewModel.ReturnUrl = returnUrl;

            return View(viewModel);
        }

        [HttpPost(nameof(ProvideLocalAuthority))]
        public async Task<IActionResult> ProvideLocalAuthority(LocalAuthorityCostCentreViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new LocalAuthorityCostCentreViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(viewModel);
            }

            var request = _mapper.Map<SetLocalAuthorityCostCentreRequest>(viewModel);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = viewModel.ReturnUrl is null
                ? nameof(BuildingPartOfDevelopment)
                : viewModel.ReturnUrl;

            return SafeRedirectToAction(action, "BuildingDetails", new { Area = "BuildingDetails" });
        }

        #endregion

        #region Responsible For The Remediation
        [HttpGet(nameof(ResponsibleForTheRemediation))]
        public async Task<IActionResult> ResponsibleForTheRemediation(string returnUrl)
        {
            var response = await _sender.Send(GetBuildingRemediationResponsibilityReasonRequest.Request);

            var viewModel = _mapper.Map<ResponsibleForTheRemediationViewModel>(response);

            viewModel.ReturnUrl = returnUrl;

            return View(viewModel);
        }

        [HttpPost(nameof(ResponsibleForTheRemediation))]
        public async Task<IActionResult> ResponsibleForTheRemediation(ResponsibleForTheRemediationViewModel model)
        {
            var validator = new ResponsibleForTheRemediationViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetBuildingRemediationResponsibilityReasonRequest>(model);
            await _sender.Send(request);

            var action = model.BuildingRemediationResponsibilityType == EBuildingRemediationResponsibilityType.YouBuiltTheBuilding
                         ? nameof(ConstructionCompletionDate) : nameof(RefurbishmentCompletionDate);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            if (!string.IsNullOrEmpty(model.ReturnUrl) && model.ReturnUrl == "CheckYourAnswers")
            {
                return SafeRedirectToAction(model.ReturnUrl, "BuildingDetails", new { Area = "BuildingDetails" });
            }

            return SafeRedirectToAction(action, "BuildingDetails", new { Area = "BuildingDetails" });
        }

        #endregion

        #region Construction Completion Date
        [HttpGet(nameof(ConstructionCompletionDate))]
        public async Task<IActionResult> ConstructionCompletionDate(string returnUrl)
        {
            var response = await _sender.Send(GetConstructionCompletionDateRequest.Request);
            var viewModel = _mapper.Map<ConstructionCompletionDateViewModel>(response);
            viewModel.ReturnUrl = returnUrl;
            return View(viewModel);
        }

        [HttpPost(nameof(ConstructionCompletionDate))]
        public async Task<IActionResult> ConstructionCompletionDate(ConstructionCompletionDateViewModel model)
        {
            var validator = new ConstructionCompletionDateViewModelValidator();

            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetConstructionCompletionDateRequest>(model);
            await _sender.Send(request);

            return model.SubmitAction == ESubmitAction.Continue ?
                RedirectToAction("BuildingsInsurance", "BuildingDetails", new { Area = "BuildingDetails" }) :
                RedirectToAction("Index", "TaskList", new { Area = "Application" });
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
                return SafeRedirectToAction(model.ReturnUrl, "BuildingDetails", new { Area = "BuildingDetails" });
            }
            if (model.DoYouKnowOriginalDeveloper!.Value)
            {
                return RedirectToAction(nameof(BuildingDeveloperInformationAddress), "BuildingDetails", new { Area = "BuildingDetails" });
            }

            return RedirectToAction("BuildingsInsurance", new { returnUrl = "BuildingDeveloperInformation" });
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

            return SafeRedirectToAction(action, null, null);
        }

        #endregion

        #region Building Insurance
        [HttpGet(nameof(BuildingsInsurance))]
        public async Task<IActionResult> BuildingsInsurance(string returnUrl)
        {
            var response = await _sender.Send(GetBuildingsInsuranceRequest.Request);

            var viewModel = _mapper.Map<BuildingsInsuranceViewModel>(response);

            return View(viewModel);
        }

        [HttpPost(nameof(BuildingsInsurance))]
        public async Task<IActionResult> BuildingsInsurance(BuildingsInsuranceViewModel model)
        {
            var validator = new BuildingsInsuranceViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetBuildingsInsuranceRequest>(model);
            await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            if (!string.IsNullOrEmpty(model.ReturnUrl))
            {
                return SafeRedirectToAction(model.ReturnUrl, "BuildingDetails", new { Area = "BuildingDetails" });
            }

            return RedirectToAction("CheckYourAnswers", new { returnUrl = "BuildingDeveloperInformation" });
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
            viewModel.ApplicationScheme = _applicationDataProvider.GetApplicationScheme();
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

        #region Refurbishment Completion Date
        [HttpGet(nameof(RefurbishmentCompletionDate))]
        public async Task<IActionResult> RefurbishmentCompletionDate(string returnUrl)
        {
            var response = await _sender.Send(GetRefurbishmentCompletionDateRequest.Request);
            var viewModel = _mapper.Map<RefurbishmentCompletionDateViewModel>(response);
            viewModel.ReturnUrl = returnUrl;
            return View(viewModel);
        }

        [HttpPost(nameof(RefurbishmentCompletionDate))]
        public async Task<IActionResult> RefurbishmentCompletionDate(RefurbishmentCompletionDateViewModel model)
        {
            var validator = new RefurbishmentCompletionDateViewModelValidator();

            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetRefurbishmentCompletionDateRequest>(model);
            await _sender.Send(request);

            return model.SubmitAction == ESubmitAction.Continue ?
                RedirectToAction("BuildingsInsurance", "BuildingDetails", new { Area = "BuildingDetails" }) :
                RedirectToAction("Index", "TaskList", new { Area = "Application" });
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