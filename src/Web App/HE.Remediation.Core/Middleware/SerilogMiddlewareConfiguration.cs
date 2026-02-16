using System.Reflection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace HE.Remediation.Core.Middleware
{
    public static class SerilogMiddlewareConfiguration
    {
        public static IHostBuilder AddSerilogLogging(this IHostBuilder builder) =>
            builder.UseSerilog((hostingContext, loggerConfiguration) =>
            { 
                var otelEndpoint = hostingContext.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"] 
                    ?? Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT");

                loggerConfiguration
                    .Enrich.WithProperty("Source", "GOV")
                    .Enrich.FromLogContext() // UserId added in ExceptionLoggingMiddleware
                    .WriteToSql(hostingContext.Configuration["DB_CONNSTRING"])
                    .WriteTo.Console();

                // Add OpenTelemetry OTLP export if endpoint is configured
                if (!string.IsNullOrEmpty(otelEndpoint))
                {
                    loggerConfiguration.WriteTo.OpenTelemetry(options =>
                    {
                        options.Endpoint = otelEndpoint;
                        options.Protocol = Serilog.Sinks.OpenTelemetry.OtlpProtocol.Grpc;
                        options.ResourceAttributes = new Dictionary<string, object>
                        {
                            ["service.name"] = "HE.Remediation.WebApp",
                            ["deployment.environment"] = hostingContext.HostingEnvironment.EnvironmentName,
                            ["service.version"] = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0"
                        };
                    });
                }
            });
    }
}
