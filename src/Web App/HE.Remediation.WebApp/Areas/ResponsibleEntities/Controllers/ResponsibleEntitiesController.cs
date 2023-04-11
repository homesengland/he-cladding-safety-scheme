using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.FreeholderCompanyOrIndividual.GetFreeholderCompanyOrIndividual;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.FreeholderCompanyOrIndividual.SetFreeholderCompanyOrIndividual;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.NotEligible.SetNotEligible;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepresentationCompanyOrIndividual.GetRepresentationCompanyOrIndividual;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepresentationCompanyOrIndividual.SetRepresentationCompanyOrIndividual;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.Representative.GetRepresentativeType;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.Representative.SetRepresentativeType;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepresentativeBasedInUk.GetRepresentativeBasedInUk;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepresentativeBasedInUk.SetRepresentativeBasedInUk;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanyType.GetResponsibleEntityCompanyType;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanyType.SetResponsibleEntityCompanyType;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityRelation.GetResponsibleEntityRelation;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityRelation.SetResponsibleEntityRelation;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityUkRegistered.GetResponsibleEntityUkRegistered;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityUkRegistered.SetResponsibleEntityUkRegistered;
using HE.Remediation.WebApp.Authorisation;
using HE.Remediation.WebApp.Constants;
using HE.Remediation.WebApp.ViewModels.ResponsibleEntities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.ResponsibleEntities.Controllers
{
    [Area("ResponsibleEntities")]
    [Route("ResponsibleEntities")]
    [CookieApplicationAuthorise]
    public class ResponsibleEntitiesController : Controller
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public ResponsibleEntitiesController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        #region "Start"

        [HttpGet(nameof(Start))]
        public async Task<IActionResult> Start()
        {
            var response = await _sender.Send(GetConfirmedNotViableRequest.Request);

            if (response.IsConfirmedNotViable != null)
            {
                return RedirectToAction("ConfirmedNotViable", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

            return RedirectToAction("Information", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }

        #endregion

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

            if (request.BasedInUk!.Value)
            {
                var action = model.ReturnUrl is null
                ? nameof(RepresentationCompanyOrIndividual)
                : model.ReturnUrl;

                return RedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

            TempData["BackLink"] = Url.Action("BasedInUk", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            return RedirectToAction("NotEligible", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }
        #endregion

        #region NotEligible
        [HttpGet(nameof(NotEligible))]
        public IActionResult NotEligible()
        {
            if (!TempData.ContainsKey("BackLink"))
            {
                return NotFound();
            }

            ViewData["BackLink"] = TempData["BackLink"];
            return View();
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
            var response = await _sender.Send(GetRepresentationCompanyOrIndividualRequest.Request);

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

            var request = _mapper.Map<SetRepresentationCompanyOrIndividualRequest>(model);
            await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = model.ReturnUrl is null
                ? nameof(RepresentationCompanyOrIndividualDetails)
                : model.ReturnUrl;

            return RedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }

        #endregion

        #region RepresentationCompanyOrIndividualDetails
        [HttpGet(nameof(RepresentationCompanyOrIndividualDetails))]
        public async Task<IActionResult> RepresentationCompanyOrIndividualDetails(string returnUrl)
        {
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

            return RedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }
        #endregion

        #region RepresentationCompanyOrIndividualAddressDetails
        [HttpGet(nameof(RepresentationCompanyOrIndividualAddressDetails))]
        public async Task<IActionResult> RepresentationCompanyOrIndividualAddressDetails(string returnUrl)
        {
            var response = await _sender.Send(GetRepresentationCompanyOrIndividualAddressDetailsRequest.Request);

            var model = _mapper.Map<RepresentationCompanyOrIndividualAddressDetailsViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(RepresentationCompanyOrIndividualAddressDetails))]
        public async Task<IActionResult> RepresentationCompanyOrIndividualAddressDetails(RepresentationCompanyOrIndividualAddressDetailsViewModel model)
        {
            var validator = new RepresentationCompanyOrIndividualAddressDetailsViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetRepresentationCompanyOrIndividualAddressDetailsRequest>(model);
            await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = model.ReturnUrl is null
                ? nameof(ResponsibleEntityRelation)
                : model.ReturnUrl;

            return RedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }

        #endregion

        #region ResponsibleEntityCompanyType
        [HttpGet(nameof(ResponsibleEntityCompanyType))]
        public async Task<IActionResult> ResponsibleEntityCompanyType(string returnUrl)
        {
            if (TempData.ContainsKey("CompanyOrIndividual"))
            {
                ViewData["CompanyOrIndividual"] = TempData["CompanyOrIndividual"];
            }

            var response = await _sender.Send(GetResponsibleEntityCompanyTypeRequest.Request);

            var model = _mapper.Map<ResponsibleEntityCompanyTypeViewModel>(response);

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

            var request = _mapper.Map<SetResponsibleEntityCompanyTypeRequest>(model);
            await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            if (model.ReturnUrl is not null)
            {
                return RedirectToAction(model.ReturnUrl, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

            switch (model.OrganisationType!.Value)
            {
                case EApplicationResponsibleEntityOrganisationType.PrivateCompany:
                case EApplicationResponsibleEntityOrganisationType.ResidentLedOrganisation:
                case EApplicationResponsibleEntityOrganisationType.RightToManageCompany:
                case EApplicationResponsibleEntityOrganisationType.Other:
                    return RedirectToAction("ResponsibleEntityUkRegistered", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
                default:
                    return RedirectToAction("LeaseholderOrPrivateOwner", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }
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

            return RedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
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
                return RedirectToAction(model.ReturnUrl, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

            if
            (
                (
                    !model.UkRegistered!.Value &&
                    response is { HasRepresentative: true, HasValidOrganisationTypes: true }
                )
                    ||
                (
                    model.UkRegistered!.Value &&
                    response is { HasValidOrganisationTypes: true }
                )

            )
            {
                return RedirectToAction("ResponsibleEntityPrimaryContactDetails", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

            if (model.UkRegistered!.Value)
            {
                return RedirectToAction("ResponsibleEntityCompanyDetails", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

            TempData["BackLink"] = Url.Action("ResponsibleEntityUkRegistered", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            return RedirectToAction("NotEligible", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
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

            var action = model.ReturnUrl is null
                ? nameof(ResponsibleEntityCompanyAddress)
                : model.ReturnUrl;

            return RedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }
        #endregion

        #region ResponsibleEntityCompanyAddress
        [HttpGet(nameof(ResponsibleEntityCompanyAddress))]
        public async Task<IActionResult> ResponsibleEntityCompanyAddress(string returnUrl)
        {
            var response = await _sender.Send(GetResponsibleEntityCompanyAddressRequest.Request);

            var model = _mapper.Map<ResponsibleEntityCompanyAddressViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(ResponsibleEntityCompanyAddress))]
        public async Task<IActionResult> ResponsibleEntityCompanyAddress(ResponsibleEntityCompanyAddressViewModel model, ESubmitAction submitAction)
        {
            var validator = new ResponsibleEntityCompanyAddressViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetResponsibleEntityCompanyAddressRequest>(model);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = model.ReturnUrl is null
                ? nameof(ResponsibleEntityPrimaryContactDetails)
                : model.ReturnUrl;

            return RedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" }); //Should redirect to story 48098 when implemented
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
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = model.ReturnUrl is null
                ? nameof(UploadEvidence)
                : model.ReturnUrl;

            return RedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" }); //Should navigate to story 48101 - UploadEvidenceOfAuthorisation
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

            return RedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
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

            return RedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
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

            return RedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }
        #endregion

        #region FreeholderCompanyAddress
        [HttpGet(nameof(FreeholderCompanyAddress))]
        public async Task<IActionResult> FreeholderCompanyAddress(string returnUrl)
        {
            var response = await _sender.Send(GetFreeholderAddressRequest.Request);

            var model = _mapper.Map<FreeholderAddressViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(FreeholderCompanyAddress))]
        public async Task<IActionResult> FreeholderCompanyAddress(FreeholderAddressViewModel model, ESubmitAction submitAction)
        {
            var validator = new FreeholderAddressViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetFreeholderAddressRequest>(model);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            TempData["CompanyOrIndividual"] = "Company";

            var action = model.ReturnUrl is null
                ? nameof(ResponsibleEntityCompanyType)
                : model.ReturnUrl;

            return RedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }
        #endregion

        #region FreeholderIndividualAddress
        [HttpGet(nameof(FreeholderIndividualAddress))]
        public async Task<IActionResult> FreeholderIndividualAddress()
        {
            var response = await _sender.Send(GetFreeholderAddressRequest.Request);

            var model = _mapper.Map<FreeholderAddressViewModel>(response);

            return View(model);
        }

        [HttpPost(nameof(FreeholderIndividualAddress))]
        public async Task<IActionResult> FreeholderIndividualAddress(FreeholderAddressViewModel model, ESubmitAction submitAction)
        {
            var validator = new FreeholderAddressViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
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

            return RedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
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
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetLeaseholderOrPrivateOwnerRequest>(model);
            await _sender.Send(request);

            var action = model.ReturnUrl is null
                ? nameof(ClaimingGrant)
                : model.ReturnUrl;

            return model.SubmitAction == ESubmitAction.Exit
                ? RedirectToAction("Index", "TaskList", new { Area = "Application" })
                : RedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
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
                return RedirectToAction(action, "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

            if (model is { IsClaimingGrant: false, HasOwners: true })
            {
                return RedirectToAction("CheckYourAnswers", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

            TempData["BackLink"] = Url.Action("ClaimingGrant", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            return RedirectToAction("NotEligible", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
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

            if (!model.IsConfirmedNotViable!.Value)
            {
                return model.OrganisationType switch
                {
                    EApplicationResponsibleEntityOrganisationType.LocalAuthority => View("ConfirmedNotViableNotOfficer"),
                    EApplicationResponsibleEntityOrganisationType.RegisteredProvider => View("ConfirmedNotViableNotChiefExecutive"),
                    _ => NotFound()
                };
            }

            var request = _mapper.Map<SetConfirmedNotViableRequest>(model);
            await _sender.Send(request);

            if (model.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "TaskList", new { Area = "Application" });
            }

            var action = model.ReturnUrl is null
                ? nameof(UploadEvidence)
                : model.ReturnUrl;

            return RedirectToAction(action, "ResponsibleEntities",
                new { Area = "ResponsibleEntities" });
        }

        [HttpPost(nameof(ConfirmedNotViableNotProceed))]
        public async Task<IActionResult> ConfirmedNotViableNotProceed(ConfirmedNotViableViewModel model)
        {
            var request = _mapper.Map<SetConfirmedNotViableRequest>(model);
            await _sender.Send(request);

            return RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }
        #endregion

        #region UploadEvidence
        [HttpGet(nameof(UploadEvidence))]
        public async Task<IActionResult> UploadEvidence(string returnUrl)
        {
            var response = await _sender.Send(GetUploadResponsibleEntitiesEvidenceRequest.Request);

            var model = _mapper.Map<UploadEvidenceViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(UploadEvidence))]
        [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
        public async Task<IActionResult> UploadEvidence(UploadEvidenceViewModel model)
        {
            if (model.SubmitAction == ESubmitAction.Continue)
            {
                return RedirectToAction("CheckYourAnswers", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
            }

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

            return RedirectToAction("UploadEvidence", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
        }
        #endregion

        #region UploadEvidenceDelete

        [HttpGet(nameof(UploadEvidence) + "/Delete")]
        public async Task<IActionResult> UploadEvidenceDelete([FromQuery] DeleteResponsibleEntitiesEvidenceRequest request)
        {
            await _sender.Send(request);
            return RedirectToAction("UploadEvidence", "ResponsibleEntities", new { Area = "ResponsibleEntities" });
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
    }
}