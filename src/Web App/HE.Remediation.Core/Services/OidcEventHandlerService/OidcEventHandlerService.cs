using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace HE.Remediation.Core.Services.OidcEventHandlerService;

public class OidcEventHandlerService
{
    private const string ErrorKey = "error";
    private const string ErrorDescriptionKey = "error_description";

    private const string AccessDeniedDescriptor = "access_denied";
    private const string VerifyEmailDescriptor = "VERIFY_EMAIL:";

    public static Task HandleRemoteFailureError(RemoteFailureContext context)
    {
        var errorState = context.Request?.Form;

        if (!ErrorStateIsValid(errorState))
        {
            return Task.CompletedTask;
        }

        var error = errorState[ErrorKey];
        var errorDescription = errorState[ErrorDescriptionKey].ToString();

        if (error == AccessDeniedDescriptor && errorDescription.StartsWith(VerifyEmailDescriptor))
        {
            var emailToVerify = errorDescription.Replace(VerifyEmailDescriptor, string.Empty);
            context.Response.Redirect($"/Authentication/EmailNotVerified?emailAddress={HttpUtility.UrlEncode(emailToVerify)}");
            context.HandleResponse();
        }

        return Task.CompletedTask;
    }

    private static bool ErrorStateIsValid(IFormCollection errorState)
    {
        if (errorState is null)
        {
            return false;
        }

        if (!errorState.TryGetValue(ErrorKey, out var error)
            || string.IsNullOrWhiteSpace(error))
        {
            return false;
        }

        if (!errorState.TryGetValue(ErrorDescriptionKey, out var errorDescriptionValues)
            || string.IsNullOrWhiteSpace(errorDescriptionValues))
        {
            return false;
        }

        return true;
    }
}