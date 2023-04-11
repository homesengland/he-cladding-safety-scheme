using Microsoft.Extensions.Hosting;
using Serilog;

namespace HE.Remediation.Core.Middleware
{
    public static  class SerilogMiddlewareConfiguration
    {
        public static IHostBuilder AddSerilogLogging(this IHostBuilder builder) =>
            builder.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                loggerConfiguration.WriteToSql(hostingContext.Configuration["DB_CONNSTRING"]);

                loggerConfiguration.WriteTo.Console();

            });
    }
}
