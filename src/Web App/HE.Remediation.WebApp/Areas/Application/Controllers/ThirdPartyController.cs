using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Get;
using HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Invite;
using HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.InviteMember;
using HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.RemoveAccess;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.ViewModels.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.Application.Controllers
{
    [Area("Application")]
    [CookieApplicationAuthorise]
    public class ThirdPartyController(ISender sender, IMapper mapper, IApplicationDataProvider applicationDataProvider) : Controller
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;
        private readonly IApplicationDataProvider _applicationDataProvider = applicationDataProvider;

        public async Task<IActionResult> Index()
        {
            var response = await _sender.Send(GetThirdPartyRequest.Request);
            var viewModel = _mapper.Map<ThirdPartyViewModel>(response);

            return View(viewModel);
        }

        [HttpGet(nameof(InviteContributor))]
        public async Task<IActionResult> InviteContributor(Guid teamMemberId)
        {
            var response = await _sender.Send(new GetInviteRequest(teamMemberId));
            var viewModel = _mapper.Map<InviteViewModel>(response);

            return View(viewModel);
        }

        [HttpPost(nameof(InviteContributor))]
        public IActionResult InviteContributor(InviteViewModel viewModel)
        {
            return RedirectToAction("InvitationDeclaration", new { teamMemberId = viewModel.TeamMemberId });
        }

        [HttpGet(nameof(InvitationDeclaration))]
        public IActionResult InvitationDeclaration(Guid teamMemberId)
        {
            var viewModel = new InvitationDeclarationViewModel
            {
                TeamMemberId = teamMemberId
            };

            return View(viewModel);
        }

        [HttpPost(nameof(InvitationDeclaration))]
        public async Task<IActionResult> InvitationDeclaration(InvitationDeclarationViewModel viewModel)
        {
            var validator = new InvitationDeclarationViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(viewModel);
            }

            var auth0UserId = _applicationDataProvider.GetAuth0UserId();
            var request = new SetInviteMemberRequest(viewModel.TeamMemberId.Value, auth0UserId);

            await _sender.Send(request);

            return RedirectToAction("InvitationSent", new { teamMemberId = viewModel.TeamMemberId });
        }

        [HttpGet(nameof(InvitationSent))]
        public async Task<IActionResult> InvitationSent(Guid teamMemberId)
        {
            var response = await _sender.Send(new GetInviteRequest(teamMemberId));
            var viewModel = new InvitationSentViewModel
            {
                TeamMemberId = teamMemberId,
                InvitedEmailAddress = response.InvitedEmailAddress
            };

            return View(viewModel);
        }

        [HttpGet(nameof(RemoveAccess))]
        public async Task<IActionResult> RemoveAccess(Guid teamMemberId)
        {
            var response = await _sender.Send(new GetInviteRequest(teamMemberId));
            var viewModel = new RemoveAccessViewModel
            {
                TeamMemberId = teamMemberId,
                InvitedName = response.InvitedName
            };

            return View(viewModel);
        }

        [HttpPost(nameof(RemoveAccess))]
        public async Task<IActionResult> RemoveAccess(RemoveAccessViewModel viewModel)
        {
            var validator = new RemoveAccessViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(viewModel);
            }

            var auth0UserId = _applicationDataProvider.GetAuth0UserId();
            await _sender.Send(new SetRemoveAccessRequest(viewModel.TeamMemberId.Value, auth0UserId));

            return RedirectToAction("Index");
        }
    }
}