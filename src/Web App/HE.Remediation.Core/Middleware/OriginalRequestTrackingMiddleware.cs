using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace HE.Remediation.Core.Middleware;

/// <summary>
/// Middleware that captures the original request path and method before any error handling redirects.
/// This ensures that when an error occurs and the request is redirected to /error,
/// the trace still contains information about the original failing endpoint.
/// </summary>
public class OriginalRequestTrackingMiddleware
{
    private readonly RequestDelegate _next;

    public OriginalRequestTrackingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var activity = Activity.Current;
        
        if (activity != null)
        {
            // Capture original request details before any error handling
            var originalPath = context.Request.Path.ToString();
            var originalMethod = context.Request.Method;
            var originalQueryString = context.Request.QueryString.ToString();
            
            // Set these as tags on the current activity
            activity.SetTag("http.original_path", originalPath);
            activity.SetTag("http.original_method", originalMethod);
            
            if (!string.IsNullOrEmpty(originalQueryString))
            {
                activity.SetTag("http.original_query_string", originalQueryString);
            }
            
            // Also add to baggage so it propagates to child spans
            activity.SetBaggage("original.path", originalPath);
            activity.SetBaggage("original.method", originalMethod);
            
            // If this is an error endpoint, also capture the referer for comparison
            if (originalPath.Equals("/error", StringComparison.OrdinalIgnoreCase) ||
                originalPath.StartsWith("/error/", StringComparison.OrdinalIgnoreCase))
            {
                var referer = context.Request.Headers["Referer"].ToString();
                if (!string.IsNullOrEmpty(referer))
                {
                    activity.SetTag("error.referer_url", referer);
                }
                
                // Check for stored exception information
                var exceptionFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
                if (exceptionFeature?.Error != null)
                {
                    activity.SetTag("error.exception_type", exceptionFeature.Error.GetType().FullName);
                    
                    if (!string.IsNullOrEmpty(exceptionFeature.Error.Source))
                    {
                        activity.SetTag("error.exception_source", exceptionFeature.Error.Source);
                    }
                    
                    if (!string.IsNullOrEmpty(exceptionFeature.Path))
                    {
                        activity.SetTag("error.original_failing_path", exceptionFeature.Path);
                    }
                }
            }
        }
        
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            if (activity != null)
            {
                activity.SetTag("error.caught_exception_type", ex.GetType().FullName);
                activity.SetTag("error.caught_at_path", context.Request.Path.ToString());
            }
            throw;
        }
    }
}
