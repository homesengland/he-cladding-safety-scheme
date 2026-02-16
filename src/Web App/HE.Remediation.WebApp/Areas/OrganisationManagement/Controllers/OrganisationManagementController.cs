using AutoMapper;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.ViewModels.OrganisationManagement;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.OrganisationDetails;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.Users;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.InviteMember;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.InvitationDeclaration;
using System.Security.Claims;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.RemoveMember;

namespace HE.Remediation.WebApp.Areas.OrganisationManagement.Controllers
{
    [Area("OrganisationManagement")]
    [Route("OrganisationManagement")]
    [CookieAuthorise]
    public class OrganisationManagementController : Controller
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public OrganisationManagementController(ISender sender, IMapper mapper, IApplicationDataProvider applicationDataProvider)
        {
            _sender = sender;
            _mapper = mapper;
            _applicationDataProvider = applicationDataProvider;
        }

        [HttpGet(nameof(Introduction))]
        public IActionResult Introduction()
        {
            return View();
        }

        [HttpGet(nameof(OrganisationDetails))]
        public IActionResult OrganisationDetails()
        {
            return View(new OrganisationDetailsViewModel());
        }

        [HttpPost(nameof(OrganisationDetails))]
        public async Task<IActionResult> OrganisationDetails(OrganisationDetailsViewModel viewModel)
        {
            var validator = new OrganisationDetailsViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(viewModel);
            }

            var request = _mapper.Map<OrganisationDetailsRequest>(viewModel);
            request.UserId = _applicationDataProvider.GetAuth0UserId();

            try
            {
                var response = await _sender.Send(request);
                TempData["OrganisationName"] = response.Name;
                return RedirectToAction("OrganisationCreated");
            }
            catch (DuplicateRegistrationNumberException ex)
            {
                ModelState.AddModelError(nameof(viewModel.RegistrationNumber), ex.Message);
                return View(viewModel);
            }
        }

        [HttpGet(nameof(OrganisationCreated))]
        public IActionResult OrganisationCreated()
        {
            if(string.IsNullOrEmpty((string)TempData["OrganisationName"]))
            {
                return RedirectToAction("Index", "Dashboard", new { Area = "Application" });
            }
            return View();
        }

        [HttpGet(nameof(Users))]
        public async Task<IActionResult> Users()
        {
            var request = new UsersRequest(_applicationDataProvider.GetAuth0UserId());

            try
            {
                var response = await _sender.Send(request);
                var model = _mapper.Map<UsersViewModel>(response);
                return View(model);
            } 
            catch(OrganisationNotExistsException)
            {
                return RedirectToAction("Introduction");
            }
        }

        [HttpGet("InviteMember/{organisationId}")]
        public IActionResult InviteMember(Guid organisationId)
        {
            return View(new InviteMemberViewModel() { OrganisationId = organisationId });
        }

        [HttpGet("EditMember/{collaborationUserId}")]
        public async Task<IActionResult> EditMember(Guid collaborationUserId)
        {
            var response = await _sender.Send(new GetMemberRequest() { CollaborationUserId = collaborationUserId });
            var model = _mapper.Map<InviteMemberViewModel>(response);
            return View("InviteMember", model);
        }

        [HttpPost("InviteMember")]
        public async Task<IActionResult> InviteMember(InviteMemberViewModel viewModel)
        {
            var adminEmail = User.Claims.Single(e => e.Type == ClaimTypes.Email).Value;
            var validator = new InviteMemberViewModelValidator(adminEmail);
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(viewModel);
            }

            var request = _mapper.Map<UpsertMemberRequest>(viewModel);
            request.AdminUserId = _applicationDataProvider.GetAuth0UserId();

            try
            {
                var response = await _sender.Send(request);

                if(viewModel.UserStatus.GetValueOrDefault() < Core.Enums.ECollaborationUserStatus.Invited)
                {
                    return RedirectToAction("InvitationDeclaration", new { response.CollaborationUserId });
                }

                return RedirectToAction("Users");

            }
            catch (UserEmailExistsException)
            {
                ModelState.AddModelError(nameof(viewModel.Email), "User with given email already exists");
                return View(viewModel);
            }
            catch (OrganisationAssociationException)
            {
                ModelState.AddModelError(nameof(viewModel.Email), "This e-mail address is already associated with an organisation and cannot be invited");
                return View(viewModel);
            }
            catch (InvalidAdminOrganisationException)
            {
                return RedirectToAction("Index", "Dashboard", new { Area = "Application" });
            } 
            catch(MinimumAdminsException)
            {
                ModelState.AddModelError(nameof(viewModel.ApplicationRole), "You have reached the minimum number of administrators for the organisation");
                return View(viewModel);
            }
            catch(MaximumAdminsException)
            {
                ModelState.AddModelError(nameof(viewModel.ApplicationRole), "You have reached the maximum number of administrators for the organisation");
                return View(viewModel);
            }
        }

        [HttpGet("InvitationDeclaration/{collaborationUserId}")]
        public IActionResult InvitationDeclaration(Guid collaborationUserId)
        {
            var model = new InvitationDeclarationViewModel() { 
                CollaborationUserId = collaborationUserId 
            };
            return View(model);
        }

        [HttpPost("InvitationDeclaration/{collaborationUserId}")]
        public async Task<IActionResult> InvitationDeclaration(InvitationDeclarationViewModel viewModel)
        {
            var validator = new InvitationDeclarationViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(viewModel);
            }

            var request = _mapper.Map<InvitationDeclarationRequest>(viewModel);
            request.Auth0UserId = _applicationDataProvider.GetAuth0UserId();
            var response = await _sender.Send(request);

            TempData["Email"] = response.InvitationEmailAddress;

            return RedirectToAction("InvitationSent");
        }

        [HttpGet("InvitationSent")]
        public IActionResult InvitationSent()
        {
            if (string.IsNullOrEmpty((string)TempData["Email"]))
            {
                return RedirectToAction("Index", "Dashboard", new { Area = "Application" });
            }
            return View();
        }

    #region Remove Member

        [HttpGet(nameof(RemoveMember))]
        [Route("{id}")]
        public async Task<IActionResult> RemoveMember(Guid id)
        {
            var request = new RemoveMembersGetRequest(id);
            var response = await _sender.Send(request);
            if (response == null)
            {
                // Member not found, redirect to Users or show a not found page
                return RedirectToAction(nameof(Users));
            }
            var model = _mapper.Map<RemoveMemberViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(RemoveMember))]
        public async Task<IActionResult> RemoveMember(RemoveMemberViewModel viewModel)
        {
            var request = new RemoveMembersSetRequest(viewModel.Id);
            request.OrganisationId = viewModel.CollaborationOrganisationId;
            request.AdminUserId = _applicationDataProvider.GetAuth0UserId();

            _ = await _sender.Send(request);

            return RedirectToAction(nameof(Users));
        }

    #endregion

    }
}
