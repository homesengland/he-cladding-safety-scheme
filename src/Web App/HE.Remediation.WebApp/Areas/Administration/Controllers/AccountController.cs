using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.Administration.CompanyAddress.GetCompanyAddressForCurrentUser;
using HE.Remediation.Core.UseCase.Areas.Administration.CompanyAddress.SetCompanyAddressForCurrentUser;
using HE.Remediation.Core.UseCase.Areas.Administration.CompanyDetails.GetCompanyDetailsForCurrentUser;
using HE.Remediation.Core.UseCase.Areas.Administration.CompanyDetails.SetCompanyDetailsForCurrentUser;
using HE.Remediation.Core.UseCase.Areas.Administration.ContactDetails.GetContactDetails;
using HE.Remediation.Core.UseCase.Areas.Administration.ContactDetails.SetContactDetails;
using HE.Remediation.Core.UseCase.Areas.Administration.Credentials.ChangePassword;
using HE.Remediation.Core.UseCase.Areas.Administration.Profile.SetUserResponsibleEntityType;
using HE.Remediation.Core.UseCase.Areas.Administration.SecondaryContactDetails.GetSecondaryContactDetails;
using HE.Remediation.Core.UseCase.Areas.Administration.SecondaryContactDetails.SetSecondaryContactDetails;
using HE.Remediation.WebApp.ViewModels.Administration;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using HE.Remediation.Core.UseCase.Areas.Administration.Profile.GetUserResponsibleEntityType;
using HE.Remediation.Core.UseCase.Areas.Administration.Dashboard.GetProfile;
using HE.Remediation.Core.Interface;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.ViewModels.Location;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.SetCorrespondenceAddress;
using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.GetCorrespondenceAddress;
using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.SetCorrespondenceAddressManual;
using HE.Remediation.Core.UseCase.Areas.Administration.CompanyAddress.SetCompanyAddressForCurrentUserManual;
using HE.Remediation.Core.UseCase.Areas.Administration.UserContactConsent.GetUserContactConsent;
using HE.Remediation.Core.UseCase.Areas.Administration.UserContactConsent.SetUserContactConsent;
using HE.Remediation.Core.UseCase.Areas.Administration.AddExtraContact.GetExtraContact;
using HE.Remediation.Core.UseCase.Areas.Administration.AddExtraContact.SetExtraContact;
using HE.Remediation.Core.UseCase.Areas.Administration.AdditionalContacts.GetAdditionalContact;
using HE.Remediation.Core.UseCase.Areas.Administration.DeleteExtraContact.SetDeleteExtraContact;

namespace HE.Remediation.WebApp.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration")]
    [CookieAuthorise]
    public class AccountController : Controller
    {
        private readonly IApplicationDataProvider _adp;
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public AccountController(IApplicationDataProvider dataprovider, ISender sender, IMapper mapper)
        {
            _adp = dataprovider;
            _sender = sender;
            _mapper = mapper;
        }

        #region "Dashboard"

        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            var response = await _sender.Send(GetProfileRequest.Request);
            var model = new DashboardUserProfileViewModel
            {
                ResponsibleEntityType = response.ResponsibleEntityTypeId
            };
            return View(model);
        }

        #endregion

        #region "Credentials"

        [HttpGet(nameof(ChangeCredentials))]
        public IActionResult ChangeCredentials()
        {
            return View();
        }

        [HttpGet(nameof(ChangePassword))]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost(nameof(ChangePassword))]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest viewModel, ESubmitAction submitAction)
        {
            var validator = new ChangePasswordViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View("ChangePassword", viewModel);
            }

            // TODO - where should additional password changing checks occur? we should do something basic on
            // the validator but additional validation should occur when changing password.
            return View();
        }

        #endregion

        #region "Profile"

        [HttpGet(nameof(Profile))]
        public async Task<IActionResult> Profile()
        {
            var profileCompletion = _adp.GetProfileCompletion();
            if (profileCompletion != null)
            {
                if ((profileCompletion.ResponsibleEntityType == EResponsibleEntityType.Company) ||
                    (profileCompletion.ResponsibleEntityType == EResponsibleEntityType.Individual))
                {
                    return RedirectToAction("Index", "Account", new { Area = "Administration" });
                }
            }

            var response = await _sender.Send(GetUserResponsibleEntityTypeRequest.Request);
            var model = new UserResponsibleEntityTypeViewModel
            {
                ResponsibleEntityType = response
            };
            return View(model);
        }

        [HttpPost(nameof(Profile))]
        public async Task<IActionResult> Profile(UserResponsibleEntityTypeViewModel model, ESubmitAction submitAction)
        {
            var validator = new UserResponsibleEntityTypeViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetUserResponsibleEntityTypeRequest>(model);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Continue)
            {
                var profileCompletion = _adp.GetProfileCompletion();
                if (model.ResponsibleEntityType == EResponsibleEntityType.Company)
                {
                    if (profileCompletion.IsCompanyDetailsComplete == false)
                    {
                        return RedirectToAction("CompanyDetails", "Account", new { Area = "Administration" });
                    }
                }
                else
                {
                    if (profileCompletion.IsCorrespondenceAddressComplete == false)
                    {
                        return RedirectToAction("CorrespondenceAddress", "Account", new { Area = "Administration" });
                    }
                }
            }
            return RedirectToAction("Index", "Account", new { Area = "Administration" });
        }

        #endregion

        #region "Contact Details"

        [HttpGet(nameof(ContactDetails))]
        public async Task<IActionResult> ContactDetails()
        {
            var response = await _sender.Send(GetContactDetailsRequest.Request);
            var model = _mapper.Map<AdminContactDetailsViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(ContactDetails))]
        public async Task<IActionResult> ContactDetails(AdminContactDetailsViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new AdminContactDetailsViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View("ContactDetails", viewModel);
            }

            var request = _mapper.Map<SetContactDetailsRequest>(viewModel);
            await _sender.Send(request);

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                var profileCompletion = _adp.GetProfileCompletion();
                if (profileCompletion.ResponsibleEntityType == EResponsibleEntityType.Unknown)
                {
                    return RedirectToAction("ContactInfoConsent", "Account", new { Area = "Administration" });
                }
                if (profileCompletion.ResponsibleEntityType == EResponsibleEntityType.Company)
                {
                    if (profileCompletion.IsCompanyDetailsComplete == false)
                    {
                        return RedirectToAction("CompanyDetails", "Account", new { Area = "Administration" });
                    }

                    return RedirectToAction("Index", "Account", new { Area = "Administration" });
                }
                else
                {
                    if (profileCompletion.IsContactInformationComplete == false)
                    {
                        return RedirectToAction("CorrespondenceAddress", "Account", new { Area = "Administration" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Account", new { Area = "Administration" });
                    }
                }
            }
            return RedirectToAction("index", "Account", new { Area = "Administration" });
        }

        #endregion

        #region "Additional Contacts"

        [HttpGet(nameof(AdditionalContacts))]
        public async Task<IActionResult> AdditionalContacts()
        {
            var response = await _sender.Send(GetAdditionalContactRequest.Request);
            var contactDetails = _mapper.Map<List<AdditionalContactsViewModel.SecondaryContactDetails>>(response);

            var outputModel = new AdditionalContactsViewModel()
            {
                ContactDetails = contactDetails
            };
            return View(outputModel);
        }

        [HttpGet("DeleteAdditionalContact/{Id?}")]
        public async Task<IActionResult> DeleteAdditionalContact(Guid? Id)
        {
            var request = new SetDeleteExtraContactRequest
            {
                Id = Id.HasValue ? Id.Value : null
            };
            await _sender.Send(request);

            return RedirectToAction("AdditionalContacts", "Account", new { Area = "Administration" });
        }

        #endregion

        #region "Secondary Contact Details"

        [HttpGet("SecondaryContactDetails/{Id?}")]
        public async Task<IActionResult> SecondaryContactDetails(Guid? Id)
        {
            GetSecondaryContactDetailsRequest request = new GetSecondaryContactDetailsRequest
            {
                Id = Id
            };
            var response = await _sender.Send(request);
            var model = _mapper.Map<AdminSecondaryContactDetailsViewModel>(response);

            return View(model);
        }

        [HttpPost("SecondaryContactDetails/{Id?}")]
        public async Task<IActionResult> SecondaryContactDetails(AdminSecondaryContactDetailsViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new AdminSecondaryContactDetailsViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View("SecondaryContactDetails", viewModel);
            }

            var request = _mapper.Map<SetSecondaryContactDetailsRequest>(viewModel);
            await _sender.Send(request);

            if (_adp.IsEnforcedFlow())
            {
                return RedirectToAction("Index", "Dashboard", new { Area = "Application" });
            }

            return RedirectToAction("AdditionalContacts", "Account", new { Area = "Administration" });                
        }

        #endregion

        #region "Company Details"

        [HttpGet(nameof(CompanyDetails))]
        public async Task<IActionResult> CompanyDetails()
        {
            var response = await _sender.Send(GetCompanyDetailsForCurrentUserRequest.Request);
            var model = _mapper.Map<CompanyDetailsViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(CompanyDetails))]
        public async Task<IActionResult> CompanyDetails(CompanyDetailsViewModel model, ESubmitAction submitAction)
        {
            var validator = new CompanyDetailsViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetCompanyDetailsForCurrentUserRequest>(model);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Continue)
            {
                var profileCompletion = _adp.GetProfileCompletion();
                if (profileCompletion.IsCompanyAddressComplete == false)
                {
                    return RedirectToAction("CompanyAddress", "Account", new { Area = "Administration" });
                }

                return RedirectToAction("Index", "Account", new { Area = "Administration" });
            }
            return RedirectToAction("Index", "Account", new { Area = "Administration" });
        }

        #endregion
        
        #region "Company Address"

        /// <summary>
        /// Shows the initial company post code entry screen - a text box prompting for a post code
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(CompanyAddress))]        
        public async Task<IActionResult> CompanyAddress()
        {
            var response = await _sender.Send(GetCompanyAddressForCurrentUserRequest.Request);
            var model = _mapper.Map<PostCodeEntryViewModel>(response);
            return View(model);
        }

        /// <summary>
        /// When the user submits their company address details for the manual entry details screen
        /// </summary>
        /// <param name="model"></param>
        /// <param name="submitAction"></param>
        /// <returns></returns>
        [HttpPost(nameof(CompanyAddress))]
        public async Task<IActionResult> CompanyAddress(PostCodeManualViewModel model, ESubmitAction submitAction)
        {
            var validator = new PostCodeManualViewModelValidator(false, false);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View("CompanyAddressManual", model);
            }

            var request = _mapper.Map<SetCompanyAddressForCurrentUserManualRequest>(model);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Continue)
            {
                var profileCompletion = _adp.GetProfileCompletion();
                if (profileCompletion.IsSecondaryContactInformationComplete == false)
                {
                    return RedirectToAction("SecondaryContactDetails", "Account", new { Area = "Administration" });
                }
                return RedirectToAction("Index", "Account", new { Area = "Administration" });
            }
            return RedirectToAction("Index", "Account", new { Area = "Administration" });
        }

        /// <summary>
        /// Showed when the user selects a post code from the drop down
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="viewModel"></param>
        /// <param name="submitAction"></param>
        /// <returns></returns>
        [HttpPost(nameof(CompanyAddrPostCodeItemSelected))]
        public async Task<IActionResult> CompanyAddrPostCodeItemSelected(string returnUrl, PostCodeSelectionViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new PostCodeSelectionViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                // need to set these properties on the output model if there is an error
                return View("CompanyAddressResults", viewModel);
            }

            var request = _mapper.Map<SetCompanyAddressForCurrentUserRequest>(viewModel);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Continue)
            {
                var profileCompletion = _adp.GetProfileCompletion();
                if (profileCompletion.IsSecondaryContactInformationComplete == false)
                {
                    return RedirectToAction("SecondaryContactDetails", "Account", new { Area = "Administration" });
                }
                return RedirectToAction("Index", "Account", new { Area = "Administration" });
            }
            return RedirectToAction("Index", "Account", new { Area = "Administration" });
        }

        /// <summary>
        /// Called when the user enters a post code on the company lookup screen. This should only receive
        /// a post code from the user. It then takes us to either the manually entry screen or the list of results in a drop down.        
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="viewModel"></param>
        /// <param name="submitAction"></param>
        /// <returns></returns>
        [HttpGet(nameof(CompanyAddrPostCodeItemEntered))]
        public async Task<IActionResult> CompanyAddrPostCodeItemEntered(string returnUrl, PostCodeEntryViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new PostCodeEntryViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View("CompanyAddress", viewModel);
            }

            if (submitAction == ESubmitAction.FindAddress)
            {
                var request = new GetPostCodeRequest { PostCode = viewModel.PostCode };
                var response = await _sender.Send(request);
                var newMappedModel = _mapper.Map<PostCodeSelectionViewModel>(response);

                if (!newMappedModel.HaveResults)
                {                    
                    PostCodeManualViewModel manualViewModel = new PostCodeManualViewModel();
                    manualViewModel.Postcode = viewModel.PostCode;
                    return View("CompanyAddressManual", manualViewModel);
                }
                return View("CompanyAddressResults", newMappedModel);
            }
            
            return View("CompanyAddress", viewModel);
        }

        /// <summary>
        /// Shows a company address entry manual entry screen. 
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="postCode"></param>
        /// <returns></returns>
        [HttpGet(nameof(CompanyAddressManual))]
        public async Task<IActionResult> CompanyAddressManual(string returnUrl, string postCode)
        {
            var response = await _sender.Send(GetCompanyAddressForCurrentUserRequest.Request);
            var model = _mapper.Map<PostCodeManualViewModel>(response);
            return View(model);
        }

        #endregion

        #region "Correspondence Address"

        /// <summary>
        /// Shows the initial correspondence address entry screen - a text box with a post code
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(CorrespondenceAddress))]
        public async Task<IActionResult> CorrespondenceAddress()
        {
            var response = await _sender.Send(GetCorrespondenceAddressRequest.Request);
            var model = _mapper.Map<PostCodeEntryViewModel>(response);
            return View(model);
        }

        /// <summary>
        /// When the user submits their correspondence address details for the manual entry details screen
        /// </summary>
        /// <param name="model"></param>
        /// <param name="submitAction"></param>
        /// <returns></returns>
        [HttpPost(nameof(CorrespondenceAddress))]
        public async Task<IActionResult> CorrespondenceAddress(PostCodeManualViewModel model, ESubmitAction submitAction)
        {
            var validator = new PostCodeManualViewModelValidator(false, false);
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View("CorrespondenceAddressManual", model);
            }

            var request = _mapper.Map<SetCorrespondenceAddressManualRequest>(model);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Continue)
            {
                var profileCompletion = _adp.GetProfileCompletion();
                if (profileCompletion.ResponsibleEntityType == EResponsibleEntityType.Unknown)
                {
                    return RedirectToAction("Profile", "Account", new { Area = "Administration" });
                }
                if (profileCompletion.ResponsibleEntityType == EResponsibleEntityType.Company)
                {
                    // we shouldn't be here for a company but if the user tries to get here, keep them in line with our
                    // enforced path
                    if (profileCompletion.IsCompanyDetailsComplete == false)
                    {
                        return RedirectToAction("CompanyDetails", "Account", new { Area = "Administration" });
                    }
                    
                    return RedirectToAction("Index", "Account", new { Area = "Administration" });                                            
                }
                else
                {
                    if (profileCompletion.IsSecondaryContactInformationComplete == false)
                    {
                        return RedirectToAction("AddExtraContact", "Account", new { Area = "Administration" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Account", new { Area = "Administration" });
                    }
                }                
            }

            return RedirectToAction("Index", "Account", new { Area = "Administration" });
        }
        
        /// <summary>
        /// Showed when the user selects a post code from the drop down
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="viewModel"></param>
        /// <param name="submitAction"></param>
        /// <returns></returns>
        [HttpPost(nameof(CorrespondenceAddrPostCodeItemSelected))]
        public async Task<IActionResult> CorrespondenceAddrPostCodeItemSelected(string returnUrl, PostCodeSelectionViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new PostCodeSelectionViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                // need to set these properties on the output model if there is an error
                return View("CorrespondenceAddressResults", viewModel);
            }

            var request = _mapper.Map<SetCorrespondenceAddressRequest>(viewModel);
            await _sender.Send(request);

            if (submitAction == ESubmitAction.Continue)
            {
                var profileCompletion = _adp.GetProfileCompletion();
                if (profileCompletion.IsSecondaryContactInformationComplete == false)
                {
                    return RedirectToAction("AddExtraContact", "Account", new { Area = "Administration" });
                }
                else
                {
                    return RedirectToAction("Index", "Account", new { Area = "Administration" });
                }
            }
            return RedirectToAction("Index", "Account", new { Area = "Administration" });
        }

        /// <summary>
        /// Called when the user enters a post code on the correspondence lookup screen. This should only receive
        /// a post code from the user. It then takes us to either the manually entry screen or the list of results in a drop down
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="viewModel"></param>
        /// <param name="submitAction"></param>
        /// <returns></returns>
        [HttpGet(nameof(CorrespondenceAddrPostCodeItemEntered))]
        public async Task<IActionResult> CorrespondenceAddrPostCodeItemEntered(string returnUrl, PostCodeEntryViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new PostCodeEntryViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View("CorrespondenceAddress", viewModel);
            }

            if (submitAction == ESubmitAction.FindAddress)
            {
                var request = new GetPostCodeRequest { PostCode = viewModel.PostCode };
                var response = await _sender.Send(request);
                var newMappedModel = _mapper.Map<PostCodeSelectionViewModel>(response);

                if (!newMappedModel.HaveResults)
                {
                    PostCodeManualViewModel manualViewModel = new PostCodeManualViewModel();
                    manualViewModel.Postcode = viewModel.PostCode;
                    return View("CorrespondenceAddressManual", manualViewModel);
                }
                return View("CorrespondenceAddressResults", newMappedModel);
            }
            
            return View("CorrespondenceAddress", viewModel);
        }

        /// <summary>
        /// Shows a correspondence address entry manual entry screen.
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="postCode"></param>
        /// <returns></returns>
        [HttpGet(nameof(CorrespondenceAddressManual))]
        public async Task<IActionResult> CorrespondenceAddressManual(string returnUrl, string postCode)
        {
            var response = await _sender.Send(GetCorrespondenceAddressRequest.Request);
            var model = _mapper.Map<PostCodeManualViewModel>(response);
            return View(model);
        }

        #endregion

        #region "Consent"

        [HttpGet(nameof(ContactInfoConsent))]
        public async Task<IActionResult> ContactInfoConsent()
        {
            var response = await _sender.Send(GetUserContactConsentRequest.Request);
            var model = _mapper.Map<ContactInfoConsentViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(ContactInfoConsent))]
        public async Task<IActionResult> ContactInfoConsent(ContactInfoConsentViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new ContactInfoConsentViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View(viewModel);
            }

            var request = _mapper.Map<SetUserContactConsentRequest>(viewModel);
            await _sender.Send(request);

            return RedirectToAction("Profile", "Account", new { Area = "Administration" });
        }

        #endregion

        #region "Add Extra Contact"

        [HttpGet(nameof(AddExtraContact))]
        public async Task<IActionResult> AddExtraContact()
        {
            var response = await _sender.Send(GetExtraContactRequest.Request);
            var model = _mapper.Map<AddExtraContactViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(AddExtraContact))]
        public async Task<IActionResult> AddExtraContact(AddExtraContactViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new AddExtraContactViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View(viewModel);
            }

            var request = _mapper.Map<SetExtraContactRequest>(viewModel);
            await _sender.Send(request);

            var action = request!.AddContact == ENoYes.Yes
                ? nameof(SecondaryContactDetails)
                : nameof(Profile);

            return RedirectToAction(action, "Account", new { Area = "Administration" });
        }

        #endregion
    }
}
