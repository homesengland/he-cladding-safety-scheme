using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.EntityResponsibleForGFA.GetResponsibleEntityResponsibleForGrantFunding;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.EntityResponsibleForGFA.SetResponsibleEntityResponsibleForGrantFunding;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.FreeholderCompanyOrIndividual.GetFreeholderCompanyOrIndividual;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.FreeholderCompanyOrIndividual.SetFreeholderCompanyOrIndividual;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatories.DeleteGrantFundingSignatory;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatories.GetGrantFundingSignatories;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatoryDetails.GetGrantFundingSignatoryDetails;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatoryDetails.SetGrantFundingSignatoryDetails;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.NotEligible.GetNotEligible;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.NotEligible.SetNotEligible;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepCompanyOrIndividual.GetRepCompanyOrIndividual;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepCompanyOrIndividual.SetRepCompanyOrIndividual;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.Representative.GetRepresentativeType;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.Representative.SetRepresentativeType;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepresentativeBasedInUk.GetRepresentativeBasedInUk;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepresentativeBasedInUk.SetRepresentativeBasedInUk;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanyRelationDetails;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanySubType.GetResponsibleEntityCompanySubType;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanySubType.SetResponsibleEntityCompanySubType;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanyType.GetResponsibleEntityCompanyType;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanyType.SetResponsibleEntityCompanyType;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityRelation.GetResponsibleEntityRelation;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityRelation.SetResponsibleEntityRelation;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityUkRegistered.GetResponsibleEntityUkRegistered;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityUkRegistered.SetResponsibleEntityUkRegistered;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RightToManage;
using HE.Remediation.WebApp.Constants;
using HE.Remediation.WebApp.ViewModels.Location;
using HE.Remediation.WebApp.ViewModels.ResponsibleEntities;
using HE.Remediation.WebApp.ViewModels.ResponsibleEntities.RightToManage;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.ResponsibleEntities.Controllers
{
    [Area("ResponsibleEntities")]
    [Route("ResponsibleEntities")]
    public class ResponsibleEntitiesController : StartController
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public ResponsibleEntitiesController(ISender sender, IMapper mapper, IApplicationDataProvider applicationDataProvider)
            : base (sender)
        {
            _sender = sender;
            _mapper = mapper;
            _applicationDataProvider = applicationDataProvider;
        }

        protected override IActionResult DefaultStart => RedirectToAction("Information", "ResponsibleEntities", new { Area = "ResponsibleEntities" });

        #region Information
        [HttpGet(nameof(Information))]
        public IActionResult Information()
        {
            return View();
        }
        #endregion

        #region Representative
        [HttpGet(nameof(Representative))]
        public async Task<IActionResult> Representative()
        {
            var response = await _sender.Send(GetRepresentativeTypeRequest.Request);
            var model = _mapper.Map<RepresentativeTypeViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(Representative))]
        public async Task<IActionResult> Representative(RepresentativeTypeViewModel model)
        {
            var validator = new RepresentativeTypeViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetRepresentativeTypeRequest>(model);

            await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            return model.RepresentativeType == EApplicationRepresentationType.Representative
                ? RedirectToAction("BasedInUk", "ResponsibleEntities", new { Area = "ResponsibleEntities" })
                : RedirectToAction("ResponsibleEntityRelation", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }
        #endregion

        #region BasedInUk
        [HttpGet(nameof(BasedInUk))]
        public async Task<IActionResult> BasedInUk(string returnUrl)
        {
            var response = await _sender.Send(GetRepresentativeBasedInUkRequest.Request);

            var model = _mapper.Map<BasedInUkViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(BasedInUk))]
        public async Task<IActionResult> BasedInUk(BasedInUkViewModel model)
        {
            var validator = new BasedInUkViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetRepresentativeBasedInUkRequest>(model);

            await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var isPrivateSectorSelfFunded = _applicationDataProvider.GetApplicationScheme() == EApplicationScheme.SelfRemediating;

            if (request.BasedInUk!.Value || isPrivateSectorSelfFunded)
            {
                var action = model.ReturnUrl is null
                ? nameof(RepresentationCompanyOrIndividual)
                : model.ReturnUrl;

                return SafeRedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

            return RedirectToAction("NotEligible", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }
        #endregion

        #region NotEligible
        [HttpGet(nameof(NotEligible))]
        public async Task<IActionResult> NotEligible()
        {
            var response = await _sender.Send(GetNotEligibleRequest.Request);

            if (response.StatusId == EApplicationStatus.ApplicationNotEligible)
            {
                ViewData["BackLinkHidden"] = true;
            }

            if (response is { RepresentationType: EApplicationRepresentationType.Representative, IsUkBased: false })
            {
                ViewData["BackLink"] = Url.Action("BasedInUk", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
                return View("NotEligibleNotInUk");
            }

            if (response is { IsUkRegistered: false, ApplicationScheme: EApplicationScheme.CladdingSafetyScheme })
            {
                ViewData["BackLink"] = Url.Action("ResponsibleEntityUkRegistered", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
                return View("NotEligibleNotInUk");
            }

            if (response is { HasAcquiredRightToManage: false })
            {
                ViewData["BackLink"] = Url.Action("AcquiredRightToManage", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
                return View("NotEligibleNoRightToManage");
            }

            if (response is not
                {
                    CompanyType: EApplicationResponsibleEntityOrganisationType.RegisteredProvider
                    or EApplicationResponsibleEntityOrganisationType.LocalAuthority,
                    IsClaimingGrant: true
                } &&
                response is not
                {
                    IsClaimingGrant: false,
                    HasOwners: true
                })
            {
                ViewData["BackLink"] = Url.Action("ClaimingGrant", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
                return View("NotEligiblePrivateTenantOrUnaffordable");
            }

            throw new Exception("Not Eligible - No valid redirect page found");
        }

        [HttpPost(nameof(NotEligible))]
        public async Task<IActionResult> NotEligibleReturnHome()
        {
            await _sender.Send(SetNotEligibleRequest.Request);
            return RedirectToAction("Index", "Dashboard", new { Area = "Application" });
        }
        #endregion

        #region RepresentationCompanyOrIndividual

        [HttpGet(nameof(RepresentationCompanyOrIndividual))]
        public async Task<IActionResult> RepresentationCompanyOrIndividual(string returnUrl)
        {
            var response = await _sender.Send(GetRepCompanyOrIndividualRequest.Request);

            var model = _mapper.Map<RepresentationCompanyOrIndividualViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(RepresentationCompanyOrIndividual))]
        public async Task<IActionResult> RepresentationCompanyOrIndividual(RepresentationCompanyOrIndividualViewModel model)
        {
            var validator = new RepresentationCompanyOrIndividualViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetRepCompanyOrIndividualRequest>(model);
            await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = model.ReturnUrl is null
                ? nameof(RepresentationCompanyOrIndividualDetails)
                : model.ReturnUrl;

            return SafeRedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }

        #endregion

        #region RepresentationCompanyOrIndividualDetails
        [HttpGet(nameof(RepresentationCompanyOrIndividualDetails))]
        public async Task<IActionResult> RepresentationCompanyOrIndividualDetails(string returnUrl)
        {
            var isRasScheme = _applicationDataProvider.GetApplicationScheme() == EApplicationScheme.ResponsibleActorsScheme;

            if (isRasScheme)
            {
                _ = await _sender.Send(new SetRepresentativeTypeRequest { RepresentativeType = EApplicationRepresentationType.Representative });
                _ = await _sender.Send(new SetRepCompanyOrIndividualRequest { ReponsibleEntityType = EResponsibleEntityType.Company });
            }

            var response = await _sender.Send(GetRepresentationCompanyOrIndividualDetailsRequest.Request);

            var model = _mapper.Map<RepresentationCompanyOrIndividualDetailsViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(RepresentationCompanyOrIndividualDetails))]
        public async Task<IActionResult> RepresentationCompanyOrIndividualDetails(RepresentationCompanyOrIndividualDetailsViewModel model)
        {
            var validator = new RepresentationCompanyOrIndividualDetailsViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetRepresentationCompanyOrIndividualDetailsRequest>(model);
            await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = model.ReturnUrl is null
                ? nameof(RepresentationCompanyOrIndividualAddressDetails)
                : model.ReturnUrl;

            return SafeRedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }
        #endregion

        #region RepresentationCompanyOrIndividualAddressDetails

        /// <summary>
        /// Shows the initial post code entry screen - a text box prompting for a post code
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet(nameof(RepresentationCompanyOrIndividualAddressDetails))]
        public async Task<IActionResult> RepresentationCompanyOrIndividualAddressDetails(string returnUrl)
        {
            var response = await _sender.Send(GetRepresentationCompanyOrIndividualAddressDetailsRequest.Request);

            var model = _mapper.Map<PostCodeEntryViewModel>(response);

            var isPrivateSectorSelfFunded = _applicationDataProvider.GetApplicationScheme() == EApplicationScheme.SelfRemediating;
            if (!response.IsRepresentativeUkBased.GetValueOrDefault() && isPrivateSectorSelfFunded)
            {
                return RedirectToAction(nameof(RepresentationCompanyOrIndividualAddressDetailsManual), returnUrl, model.PostCode);
            }

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        /// <summary>
        /// When the user submits their address details for the manual entry details screen
        /// </summary>
        /// <param name="model"></param>
        /// <param name="submitAction"></param>
        /// <returns></returns>
        [HttpPost(nameof(RepresentationCompanyOrIndividualAddressDetails))]
        public async Task<IActionResult> RepresentationCompanyOrIndividualAddressDetails(PostCodeManualViewModel model, ESubmitAction submitAction)
        {

            var isPrivateSectorSelfFunded = _applicationDataProvider.GetApplicationScheme() == EApplicationScheme.SelfRemediating;
            var checkCountry = !model.IsRepresentativeUkBased.HasValue || (model.IsRepresentativeUkBased.HasValue && !model.IsRepresentativeUkBased.Value && isPrivateSectorSelfFunded);
            var validator = new PostCodeManualViewModelValidator(checkCountry);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View("RepresentationCompanyOrIndividualAddressDetailsManual", model);
            }

            var request = _mapper.Map<SetRepresentationCompanyOrIndividualAddressManualDetailsRequest>(model);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var isRasScheme = _applicationDataProvider.GetApplicationScheme() == EApplicationScheme.ResponsibleActorsScheme;
            var action = model.ReturnUrl is null
                ? isRasScheme ? nameof(ResponsibleEntityCompanyType): nameof(ResponsibleEntityRelation)
                : model.ReturnUrl;

            return SafeRedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }

        /// <summary>
        /// Showed when the user selects a post code from the drop down
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="viewModel"></param>
        /// <param name="submitAction"></param>
        /// <returns></returns>
        [HttpPost(nameof(CompanyOrIndAddrDetailsPostCodeItemSelected))]
        public async Task<IActionResult> CompanyOrIndAddrDetailsPostCodeItemSelected(string returnUrl, PostCodeSelectionViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new PostCodeSelectionViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                // need to set these properties on the output model if there is an error
                return View("RepresentationCompanyOrIndividualAddressDetailsResults", viewModel);
            }

            var request = _mapper.Map<SetRepresentationCompanyOrIndividualAddressDetailsRequest>(viewModel);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var isRasScheme = _applicationDataProvider.GetApplicationScheme() == EApplicationScheme.ResponsibleActorsScheme;

            var action = returnUrl is null
                ? isRasScheme ? nameof(ResponsibleEntityCompanyType) : nameof(ResponsibleEntityRelation)
                : returnUrl;

            return SafeRedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }

        /// <summary>
        /// Called when the user enters a post code and hence this controller should ONLY receive
        /// a post code from the user. This takes us to either the manually entry screen or the list of results in a drop down.
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="viewModel"></param>
        /// <param name="submitAction"></param>
        /// <returns></returns>
        [HttpGet(nameof(CompanyOrIndAddrDetailsPostCodeItemEntered))]
        public async Task<IActionResult> CompanyOrIndAddrDetailsPostCodeItemEntered(string returnUrl, PostCodeEntryViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new PostCodeEntryViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View("RepresentationCompanyOrIndividualAddressDetails", viewModel);
            }

            if (submitAction == ESubmitAction.FindAddress)
            {
                var request = new GetPostCodeRequest { PostCode = viewModel.PostCode };
                var response = await _sender.Send(request);
                var newMappedModel = _mapper.Map<PostCodeSelectionViewModel>(response);

                if (!newMappedModel.HaveResults)
                {                    
                    var manualViewModel = _mapper.Map<PostCodeManualViewModel>(response);
                    manualViewModel.Postcode = viewModel.PostCode;
                    var getCountriesResponse = await _sender.Send(GetCountriesRequest.Request);
                    manualViewModel.Countries = getCountriesResponse.Countries;
                    return View("RepresentationCompanyOrIndividualAddressDetailsManual", manualViewModel);
                }
                return View("RepresentationCompanyOrIndividualAddressDetailsResults", newMappedModel);
            }

            return View("RepresentationCompanyOrIndividualAddressDetails", viewModel);
        }

        /// <summary>
        /// Shows a company address entry manual entry screen
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="postCode"></param>
        /// <returns></returns>
        [HttpGet(nameof(RepresentationCompanyOrIndividualAddressDetailsManual))]
        public async Task<IActionResult> RepresentationCompanyOrIndividualAddressDetailsManual(string returnUrl, string postCode)
        {
            var response = await _sender.Send(GetRepresentationCompanyOrIndividualAddressDetailsRequest.Request);
            var model = _mapper.Map<PostCodeManualViewModel>(response);
            return View(model);
        }

        #endregion

        #region ResponsibleEntityCompanyType
        [HttpGet(nameof(ResponsibleEntityCompanyType))]
        public async Task<IActionResult> ResponsibleEntityCompanyType(string returnUrl)
        {
            if (TempData.ContainsKey("CompanyOrIndividual"))
            {                
                ViewData["CompanyOrIndividual"] = TempData["CompanyOrIndividual"];
                TempData["CompanyOrIndividual"] = ViewData["CompanyOrIndividual"];
            }

            var response = await _sender.Send(GetResponsibleEntityCompanyTypeRequest.Request);

            var model = _mapper.Map<ResponsibleEntityCompanyTypeViewModel>(response);

            model.ApplicationScheme = _applicationDataProvider.GetApplicationScheme();
            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(ResponsibleEntityCompanyType))]
        public async Task<IActionResult> ResponsibleEntityCompanyType(ResponsibleEntityCompanyTypeViewModel model)
        {
            var validator = new ResponsibleEntityCompanyTypeViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var applicationScheme = _applicationDataProvider.GetApplicationScheme();
            var isSocialSectorScheme = applicationScheme == EApplicationScheme.SocialSector;

            if (isSocialSectorScheme)
            {
                _ = await _sender.Send(new SetRepresentativeTypeRequest { RepresentativeType = EApplicationRepresentationType.ResponsibleEntity });
                _ = await _sender.Send(new SetRepCompanyOrIndividualRequest { ReponsibleEntityType = EResponsibleEntityType.Company });
            }

            var request = _mapper.Map<SetResponsibleEntityCompanyTypeRequest>(model);
            await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            if (model.ReturnUrl is not null)
            {
                return SafeRedirectToAction(model.ReturnUrl, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

            if (applicationScheme == EApplicationScheme.ResponsibleActorsScheme)
            {
                return RedirectToAction("ResponsibleEntityCompanyRelationDetails", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

            switch (model.OrganisationType!.Value)
            {
                case EApplicationResponsibleEntityOrganisationType.RightToManageCompany:
                    return RedirectToAction("AcquiredRightToManage", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
                case EApplicationResponsibleEntityOrganisationType.PrivateCompany:
                case EApplicationResponsibleEntityOrganisationType.ResidentLedOrganisation:
                case EApplicationResponsibleEntityOrganisationType.RegisteredProvider:
                case EApplicationResponsibleEntityOrganisationType.LocalAuthority:
                    return RedirectToAction("ResponsibleEntityUkRegistered", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
                case EApplicationResponsibleEntityOrganisationType.Other:
                    return RedirectToAction("ResponsibleEntityCompanySubType");
                default:
                    return RedirectToAction("ResponsibleEntityUkRegistered", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }
        }
        #endregion

        #region ResponsibleEntityCompanyRelationDetails
        [HttpGet(nameof(ResponsibleEntityCompanyRelationDetails))]
        public async Task<IActionResult> ResponsibleEntityCompanyRelationDetails(string returnUrl)
        {
            var response = await _sender.Send(GetResponsibleEntityCompanyRelationDetailsRequest.Request);
            var model = _mapper.Map<ResponsibleEntityCompanyRelationDetailsViewModel>(response);
            model.ReturnUrl = returnUrl;
            return View(model);
        }
        [HttpPost(nameof(ResponsibleEntityCompanyRelationDetails))]
        public async Task<IActionResult> ResponsibleEntityCompanyRelationDetails(ResponsibleEntityCompanyRelationDetailsViewModel model)
        {
            var applicationScheme = _applicationDataProvider.GetApplicationScheme();
            if (applicationScheme == EApplicationScheme.ResponsibleActorsScheme)
            {
                var validator = new ResponsibleEntityCompanyRelationDetailsViewModelValidator();
                var validationResult = await validator.ValidateAsync(model);
                if (!validationResult.IsValid)
                {
                    validationResult.AddToModelState(ModelState, string.Empty);
                    return View(model);
                }
                var request = _mapper.Map<SetResponsibleEntityCompanyRelationDetailsRequest>(model);
                await _sender.Send(request);
                if (model.SubmitAction == ESubmitAction.Exit)
                {
                    return RedirectToAction("Index", "TaskList", new { Area = "Application" });
                }
                if (model.ReturnUrl is not null)
                {
                    return SafeRedirectToAction(model.ReturnUrl, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
                }

                return RedirectToAction("ResponsibleEntityCompanyDetails", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }
            else
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }
        }
        #endregion

        #region ResponsibleEntityCompanySubType

        [HttpGet(nameof(ResponsibleEntityCompanySubType))]
        public async Task<IActionResult> ResponsibleEntityCompanySubType(string returnUrl)
        {            
            var response = await _sender.Send(GetResponsibleEntityCompanySubTypeRequest.Request);

            var model = _mapper.Map<ResponsibleEntityCompanySubTypeViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(ResponsibleEntityCompanySubType))]
        public async Task<IActionResult> ResponsibleEntityCompanySubType(ResponsibleEntityCompanySubTypeViewModel model)
        {
            var validator = new ResponsibleEntityCompanySubTypeViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetResponsibleEntityCompanySubTypeRequest>(model);
            await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            if (model.ReturnUrl is not null)
            {
                return SafeRedirectToAction(model.ReturnUrl, null, null);
            }

            return RedirectToAction("ResponsibleEntityUkRegistered");
        }

        #endregion

        #region ResponsibleEntityRelation
        [HttpGet(nameof(ResponsibleEntityRelation))]
        public async Task<IActionResult> ResponsibleEntityRelation(string returnUrl)
        {
            var response = await _sender.Send(GetResponsibleEntityRelationRequest.Request);

            var model = _mapper.Map<ResponsibleEntityRelationViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(ResponsibleEntityRelation))]
        public async Task<IActionResult> ResponsibleEntityRelation(ResponsibleEntityRelationViewModel model, ESubmitAction submitAction)
        {
            var validator = new ResponsibleEntityRelationViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetResponsibleEntityRelationRequest>(model);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = request!.ResponsibleEntityRelation == EResponsibleEntityRelation.Freeholder
                ? nameof(ResponsibleEntityCompanyType)
                : nameof(FreeholderCompanyOrIndividual);

            action = model.ReturnUrl is null
                ? action
                : model.ReturnUrl;

            return SafeRedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }
        #endregion

        #region ResponsibleEntityUkRegistered

        [HttpGet(nameof(ResponsibleEntityUkRegistered))]
        public async Task<IActionResult> ResponsibleEntityUkRegistered(string returnUrl)
        {
            var response = await _sender.Send(GetResponsibleEntityUkRegisteredRequest.Request);

            var model = _mapper.Map<ResponsibleEntityUkRegisteredViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(ResponsibleEntityUkRegistered))]
        public async Task<IActionResult> ResponsibleEntityUkRegistered(ResponsibleEntityUkRegisteredViewModel model)
        {
            var validator = new ResponsibleEntityUkRegisteredViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetResponsibleEntityUkRegisteredRequest>(model);

            var response = await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            if (model.ReturnUrl is not null)
            {
                return SafeRedirectToAction(model.ReturnUrl, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

            var isUkBased = model.UkRegistered!.Value || response is { HasRepresentative: true, HasRepresentativeUkBased: true };
            var isCladdingSafetyScheme = _applicationDataProvider.GetApplicationScheme() == EApplicationScheme.CladdingSafetyScheme;

            if(!isUkBased && isCladdingSafetyScheme)
            {
                return RedirectToAction("NotEligible", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

            if (response is { HasValidOrganisationTypes: true }) 
            {
                // individual applicant
                return RedirectToAction("ResponsibleEntityPrimaryContactDetails", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            } 
            else
            {
                if (response.OrganisationType is EApplicationResponsibleEntityOrganisationType.LocalAuthority or EApplicationResponsibleEntityOrganisationType.RegisteredProvider)
                {
                    // company
                    return RedirectToAction("ResponsibleEntityOrganisationDetails", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
                } 
                else
                {
                    // organisation
                    return RedirectToAction("ResponsibleEntityCompanyDetails", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
                }
            }
        }
        #endregion

        #region ResponsibleEntityCompanyDetails

        [HttpGet(nameof(ResponsibleEntityCompanyDetails))]
        public async Task<IActionResult> ResponsibleEntityCompanyDetails(string returnUrl)
        {
            var response = await _sender.Send(GetResponsibleEntityCompanyDetailsRequest.Request);

            var model = _mapper.Map<ResponsibleEntityCompanyDetailsViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(ResponsibleEntityCompanyDetails))]
        public async Task<IActionResult> ResponsibleEntityCompanyDetails(ResponsibleEntityCompanyDetailsViewModel model, ESubmitAction submitAction)
        {
            var validator = new ResponsibleEntityCompanyDetailsViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetResponsibleEntityCompanyDetailsRequest>(model);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var addressAction = model.IsUkBased ? nameof(ResponsibleEntityCompanyAddress)
                : nameof(ResponsibleEntityCompanyAddressManual);

            var action = model.ReturnUrl is null
                ? addressAction
                : model.ReturnUrl;

            return SafeRedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities", returnUrl = nameof(ResponsibleEntityCompanyDetails) });
        }
        #endregion

        #region ResponsibleEntityCompanyDetails

        [HttpGet(nameof(ResponsibleEntityOrganisationDetails))]
        public async Task<IActionResult> ResponsibleEntityOrganisationDetails(string returnUrl)
        {
            var response = await _sender.Send(GetResponsibleEntityOrganisationDetailsRequest.Request);

            var model = _mapper.Map<ResponsibleEntityOrganisationViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(ResponsibleEntityOrganisationDetails))]
        public async Task<IActionResult> ResponsibleEntityOrganisationDetails(ResponsibleEntityOrganisationViewModel model, ESubmitAction submitAction)
        {
            var validator = new ResponsibleEntityOrganisationViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetResponsibleEntityOrganisationDetailsRequest>(model);
            var response = await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = model.ReturnUrl is null
                ? !response.IsUkRegistered.HasValue || (response.IsUkRegistered.HasValue && response.IsUkRegistered.Value) ? nameof(ResponsibleEntityCompanyAddress) : nameof(ResponsibleEntityCompanyAddressManual)
                : model.ReturnUrl;

            return SafeRedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities", returnUrl = nameof(ResponsibleEntityOrganisationDetails) });
        }
        #endregion

        #region ResponsibleEntityCompanyAddress


        [HttpGet(nameof(ResponsibleEntityCompanyAddress))]
        public async Task<IActionResult> ResponsibleEntityCompanyAddress(string returnUrl)
        {
            var response = await _sender.Send(GetResponsibleEntityCompanyAddressRequest.Request);

            var model = _mapper.Map<PostCodeEntryViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(ResponsibleEntityCompanyAddress))]
        public async Task<IActionResult> ResponsibleEntityCompanyAddress(PostCodeManualViewModel model, ESubmitAction submitAction)
        {
            var isRasScheme = _applicationDataProvider.GetApplicationScheme() == EApplicationScheme.ResponsibleActorsScheme;

            var validator = new PostCodeManualViewModelValidator(true);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View("ResponsibleEntityCompanyAddressManual", model);
            }

            var request = _mapper.Map<SetResponsibleEntityCompanyAddressManualRequest>(model);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = model.ReturnUrl is null
                ? (isRasScheme ? nameof(CheckYourAnswers) : nameof(ResponsibleEntityPrimaryContactDetails))                            
                : model.ReturnUrl;

            return SafeRedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" }); //Should redirect to story 48098 when implemented
        }

        [HttpPost(nameof(RespEntityCompAddrPostCodeItemSelected))]
        public async Task<IActionResult> RespEntityCompAddrPostCodeItemSelected(string returnUrl, PostCodeSelectionViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new PostCodeSelectionViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                // need to set these properties on the output model if there is an error
                return View("ResponsibleEntityCompanyAddressResults", viewModel);
            }

            var request = _mapper.Map<SetResponsibleEntityCompanyAddressRequest>(viewModel);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = returnUrl is null
                ? nameof(ResponsibleEntityPrimaryContactDetails)
                : returnUrl;

            return SafeRedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }

        [HttpGet(nameof(RespEntityCompAddrPostCodeItemEntered))]
        public async Task<IActionResult> RespEntityCompAddrPostCodeItemEntered(string returnUrl, PostCodeEntryViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new PostCodeEntryViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View("ResponsibleEntityCompanyAddress", viewModel);
            }

            if (submitAction == ESubmitAction.FindAddress)
            {
                var request = new GetPostCodeRequest { PostCode = viewModel.PostCode };
                var response = await _sender.Send(request);
                var newMappedModel = _mapper.Map<PostCodeSelectionViewModel>(response);

                if (!newMappedModel.HaveResults)
                {
                    return RedirectToAction("ResponsibleEntityCompanyAddressManual", new { postCode = viewModel.PostCode });
                }
                return View("ResponsibleEntityCompanyAddressResults", newMappedModel);
            }

            return View("ResponsibleEntityCompanyAddress", viewModel);
        }

        [HttpGet(nameof(ResponsibleEntityCompanyAddressManual))]
        public async Task<IActionResult> ResponsibleEntityCompanyAddressManual(string returnUrl, string postCode)
        {
            var response = await _sender.Send(GetResponsibleEntityCompanyAddressRequest.Request);
            var model = _mapper.Map<PostCodeManualViewModel>(response);
            return View(model);
        }

        #endregion

        #region ResponsibleEntityPrimaryContactDetails
        [HttpGet(nameof(ResponsibleEntityPrimaryContactDetails))]
        public async Task<IActionResult> ResponsibleEntityPrimaryContactDetails(string returnUrl)
        {
            var response = await _sender.Send(GetResponsibleEntityPrimaryContactDetailsRequest.Request);

            var model = _mapper.Map<ResponsibleEntityPrimaryContactDetailsViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(ResponsibleEntityPrimaryContactDetails))]
        public async Task<IActionResult> ResponsibleEntityPrimaryContactDetails(ResponsibleEntityPrimaryContactDetailsViewModel model, ESubmitAction submitAction)
        {
            var validator = new ResponsibleEntityPrimaryContactDetailsViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetResponsibleEntityPrimaryContactDetailsRequest>(model);
            var response = await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var applicationScheme = _applicationDataProvider.GetApplicationScheme();
            var isSelfFunded = applicationScheme != EApplicationScheme.CladdingSafetyScheme;
            var isSocialSector = applicationScheme == EApplicationScheme.SocialSector;

            var isRepresentative = response.RepresentationType == EApplicationRepresentationType.Representative;

            string action;
            if (!string.IsNullOrEmpty(model.ReturnUrl))
            {
                action = model.ReturnUrl;
            }
            else if (isSelfFunded && !isSocialSector)
            {
                return SafeRedirectToAction(nameof(UploadEvidence), "ResponsibleEntities", new { Area = "ResponsibleEntities", uploadType = EResponsibleEntityUploadType.Represent, isRepresentative });
            }
            else if (isRepresentative)
            {
                action = nameof(UploadRepresentEvidence);
            }
            else if (isSocialSector || response.OrganisationType is EApplicationResponsibleEntityOrganisationType.LocalAuthority or EApplicationResponsibleEntityOrganisationType.RegisteredProvider)
            {
                action = nameof(LeaseholderOrPrivateOwner);
            }
            else
            {
                action = nameof(ResponsibleEntityResponsibleForGrantFunding);
            }
            
            return SafeRedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" }); //Should navigate to story 48101 - UploadEvidenceOfAuthorisation
        }
        #endregion

        #region FreeholderCompanyOrIndividual
        [HttpGet(nameof(FreeholderCompanyOrIndividual))]
        public async Task<IActionResult> FreeholderCompanyOrIndividual(string returnUrl)
        {
            var response = await _sender.Send(GetFreeholderCompanyOrIndividualRequest.Request);

            var model = _mapper.Map<FreeholderCompanyOrIndividualViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(FreeholderCompanyOrIndividual))]
        public async Task<IActionResult> FreeholderCompanyOrIndividual(FreeholderCompanyOrIndividualViewModel model, ESubmitAction submitAction)
        {
            var validator = new FreeholderCompanyOrIndividualViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetFreeholderCompanyOrIndividualRequest>(model);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = model.ReponsibleEntityType == EResponsibleEntityType.Individual
                ? nameof(FreeholderIndividualDetails)
                : nameof(FreeholderCompanyDetails);

            action = model.ReturnUrl is null
                ? action
                : model.ReturnUrl;

            return SafeRedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }
        #endregion

        #region FreeholderCompanyDetails
        [HttpGet(nameof(FreeholderCompanyDetails))]
        public async Task<IActionResult> FreeholderCompanyDetails(string returnUrl)
        {
            var response = await _sender.Send(GetFreeholderCompanyDetailsRequest.Request);

            var model = _mapper.Map<FreeholderCompanyDetailsViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(FreeholderCompanyDetails))]
        public async Task<IActionResult> FreeholderCompanyDetails(FreeholderCompanyDetailsViewModel model, ESubmitAction submitAction)
        {
            var validator = new FreeholderCompanyDetailsViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetFreeholderCompanyDetailsRequest>(model);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = model.ReturnUrl is null
                ? nameof(FreeholderCompanyAddress)
                : model.ReturnUrl;

            return SafeRedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }
        #endregion

        #region FreeholderIndividualDetails
        [HttpGet(nameof(FreeholderIndividualDetails))]
        public async Task<IActionResult> FreeholderIndividualDetails(string returnUrl)
        {
            var response = await _sender.Send(GetFreeholderIndividualDetailsRequest.Request);

            var model = _mapper.Map<FreeholderIndividualDetailsViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(FreeholderIndividualDetails))]
        public async Task<IActionResult> FreeholderIndividualDetails(FreeholderIndividualDetailsViewModel model, ESubmitAction submitAction)
        {
            var validator = new FreeholderIndividualDetailsViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetFreeholderIndividualDetailsRequest>(model);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = model.ReturnUrl is null
                ? nameof(FreeholderIndividualAddress)
                : model.ReturnUrl;

            return SafeRedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }
        #endregion

        #region FreeholderCompanyAddress
        [HttpGet(nameof(FreeholderCompanyAddress))]
        public async Task<IActionResult> FreeholderCompanyAddress(string returnUrl)
        {
            var response = await _sender.Send(GetFreeholderAddressRequest.Request);

            var model = _mapper.Map<PostCodeEntryViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(FreeholderCompanyAddress))]
        public async Task<IActionResult> FreeholderCompanyAddress(PostCodeManualViewModel model, ESubmitAction submitAction)
        {
            var validator = new PostCodeManualViewModelValidator(false);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View("FreeholderCompanyAddressManual", model);
            }

            var request = _mapper.Map<SetFreeholderAddressManualRequest>(model);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            TempData["CompanyOrIndividual"] = "Company";

            var action = model.ReturnUrl is null
                ? nameof(ResponsibleEntityCompanyType)
                : model.ReturnUrl;

            return SafeRedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }

        [HttpPost(nameof(FreeholdCompAddrPostCodeItemSelected))]
        public async Task<IActionResult> FreeholdCompAddrPostCodeItemSelected(string returnUrl, PostCodeSelectionViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new PostCodeSelectionViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                // need to set these properties on the output model if there is an error
                return View("FreeholderCompanyAddressResults", viewModel);
            }

            var request = _mapper.Map<SetFreeholderAddressRequest>(viewModel);
            await _sender.Send(request);
            
            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            TempData["CompanyOrIndividual"] = "Individual";

            var action = returnUrl is null
                ? nameof(ResponsibleEntityCompanyType)
                : returnUrl;

            return SafeRedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }

        [HttpGet(nameof(FreeholdCompAddrPostCodeItemEntered))]
        public async Task<IActionResult> FreeholdCompAddrPostCodeItemEntered(string returnUrl, PostCodeEntryViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new PostCodeEntryViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View("FreeholderCompanyAddress", viewModel);
            }

            if (submitAction == ESubmitAction.FindAddress)
            {
                var request = new GetPostCodeRequest { PostCode = viewModel.PostCode };
                var response = await _sender.Send(request);
                var newMappedModel = _mapper.Map<PostCodeSelectionViewModel>(response);

                if (!newMappedModel.HaveResults)
                {
                    var manualViewModel = _mapper.Map<PostCodeManualViewModel>(response);
                    manualViewModel.Postcode = viewModel.PostCode;
                    return View("FreeholderCompanyAddressManual", manualViewModel);
                }
                return View("FreeholderCompanyAddressResults", newMappedModel);
            }

            return View("FreeholderCompanyAddress", viewModel);
        }

        [HttpGet(nameof(FreeholderCompanyAddressManual))]
        public async Task<IActionResult> FreeholderCompanyAddressManual(string returnUrl, string postCode)
        {
            var response = await _sender.Send(GetFreeholderAddressRequest.Request);
            var model = _mapper.Map<PostCodeManualViewModel>(response);
            return View(model);
        }

        #endregion

        #region FreeholderIndividualAddress
        [HttpGet(nameof(FreeholderIndividualAddress))]
        public async Task<IActionResult> FreeholderIndividualAddress()
        {
            var response = await _sender.Send(GetFreeholderAddressRequest.Request);

            var model = _mapper.Map<PostCodeEntryViewModel>(response);

            return View(model);
        }

        [HttpPost(nameof(FreeholderIndividualAddress))]
        public async Task<IActionResult> FreeholderIndividualAddress(PostCodeManualViewModel model, ESubmitAction submitAction)
        {
            var validator = new PostCodeManualViewModelValidator(false);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View("FreeholderIndividualAddressManual", model);
            }

            var request = _mapper.Map<SetFreeholderAddressRequest>(model);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            TempData["CompanyOrIndividual"] = "Individual";

            var action = model.ReturnUrl is null
                ? nameof(ResponsibleEntityCompanyType)
                : model.ReturnUrl;

            return SafeRedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }

        [HttpPost(nameof(FreeholdIndAddrPostCodeItemSelected))]
        public async Task<IActionResult> FreeholdIndAddrPostCodeItemSelected(string returnUrl, PostCodeSelectionViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new PostCodeSelectionViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                // need to set these properties on the output model if there is an error
                return View("FreeholderIndividualAddressResults", viewModel);
            }

            var request = _mapper.Map<SetFreeholderAddressRequest>(viewModel);
            await _sender.Send(request);
            
            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            TempData["CompanyOrIndividual"] = "Individual";

            var action = returnUrl is null
                ? nameof(ResponsibleEntityCompanyType)
                : returnUrl;

            return SafeRedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }

        [HttpGet(nameof(FreeholdIndAddrPostCodeItemEntered))]
        public async Task<IActionResult> FreeholdIndAddrPostCodeItemEntered(string returnUrl, PostCodeEntryViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new PostCodeEntryViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View("FreeholderIndividualAddress", viewModel);
            }

            if (submitAction == ESubmitAction.FindAddress)
            {
                var request = new GetPostCodeRequest { PostCode = viewModel.PostCode };
                var response = await _sender.Send(request);
                var newMappedModel = _mapper.Map<PostCodeSelectionViewModel>(response);

                if (!newMappedModel.HaveResults)
                {
                    var manualViewModel = _mapper.Map<PostCodeManualViewModel>(response);
                    manualViewModel.Postcode = viewModel.PostCode;
                    return View("FreeholderIndividualAddressManual", manualViewModel);
                }
                return View("FreeholderIndividualAddressResults", newMappedModel);
            }

            return View("FreeholderIndividualAddress", viewModel);
        }
        
        [HttpGet(nameof(FreeholderIndividualAddressManual))]
        public async Task<IActionResult> FreeholderIndividualAddressManual(string returnUrl, string postCode)
        {
            var response = await _sender.Send(GetFreeholderAddressRequest.Request);
            var model = _mapper.Map<PostCodeManualViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(FreeholderIndividualAddressManual))]
        public async Task<IActionResult> FreeholderIndividualAddressManual(PostCodeManualViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new PostCodeManualViewModelValidator(false);
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View(viewModel);
            }
            
            var request = _mapper.Map<SetFreeholderAddressManualRequest>(viewModel);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            TempData["CompanyOrIndividual"] = "Individual";

            var action = viewModel.ReturnUrl is null
                ? nameof(ResponsibleEntityCompanyType)
                : viewModel.ReturnUrl;

            return SafeRedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }
      
        #endregion

        #region LeaseholderOrPrivateOwner

        [HttpGet(nameof(LeaseholderOrPrivateOwner))]
        public async Task<IActionResult> LeaseholderOrPrivateOwner(string returnUrl)
        {
            var response = await _sender.Send(GetLeaseholderOrPrivateOwnerRequest.Request);

            var model = _mapper.Map<LeaseholderOrPrivateOwnerViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(LeaseholderOrPrivateOwner))]
        public async Task<IActionResult> LeaseholderOrPrivateOwner(LeaseholderOrPrivateOwnerViewModel model)
        {
            var validator = new LeaseholderOrPrivateOwnerViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetLeaseholderOrPrivateOwnerRequest>(model);
            await _sender.Send(request);

            // get organisation type
            var organisationTypeResponse = await _sender.Send(GetResponsibleEntityCompanyTypeRequest.Request);
            var defaultOrgType = new GetResponsibleEntityCompanyTypeResponse() { OrganisationType = EApplicationResponsibleEntityOrganisationType.Other };
            var organisationType = (organisationTypeResponse ?? defaultOrgType).OrganisationType;

            // set conditionals
            var applicationScheme = _applicationDataProvider.GetApplicationScheme();
            var isCss = applicationScheme == EApplicationScheme.CladdingSafetyScheme;
            var isSocialSector = applicationScheme == EApplicationScheme.SocialSector;
            var isRegisteredProvider = organisationType == EApplicationResponsibleEntityOrganisationType.RegisteredProvider;
            var isLocalAuthority = organisationType == EApplicationResponsibleEntityOrganisationType.LocalAuthority;

            if(model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            if(model.ReturnUrl != null)
            {
                return SafeRedirectToAction(model.ReturnUrl);
            }

            if(isCss && (isRegisteredProvider || isLocalAuthority))
            {
                return RedirectToAction(nameof(ResponsibleEntityResponsibleForGrantFunding));
            }

            if (isSocialSector)
            {
                return RedirectToAction(nameof(CheckYourAnswers));
            }

            return RedirectToAction(nameof(ClaimingGrant));
        }
        #endregion

        #region ClaimingGrant
        [HttpGet(nameof(ClaimingGrant))]
        public async Task<IActionResult> ClaimingGrant(string returnUrl)
        {
            var response = await _sender.Send(GetIsClaimingGrantRequest.Request);

            var model = _mapper.Map<ClaimingGrantViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(ClaimingGrant))]
        public async Task<IActionResult> ClaimingGrant(ClaimingGrantViewModel model)
        {
            var validator = new ClaimingGrantViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetIsClaimingGrantRequest>(model);
            var response = await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = model.ReturnUrl is null
                ? nameof(ConfirmedNotViable)
                : model.ReturnUrl;

            if (model.IsClaimingGrant == true &&
                response.CompanyType is EApplicationResponsibleEntityOrganisationType.RegisteredProvider or EApplicationResponsibleEntityOrganisationType.LocalAuthority)
            {
                return SafeRedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }
            return RedirectToAction("ResponsibleEntityResponsibleForGrantFunding", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }
        #endregion

        #region ResponsibleForGrantFunding
        [HttpGet(nameof(ResponsibleEntityResponsibleForGrantFunding))]
        public async Task<IActionResult> ResponsibleEntityResponsibleForGrantFunding(string returnUrl)
        {
            var response = await _sender.Send(GetResponsibleEntityResponsibleForGrantFundingRequest.Request);

            var model = _mapper.Map<ResponsibleEntityResponsibleForGrantFundingViewModel>(response);

            model.ReturnUrl = returnUrl;
            ViewData["BackLink"] = GetBackLinkForResponsibleEntityResponsibleForGrantFunding(model);

            return View(model);
        }

        [HttpPost(nameof(ResponsibleEntityResponsibleForGrantFunding))]
        public async Task<IActionResult> ResponsibleEntityResponsibleForGrantFunding(ResponsibleEntityResponsibleForGrantFundingViewModel model)
        {
            var validator = new ResponsibleEntityResponsibleForGrantFundingViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                ViewData["BackLink"] = GetBackLinkForResponsibleEntityResponsibleForGrantFunding(model);
                return View(model);
            }

            var request = _mapper.Map<SetResponsibleEntityResponsibleForGrantFundingRequest>(model);
            var response = await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            if (model is { ResponsibleForGrantFunding: true } || response.SignatoriesAlreadyExist)
            {
                return RedirectToAction("GrantFundingSignatories", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }
            else
            {
                return RedirectToAction("GrantFundingSignatoryDetails", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }
        }

        private string GetBackLinkForResponsibleEntityResponsibleForGrantFunding(ResponsibleEntityResponsibleForGrantFundingViewModel model)
        {
            if (model is { IsClaimingGrant: false, HasOwners: true })
            {
                return Url.Action("ClaimingGrant", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

            if (model.RepresentationType == EApplicationRepresentationType.ResponsibleEntity
                && model.OrganisationType != EApplicationResponsibleEntityOrganisationType.LocalAuthority
                && model.OrganisationType != EApplicationResponsibleEntityOrganisationType.RegisteredProvider)
            {
                return Url.Action("ResponsibleEntityPrimaryContactDetails", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

            if (model.OrganisationType is EApplicationResponsibleEntityOrganisationType.LocalAuthority or EApplicationResponsibleEntityOrganisationType.RegisteredProvider)
            {
                return Url.Action("LeaseholderOrPrivateOwner", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

            if (model.RepresentationType == EApplicationRepresentationType.Representative)
            {
                return Url.Action("UploadRepresentEvidence", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

            return Url.Action("Index", "TaskList", new { Area = "Application" });
        }
        #endregion

        #region GrantFundingSignatories
        [HttpGet(nameof(GrantFundingSignatories))]
        public async Task<IActionResult> GrantFundingSignatories(string returnUrl)
        {
            var response = await _sender.Send(GetGrantFundingSignatoriesRequest.Request);

            var signatories = _mapper.Map<List<GrantFundingSignatoryViewModel>>(response);
            var model = new GrantFundingSignatoriesViewModel
            {
                GrantFundingSignatories = signatories,
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost(nameof(GrantFundingSignatories))]
        public async Task<IActionResult> GrantFundingSignatories(GrantFundingSignatoriesViewModel model)
        {
            var validator = new GrantFundingSignatoriesViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            return RedirectToAction("CheckYourAnswers", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }

        [HttpGet(nameof(AddGrantFundingSignatory))]
        public IActionResult AddGrantFundingSignatory(GrantFundingSignatoriesViewModel model)
        {
            return RedirectToAction("GrantFundingSignatoryDetails", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }

        [HttpGet(nameof(RemoveGrantFundingSignatory))]
        public async Task<IActionResult> RemoveGrantFundingSignatory(Guid grantFundingSignatoryId)
        {
            await _sender.Send(new DeleteGrantFundingSignatoryRequest { GrantFundingSignatoryId = grantFundingSignatoryId });

            return RedirectToAction("GrantFundingSignatories", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }

        #endregion

        #region GrantFundingSignatoryDetails
        [HttpGet(nameof(GrantFundingSignatoryDetails))]
        public async Task<IActionResult> GrantFundingSignatoryDetails(Guid? grantFundingSignatoryId, string returnUrl)
        {
            var response = await _sender.Send(new GetGrantFundingSignatoryDetailsRequest { GrantFundingSignatoryId = grantFundingSignatoryId });

            var model = _mapper.Map<GrantFundingSignatoryDetailsViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(GrantFundingSignatoryDetails))]
        public async Task<IActionResult> GrantFundingSignatoryDetails(GrantFundingSignatoryDetailsViewModel model)
        {
            var validator = new GrantFundingSignatoryDetailsViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetGrantFundingSignatoryDetailsRequest>(model);
            await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            return RedirectToAction("GrantFundingSignatories", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }
        #endregion

        #region ConfirmedNotViable

        [HttpGet(nameof(ConfirmedNotViable))]
        public async Task<IActionResult> ConfirmedNotViable(string returnUrl)
        {
            var response = await _sender.Send(GetConfirmedNotViableRequest.Request);

            var model = _mapper.Map<ConfirmedNotViableViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(ConfirmedNotViable))]
        public async Task<IActionResult> ConfirmedNotViable(ConfirmedNotViableViewModel model)
        {
            var validation = new ConfirmedNotViableViewModelValidator();
            var validationResult = await validation.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetConfirmedNotViableRequest>(model);
            await _sender.Send(request);

            if (!model.IsConfirmedNotViable!.Value)
            {
                if(model.SubmitAction == ESubmitAction.Continue)
                {
                    return model.OrganisationType switch
                    {
                        EApplicationResponsibleEntityOrganisationType.LocalAuthority => View("ConfirmedNotViableNotOfficer"),
                        EApplicationResponsibleEntityOrganisationType.RegisteredProvider => View("ConfirmedNotViableNotChiefExecutive"),
                        _ => NotFound()
                    };
                }

                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = model.ReturnUrl is null
                ? nameof(UploadEvidence)
                : model.ReturnUrl;

            return SafeRedirectToAction(action, "ResponsibleEntities",
                new { Area = "ResponsibleEntities", UploadType = model.OrganisationType == EApplicationResponsibleEntityOrganisationType.LocalAuthority ? EResponsibleEntityUploadType.S151 : EResponsibleEntityUploadType.ChiefExec });
        }
        #endregion

        #region Upload Represent Evidence
        [HttpGet(nameof(UploadRepresentEvidence))]
        public async Task<IActionResult> UploadRepresentEvidence(string returnUrl)
        {
            var response = await _sender.Send(new GetUploadResponsibleEntitiesEvidenceRequest { UploadType = EResponsibleEntityUploadType.Represent });

            var model = _mapper.Map<UploadEvidenceViewModel>(response);

            model.ReturnUrl = returnUrl ?? nameof(UploadRepresentEvidence);
            model.UploadType = EResponsibleEntityUploadType.Represent;
            model.DeleteParameters = new Dictionary<string, string>
            {
                {"returnUrl", model.ReturnUrl }
            };

            return View(model);
        }

        [HttpPost(nameof(UploadRepresentEvidence))]
        [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
        public async Task<IActionResult> UploadRepresentEvidence(UploadEvidenceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // this will happen when the request size limit is exceeded, the model is null so manually add the error message
                ModelState.AddModelError("Files", "One more more files are larger than 20mb");
                return View(model);
            }

            var validator = new UploadEvidenceViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            if (model.SubmitAction == ESubmitAction.Continue)
            {
                var applicationScheme = _applicationDataProvider.GetApplicationScheme();
                var isNotCssScheme = applicationScheme != EApplicationScheme.CladdingSafetyScheme;
                if (isNotCssScheme)
                {
                    return RedirectToAction("CheckYourAnswers", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
                }

                return model.OrganisationType switch
                {
                    EApplicationResponsibleEntityOrganisationType.LocalAuthority or EApplicationResponsibleEntityOrganisationType.RegisteredProvider => RedirectToAction("LeaseholderOrPrivateOwner", "ResponsibleEntities", new { Area = "ResponsibleEntities" }),
                    _ => RedirectToAction("ResponsibleEntityResponsibleForGrantFunding", "ResponsibleEntities", new { Area = "ResponsibleEntities" }),
                };
            }

            try
            {
                var request = _mapper.Map<SetUploadResponsibleEntitiesEvidenceRequest>(model);
                await _sender.Send(request);
            }
            catch (InvalidFileException ex)
            {
                ModelState.AddModelError(nameof(model.File), ex.Message);
                return View(model);
            }

            return SafeRedirectToAction(model.ReturnUrl, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }
        #endregion

        #region UploadEvidence
        [HttpGet(nameof(UploadEvidence))]
        public async Task<IActionResult> UploadEvidence(string returnUrl, [FromQuery] EResponsibleEntityUploadType uploadType, [FromQuery] bool isRepresentative)
        {
            var response = await _sender.Send(new GetUploadResponsibleEntitiesEvidenceRequest { UploadType = uploadType });
            
            var model = _mapper.Map<UploadEvidenceViewModel>(response);

            model.ReturnUrl = returnUrl;
            model.UploadType = uploadType;
            model.IsRepresentative = isRepresentative;

            model.DeleteParameters = new Dictionary<string, string>
            {
                {"returnUrl", nameof(UploadEvidence) },
                {"uploadType", model.UploadType.ToString() }
            };

            return View(model);
        }

        [HttpPost(nameof(UploadEvidence))]
        [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
        public async Task<IActionResult> UploadEvidence(UploadEvidenceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // this will happen when the request size limit is exceeded, the model is null so manually add the error message
                ModelState.AddModelError(nameof(model.File), "The file is larger than 100mb");
                return View(model);
            }

            var validator = new UploadEvidenceViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            if (model.SubmitAction == ESubmitAction.Continue)
            {
                var applicationScheme = _applicationDataProvider.GetApplicationScheme();
                var isNotCssScheme = applicationScheme != EApplicationScheme.CladdingSafetyScheme;
                if (isNotCssScheme)
                {
                    return RedirectToAction("CheckYourAnswers", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
                }
                        
                return RedirectToAction("ResponsibleEntityResponsibleForGrantFunding", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

            try
            {
                var request = _mapper.Map<SetUploadResponsibleEntitiesEvidenceRequest>(model);
                await _sender.Send(request);
            }
            catch (InvalidFileException ex)
            {
                ModelState.AddModelError(nameof(model.File), ex.Message);
                return View(model);
            }

            return RedirectToAction("UploadEvidence", "ResponsibleEntities", new { Area = "ResponsibleEntities", UploadType = model.UploadType });
        }
        #endregion

        #region UploadEvidenceDelete

        [HttpGet(nameof(UploadEvidence) + "/Delete")]
        public async Task<IActionResult> UploadEvidenceDelete([FromQuery] DeleteResponsibleEntitiesEvidenceRequest request)
        {
            await _sender.Send(request);
            return SafeRedirectToAction(request.ReturnUrl, "ResponsibleEntities", new { Area = "ResponsibleEntities", uploadType = request.UploadType });
        }
        #endregion

        #region CheckYourAnswers
        [HttpGet(nameof(CheckYourAnswers))]
        public async Task<IActionResult> CheckYourAnswers()
        {
            var response = await _sender.Send(GetResponsibleEntityAnswersRequest.Request);
            var model = _mapper.Map<CheckYourAnswersViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(CheckYourAnswers))]
        public async Task<IActionResult> CheckYourAnswers(CheckYourAnswersViewModel model)
        {
            await _sender.Send(SetResponsibleEntityCompleteRequest.Request);
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

            await _sender.Send(new ResetResponsibleEntitiesSectionRequest(), cancellationToken);

            return RedirectToAction("Information");
        }
        #endregion

        #region Right to Manage
        [HttpGet(nameof(AcquiredRightToManage))]
        public async Task<IActionResult> AcquiredRightToManage(CancellationToken cancellationToken)
        {
            var response = await _sender.Send(GetAcquiredRightToManageRequest.Request, cancellationToken);
            var model = _mapper.Map<AcquiredRightToManageViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(AcquiredRightToManage))]
        public async Task<IActionResult> AcquiredRightToManage(AcquiredRightToManageViewModel model, CancellationToken cancellationToken)
        {
            var validator = new AcquiredRightToManageViewModelValidator();

            var validationResult = await validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetAcquiredRightToManageRequest>(model);
            await _sender.Send(request, cancellationToken);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            if (model.HasAcquiredRightToManage == true)
            {
                return RedirectToAction("WhenRightToManageAcquired", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

            return RedirectToAction("NotEligible", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }

        [HttpGet(nameof(WhenRightToManageAcquired))]
        public async Task<IActionResult> WhenRightToManageAcquired(CancellationToken cancellationToken)
        {
            var response = await _sender.Send(GetWhenRightToManageAcquiredRequest.Request, cancellationToken);
            var model = _mapper.Map<WhenRightToManageAcquiredViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(WhenRightToManageAcquired))]
        public async Task<IActionResult> WhenRightToManageAcquired(WhenRightToManageAcquiredViewModel model, CancellationToken cancellationToken)
        {
            var validator = new WhenRightToManageAcquiredViewModelValidator();
            var validationResult = await validator.ValidateAsync(model, cancellationToken);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetWhenRightToManageAcquiredRequest>(model);
            await _sender.Send(request, cancellationToken);


            return model.SubmitAction == ESubmitAction.Continue
                ? RedirectToAction("RightToManageEvidence", "ResponsibleEntities", new { Area = "ResponsibleEntities" })
                : RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }

        [HttpGet(nameof(RightToManageEvidence))]
        public async Task<IActionResult> RightToManageEvidence(CancellationToken cancellationToken)
        {
            var response = await _sender.Send(GetRightToManageEvidenceRequest.Request, cancellationToken);
            var model = _mapper.Map<RightToManageEvidenceViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(RightToManageEvidence))]
        [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
        public async Task<IActionResult> RightToManageEvidence(RightToManageEvidenceViewModel model, CancellationToken cancellationToken)
        {
            var validator = new RightToManageEvidenceViewModelValidator();
            var validationResult = await validator.ValidateAsync(model, cancellationToken);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            if (model.SubmitAction == ESubmitAction.Upload)
            {
                var request = _mapper.Map<AddRightToManageEvidenceRequest>(model);
                await _sender.Send(request, cancellationToken);
                return RedirectToAction("RightToManageEvidence", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

            return model.SubmitAction == ESubmitAction.Continue
                ? RedirectToAction("ResponsibleEntityUkRegistered", "ResponsibleEntities", new { Area = "ResponsibleEntities" })
                : RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }

        [HttpGet(nameof(RightToManageEvidence) + "/Delete")]
        public async Task<IActionResult> DeleteRightToManageEvidence(
            [FromQuery] DeleteRightToManageEvidenceRequest request, 
            CancellationToken cancellationToken)
        {
            await _sender.Send(request, cancellationToken);
            return RedirectToAction("RightToManageEvidence", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }

        #endregion
    }
}