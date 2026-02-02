using Microsoft.AspNetCore.Mvc;
using HE.Remediation.WebApp.Attributes.Authorisation;
using System.Security.Claims;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.UserOnboarding;
using Mediator;
using HE.Remediation.WebApp.ViewModels.OrganisationManagement;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;

namespace HE.Remediation.WebApp.Areas.OrganisationManagement.Controllers
{
    [Area("OrganisationManagement")]
    [Route("UserOnboarding")]
    [CookieAuthorise]
    public class UserOnboardingController : Controller
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public UserOnboardingController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet(nameof(Join))]
        public async Task<IActionResult> Join([FromQuery]bool newSignup = false)
        {
            var auth0UserId = User.Claims.Single(e => e.Type == ClaimTypes.NameIdentifier).Value;

            var response = await _sender.Send(new GetOrgUserInviteRequest() { Auth0UserId = auth0UserId });
            var model = _mapper.Map<JoinViewModel>(response);
            if(model == null) // no pending invites
            {
                return newSignup ?
                    RedirectToAction("Callback", "Authentication") :
                    RedirectToAction("Index", "Account", new { Area = "Administration" });
            }
            model.NewSignUp = newSignup;
            return View(model);
        }

        [HttpPost(nameof(Join))]
        public async Task<IActionResult> Join(Guid collaborationUserId, bool newSignup, bool action)
        {
            var request = new SetOrgUserInviteRequest() { CollaborationUserId = collaborationUserId, IsAccepted = action };
            await _sender.Send(request);

            return newSignup ?
                RedirectToAction("Index", "Account", new { Area = "Administration" }) :
                RedirectToAction("Callback", "Authentication");
        }

        [HttpGet(nameof(Blocked))]
        public async Task<IActionResult> Blocked()
        {
            await HttpContext.SignOutAsync();
            return View();
        }
    }
}