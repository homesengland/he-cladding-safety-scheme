using System.Security.Claims;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.SessionTimeout;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HE.Remediation.WebApp.Authorisation;

public class CookieAuthoriseFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity is not { IsAuthenticated: true })
        {
            context.Result = new RedirectResult("/Authentication/Login");
            return;
        }

        var applicationDataProvider = context.HttpContext.RequestServices.GetRequiredService<IApplicationDataProvider>();

        var claimId = context.HttpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var cookieId = applicationDataProvider.GetAuth0UserId();

        if (string.IsNullOrEmpty(claimId))
        {
            context.Result = new NotFoundResult();
            return;
        }

        if (claimId != cookieId)
        {
            context.Result = new NotFoundResult();
        }

        var sessionTimeoutService = context.HttpContext.RequestServices.GetRequiredService<SessionTimeout>();
        var sessionTimeoutCookie = applicationDataProvider.GetSessionTimeout();
        var span = DateTimeOffset.Now.Subtract(sessionTimeoutCookie.Value);

        if (span.Minutes > sessionTimeoutService.Minutes)
        {
            context.Result = new RedirectResult("/Authentication/SessionTimeout");
        }
        else
        {
            applicationDataProvider.SetSessionTimeout();
        }
    }
}