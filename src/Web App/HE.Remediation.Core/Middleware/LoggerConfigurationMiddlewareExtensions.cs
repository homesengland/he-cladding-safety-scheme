using Microsoft.AspNetCore.Builder;

namespace HE.Remediation.Core.Middleware
{
    public static class LoggerConfigurationMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionLoggingMiddleware>();
        }
    }
}
