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
                .WithParameter("prompt", "login")
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        public async Task CreateAccount()
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Callback", "Authentication")!)
                .WithParameter("prompt", "login")
                .WithParameter("screen_hint", "signup")
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
            // Collaboration:

            // Check user is not a revoked organisation user
            if (postLoginResponse.UserInviteStatus.IsOrganisationInviteRevoked)
            {
                return RedirectToAction("Blocked", "UserOnboarding", new { Area = "OrganisationManagement" });
            }

            //  Existing users hit this when logging in after being invited
            //  For new users - see logic in AccountController.RedirectToAccountHome
            if (postLoginResponse.UserInviteStatus.IsOrganisationInvitePending)
            {
                return RedirectToAction("Join", "UserOnboarding", new { Area = "OrganisationManagement" });
            }
            if (postLoginResponse.UserInviteStatus.IsApplicationInvitePending)
            {
                return RedirectToAction("Join", "UserOnboarding", new { Area = "Application" });
            }

            if (postLoginResponse.UserProfileCompletion.IsContactInformationComplete == false)
            {
                // Go to the Contact information (profile information) page
                return RedirectToAction("contactdetails", "Account", new { Area = "Administration" });
            }

            if (postLoginResponse.UserProfileCompletion.IsResponsibleEntityTypeSelectionComplete == false)
            {
                // Go to the Company or Individual (profile information) page                
                return RedirectToAction("profile", "Account", new { Area = "Administration" });
            }

            if (postLoginResponse.UserProfileCompletion.ResponsibleEntityType == EResponsibleEntityType.Unknown)
            {
                // extra guard - we should have IsResponsibleEntityTypeSelectionComplete set to false and hence not arrive here
                // However, just incase we don't know our entity type, we need to get it from the profile page
                return RedirectToAction("profile", "Account", new { Area = "Administration" });
            }
            else if (postLoginResponse.UserProfileCompletion.ResponsibleEntityType == EResponsibleEntityType.Individual)
            {
                if (postLoginResponse.UserProfileCompletion.IsCorrespondenceAddressComplete == false)
                {
                    // Go to the Correspondance Address page                    
                    return RedirectToAction("CorrespondenceAddress", "Account", new { Area = "Administration" });
                }

                if (postLoginResponse.UserProfileCompletion.IsSecondaryContactInformationComplete == false)
                {
                    // Go to the Secondary contact (profile information) page
                    if (postLoginResponse.UserProfileCompletion.WantedToAddSecondaryContact == true)
                    {
                        return RedirectToAction("SecondaryContactDetails", "Account", new { Area = "Administration" });
                    }
                }
            }
            else if (postLoginResponse.UserProfileCompletion.ResponsibleEntityType == EResponsibleEntityType.Company)
            {
                if (postLoginResponse.UserProfileCompletion.IsCompanyDetailsComplete == false)
                {
                    // Go to Company Details (profile information) page                    
                    return RedirectToAction("CompanyDetails", "Account", new { Area = "Administration" });
                }

                if (postLoginResponse.UserProfileCompletion.IsCompanyAddressComplete == false)
                {
                    // Go to Company Address (profile information) page                    
                    return RedirectToAction("CompanyAddress", "Account", new { Area = "Administration" });
                }

                if (postLoginResponse.UserProfileCompletion.IsSecondaryContactInformationComplete == false)
                {
                    // Go to the Secondary contact (profile information) page                    
                    return RedirectToAction("SecondaryContactDetails", "Account", new { Area = "Administration" });
                }
            }

            // Profile is considered to be complete
            return RedirectToAction("Index", "Dashboard", new { Area = "Application" });
        }
    }
}