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
using HE.Remediation.WebApp.Authorisation;
using HE.Remediation.WebApp.ViewModels.Administration;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondanceAddress.GetCorrespondanceAddress;
using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondanceAddress.SetCorrespondanceAddress;
using HE.Remediation.Core.UseCase.Areas.Administration.Profile.GetUserResponsibleEntityType;
using HE.Remediation.Core.UseCase.Areas.Administration.Dashboard.GetProfile;
using HE.Remediation.Core.Interface;

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
        public async Task<IActionResult>  Index()
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
                    if (profileCompletion.IsSecondaryContactInformationComplete == false)
                    {
                        return RedirectToAction("SecondaryContactDetails", "Account", new { Area = "Administration" });                
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
                if (profileCompletion.IsCorrespondenceAddressComplete)
                {
                    return RedirectToAction("index", "Account", new { Area = "Administration" });                
                }
                return RedirectToAction("CorrespondanceAddress", "Account", new { Area = "Administration" });                
            }
            return RedirectToAction("index", "Account", new { Area = "Administration" });                
        }

        #endregion

        #region "Secondary Contact Details"

        [HttpGet(nameof(SecondaryContactDetails))]
        public async Task<IActionResult> SecondaryContactDetails()
        {
            var response = await _sender.Send(GetSecondaryContactDetailsRequest.Request);
            var model = _mapper.Map<AdminSecondaryContactDetailsViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(SecondaryContactDetails))]
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

            return RedirectToAction("index", "Account", new { Area = "Administration" });                
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

        [HttpGet(nameof(CompanyAddress))]
        public async Task<IActionResult> CompanyAddress()
        {
            var response = await _sender.Send(GetCompanyAddressForCurrentUserRequest.Request);
            var model = _mapper.Map<CompanyAddressViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(CompanyAddress))]
        public async Task<IActionResult> CompanyAddress(CompanyAddressViewModel model, ESubmitAction submitAction)
        {
            var validator = new CompanyAddressViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetCompanyAddressForCurrentUserRequest>(model);
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
        #endregion

        #region "Correspondance Address"

        [HttpGet(nameof(CorrespondanceAddress))]
        public async Task<IActionResult> CorrespondanceAddress()
        {
            var response = await _sender.Send(GetCorrespondanceAddressRequest.Request);
            var model = _mapper.Map<CorrespondanceAddressViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(CorrespondanceAddress))]
        public async Task<IActionResult> CorrespondanceAddress(CorrespondanceAddressViewModel model, ESubmitAction submitAction)
        {
            var validator = new CorrespondanceAddressViewModelValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(model);
            }

            var request = _mapper.Map<SetCorrespondanceAddressRequest>(model);
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
                        return RedirectToAction("SecondaryContactDetails", "Account", new { Area = "Administration" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Account", new { Area = "Administration" });
                    }
                }                
            }

            return RedirectToAction("Index", "Account", new { Area = "Administration" });
        }

        #endregion
    }
}
