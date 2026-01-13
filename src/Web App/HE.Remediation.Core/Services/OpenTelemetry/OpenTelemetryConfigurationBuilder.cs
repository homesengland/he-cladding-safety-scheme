using HE.Remediation.Core.Settings;

namespace HE.Remediation.Core.Services.OpenTelemetry;

#nullable enable
public class OpenTelemetryConfigurationBuilder : IOpenTelemetryConfigurationBuilder
{
    public OpenTelemetryOptions BuildOptions(
        Func<string, string?> configurationGetter,
        Func<string, string?> environmentVariableGetter)
    {
        var options = new OpenTelemetryOptions
        {
            ServiceName = configurationGetter("AWSXRay:ServiceName") ?? "HE.Remediation.WebApp",
            ExportProcessorType = configurationGetter("AWSXRay:ExportProcessorType"),
            SamplingRatio = ParseDouble(environmentVariableGetter("OTEL_TRACES_SAMPLER_ARG"), 0.05),
            OtelExporterEndpoint = environmentVariableGetter("OTEL_EXPORTER_OTLP_ENDPOINT") ?? configurationGetter("OTEL_EXPORTER_OTLP_ENDPOINT"),
            EnableConsoleExporter = ParseBool(configurationGetter("OpenTelemetry:EnableConsoleExporter"), false),
            SetDbStatementForText = ParseBool(configurationGetter("OpenTelemetry:SetDbStatementForText"), false),
            SetDbStatementForStoredProcedure = ParseBool(configurationGetter("OpenTelemetry:SetDbStatementForStoredProcedure"), false),
            ResourceAttributes = ParseResourceAttributes(environmentVariableGetter("OTEL_RESOURCE_ATTRIBUTES")) ?? ParseResourceAttributes(configurationGetter("AWSXRay:ResourceAttributes")),
            
            // Enhanced SQL instrumentation options
            EnableSqlFiltering = ParseBool(configurationGetter("OpenTelemetry:EnableSqlFiltering"), false),
            EnableEnhancedSqlEnrichment = ParseBool(configurationGetter("OpenTelemetry:EnableEnhancedSqlEnrichment"), true),
            EnableSqlMetrics = ParseBool(configurationGetter("OpenTelemetry:EnableSqlMetrics"), false),
            SqlFilterExcludePatterns = ParseStringArray(configurationGetter("OpenTelemetry:SqlFilterExcludePatterns"))
        };

        return options;
    }

    private static double ParseDouble(string? value, double defaultValue)
    {
        if (string.IsNullOrWhiteSpace(value))
            return defaultValue;

        return double.TryParse(value, out var result) ? result : defaultValue;
    }

    private static bool ParseBool(string? value, bool defaultValue)
    {
        if (string.IsNullOrWhiteSpace(value))
            return defaultValue;

        return bool.TryParse(value, out var result) ? result : defaultValue;
    }

    private static Dictionary<string, string> ParseResourceAttributes(string? resourceAttributesString)
    {
        var attributes = new Dictionary<string, string>();

        if (string.IsNullOrWhiteSpace(resourceAttributesString))
            return attributes;

        var attributePairs = resourceAttributesString.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (var attribute in attributePairs)
        {
            var attributeParts = attribute.Split('=', 2);

            if (attributeParts.Length == 2)
            {
                var key = attributeParts[0].Trim();

                if (!string.IsNullOrEmpty(key))
                {
                    attributes[key] = attributeParts[1].Trim();
                }
            }
        }

        return attributes;
    }

    private static string[]? ParseStringArray(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        return value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }
}
#nullable disable