using System.Security.Claims;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.SessionTimeout;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HE.Remediation.WebApp.Authorisation;

public class CookieApplicationAuthoriseFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity is not { IsAuthenticated: true })
        {
            context.Result = new RedirectResult("/Authentication/Login");
            return;
        }

        var applicationDataProvider = context.HttpContext.RequestServices.GetRequiredService<IApplicationDataProvider>();

        if (applicationDataProvider.GetApplicationId() == default)
        {
            context.Result = new NotFoundResult();
            return;
        }

        var auth0UserId = context.HttpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(auth0UserId))
        {
            context.Result = new NotFoundResult();
            return;
        }

        var cookieAuth0UserId = applicationDataProvider.GetAuth0UserId();

        if (auth0UserId != cookieAuth0UserId)
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