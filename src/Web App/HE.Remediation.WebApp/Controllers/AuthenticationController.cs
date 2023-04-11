using Auth0.AspNetCore.Authentication;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.Authentication.Login.PostLogin;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HE.Remediation.WebApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ISender _sender;

        public AuthenticationController(ISender sender)
        {
            _sender = sender;
        }

        public async Task Login()
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Callback", "Authentication")!)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Index", "LandingPage")!)
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> Callback()
        {
            var userProfile = await _sender.Send(GetPostLoginRequest());

            return GetPostLoginActionResult(userProfile);
        }

        public async Task SessionTimeout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri("/Error/HandleError/408")
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public IActionResult EmailNotVerified([FromQuery] string emailAddress)
        {
            return View("EmailNotVerified", emailAddress);
        }

        private PostLoginRequest GetPostLoginRequest()
        {
            return new PostLoginRequest
            {
                Auth0UserId = User.Claims.Single(e => e.Type == ClaimTypes.NameIdentifier).Value,
                EmailAddress = User.Claims.Single(e => e.Type == ClaimTypes.Email).Value,
                UserAgent = HttpContext.Request.Headers["User-Agent"],
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                LoginDateTime = DateTime.UtcNow
            };
        }

        private IActionResult GetPostLoginActionResult(PostLoginResponse postLoginResponse)
        {
            if (postLoginResponse.UserProfileCompletion.IsContactInformationComplete == false)
            {
                // Replace with redirection to Contact information (profile information) page
                // TODO: https://dev.azure.com.mcas.ms/homesengland/Medium-Rise%20Scheme/_workitems/edit/49626/
                 return RedirectToAction("contactdetails", "Administration");
            }

            if (postLoginResponse.UserProfileCompletion.IsCorrespondenceAddressComplete == false)
            {
                // TODO: Replace with redirection to Correspondance Address page
                // https://dev.azure.com.mcas.ms/homesengland/Medium-Rise%20Scheme/_workitems/edit/48889/
                return RedirectToAction("CorrespondanceAddress", "Administration");
            }

            if (postLoginResponse.UserProfileCompletion.IsResponsibleEntityTypeSelectionComplete == false)
            {
                // TODO: Replace with redirection to Company or Individual (profile information) page
                // https://dev.azure.com.mcas.ms/homesengland/Medium-Rise%20Scheme/_workitems/edit/48889/
                return RedirectToAction("profile", "Administration");
            }

            if (postLoginResponse.UserProfileCompletion.ResponsibleEntityType == EResponsibleEntityType.Company)
            {
                if (postLoginResponse.UserProfileCompletion.IsCompanyDetailsComplete == false)
                {
                    // TODO: Replace with redirection to Company Details (profile information) page
                    // https://dev.azure.com.mcas.ms/homesengland/Medium-Rise%20Scheme/_workitems/edit/48890/
                    return RedirectToAction("companydetails", "Administration");
                }

                if (postLoginResponse.UserProfileCompletion.IsCompanyAddressComplete == false)
                {
                    // TODO: Replace with redirection to Company Address (profile information) page
                    // https://dev.azure.com.mcas.ms/homesengland/Medium-Rise%20Scheme/_workitems/edit/48891/
                    return RedirectToAction("companyaddress", "Administration");
                }
            }
            
            if (postLoginResponse.UserProfileCompletion.IsSecondaryContactInformationComplete == false)
            {
                // TODO: Replace with redirection to Secondary contact (profile information) page
                // https://dev.azure.com.mcas.ms/homesengland/Medium-Rise%20Scheme/_workitems/edit/48892/
                return RedirectToAction("SecondaryContactDetails", "Administration");
            }
            
            // Profile is considered to be complete
            return RedirectToAction("Dashboard", "Application");
        }
    }
}
