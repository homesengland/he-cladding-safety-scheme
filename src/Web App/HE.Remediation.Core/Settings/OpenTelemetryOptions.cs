namespace HE.Remediation.Core.Settings;

#nullable enable
public class OpenTelemetryOptions
{
    // Service Configuration
    public string ServiceName { get; set; } = "HE.Remediation.WebApp";
    public string? ExportProcessorType { get; set; }
    public double SamplingRatio { get; set; } = 1.0;
    public string? OtelExporterEndpoint { get; set; }
    public Dictionary<string, string> ResourceAttributes { get; set; } = new();
    public bool EnableConsoleExporter { get; set; }
    
    // SQL Instrumentation Configuration
    public bool SetDbStatementForText { get; set; } = true;
    public bool SetDbStatementForStoredProcedure { get; set; } = true;
    public bool EnableSqlFiltering { get; set; } = false;
    public bool EnableEnhancedSqlEnrichment { get; set; } = true;
    public bool EnableSqlMetrics { get; set; } = false;
    public string[]? SqlFilterExcludePatterns { get; set; }
    public bool SuppressSqlInstrumentation { get; set; } = false;
    
    // Logging Configuration
    public bool EnableLogging { get; set; } = true;
    public bool ExportLogsToOtlp { get; set; } = true;
    public string LogLevel { get; set; } = "Information";
}
#nullable disable
