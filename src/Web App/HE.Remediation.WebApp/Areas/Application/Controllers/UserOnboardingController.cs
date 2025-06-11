using Microsoft.AspNetCore.Mvc;
using HE.Remediation.WebApp.Attributes.Authorisation;
using System.Security.Claims;
using MediatR;
using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Join;
using HE.Remediation.WebApp.ViewModels.Application;

namespace HE.Remediation.WebApp.Areas.Application.Controllers
{
    [Area("Application")]
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

        [HttpGet]
        public async Task<IActionResult> Join([FromQuery] bool newSignup = false)
        {
            var auth0UserId = User.Claims.Single(e => e.Type == ClaimTypes.NameIdentifier).Value;

            var response = await _sender.Send(new GetThirdPartyJoinRequest() { Auth0UserId = auth0UserId });
            var model = _mapper.Map<JoinViewModel>(response);
            if (model == null) // no pending invites
            {
                return newSignup ?
                    RedirectToAction("Index", "Account", new { Area = "Administration" }) :
                    RedirectToAction("Callback", "Authentication", new { Area = "" });
            }
            model.NewSignUp = newSignup;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Join(Guid collaborationUserId, Guid applicationDetailsId, bool newSignup, bool action)
        {
            var request = new SetThirdPartyJoinRequest()
            {
                CollaborationUserId = collaborationUserId,
                ApplicationDetailsId = applicationDetailsId,
                IsAccepted = action
            };
            await _sender.Send(request);

            return newSignup ?
                RedirectToAction("Index", "Account", new { Area = "Administration" }) :
                RedirectToAction("Callback", "Authentication", new { Area = "" });
        }
    }
}