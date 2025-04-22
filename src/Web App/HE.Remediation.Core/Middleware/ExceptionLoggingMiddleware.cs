using HE.Remediation.Core.Interface;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Context;

namespace HE.Remediation.Core.Middleware
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IApplicationDataProvider applicationDataProvider)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var userId = applicationDataProvider.GetUserId();
                using (LogContext.PushProperty("UserId", userId))
                {
                    Log.Error(ex.Message, ex);
                }
                throw;
            }
        }
    }
}
