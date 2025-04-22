using Microsoft.Extensions.Hosting;
using Serilog;

namespace HE.Remediation.Core.Middleware
{
    public static  class SerilogMiddlewareConfiguration
    {
        public static IHostBuilder AddSerilogLogging(this IHostBuilder builder) =>
            builder.UseSerilog((hostingContext, loggerConfiguration) =>
            { 
                loggerConfiguration
                    .Enrich.WithProperty("Source", "GOV")
                    .Enrich.FromLogContext() // UserId added in ExceptionLoggingMiddleware
                    .WriteToSql(hostingContext.Configuration["DB_CONNSTRING"])
                    .WriteTo.Console();

            });
    }
}
