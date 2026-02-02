using System.Security.Claims;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.SessionTimeout;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace HE.Remediation.WebApp.Attributes.Authorisation;

public class CookieApplicationAuthoriseFilter : IAuthorizationFilter
{
    private readonly ILogger<CookieApplicationAuthoriseFilter> _logger;

    public CookieApplicationAuthoriseFilter(ILogger<CookieApplicationAuthoriseFilter> logger)
    {
        _logger = logger;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var safePath = context.HttpContext.Request.Path.ToString()
            .Replace("\r", string.Empty)
            .Replace("\n", string.Empty);

        var allowAnonymous = context.HttpContext.GetEndpoint()?.Metadata.GetMetadata<IAllowAnonymous>();

        if (allowAnonymous is not null)
        {
            _logger.LogInformation("AllowAnonymous detected, skipping authorization.");
            return;
        }

        if (context.HttpContext.User.Identity is not { IsAuthenticated: true })
        {
            _logger.LogWarning("User not authenticated, redirecting to login. Path: {Path}", safePath);
            context.Result = new RedirectResult("/Authentication/Login");
            return;
        }

        var applicationDataProvider = context.HttpContext.RequestServices.GetRequiredService<IApplicationDataProvider>();

        if (applicationDataProvider.GetApplicationId() == default)
        {
            _logger.LogWarning("ApplicationId missing in cookie, returning 404. Path: {Path}", safePath);
            context.Result = new NotFoundResult();
            return;
        }

        var auth0UserId = context.HttpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(auth0UserId))
        {
            _logger.LogWarning("Auth0UserId claim missing, returning 404. Path: {Path}", safePath);
            context.Result = new NotFoundResult();
            return;
        }

        var cookieAuth0UserId = applicationDataProvider.GetAuth0UserId();

        if (auth0UserId != cookieAuth0UserId)
        {
            _logger.LogWarning("Auth0UserId mismatch (claim: {ClaimId}, cookie: {CookieId}), returning 404. Path: {Path}", auth0UserId, cookieAuth0UserId, safePath);
            context.Result = new NotFoundResult();
            return;
        }

        var sessionTimeoutService = context.HttpContext.RequestServices.GetRequiredService<SessionTimeout>();
        var sessionTimeoutCookie = applicationDataProvider.GetSessionTimeout();
        var span = DateTimeOffset.Now.Subtract(sessionTimeoutCookie.Value);

        if (span.Minutes > sessionTimeoutService.Minutes)
        {
            _logger.LogWarning("Session timeout exceeded for user {UserId}, redirecting to session timeout. Path: {Path}", auth0UserId, safePath);
            context.Result = new RedirectResult("/Authentication/SessionTimeout");
        }
        else
        {
            applicationDataProvider.SetSessionTimeout();
            _logger.LogInformation("Session timeout refreshed for user {UserId}. Path: {Path}", auth0UserId, safePath);
        }
    }
}