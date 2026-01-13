using HE.Remediation.Core.Settings;

namespace HE.Remediation.Core.Services.OpenTelemetry;

#nullable enable
public interface IOpenTelemetryConfigurationBuilder
{
    OpenTelemetryOptions BuildOptions(Func<string, string?> configurationGetter, Func<string, string?> environmentVariableGetter);
}
#nullable disable
