using HE.Remediation.Core.Services.OpenTelemetry;

namespace HE.Remediation.Core.Tests.Extensions;

public class OpenTelemetryConfigurationBuilderTests : TestBase
{
    private readonly OpenTelemetryConfigurationBuilder _builder;

    public OpenTelemetryConfigurationBuilderTests()
    {
        _builder = new OpenTelemetryConfigurationBuilder();
    }

    [Fact]
    public void BuildOptions_WithAllValidSettings_ReturnsCompleteOptions()
    {
        // Arrange
        var configValues = new Dictionary<string, string?>
        {
            ["AWSXRay:ServiceName"] = "TestService",
            ["AWSXRay:ExportProcessorType"] = "Batch",
            ["OpenTelemetry:EnableConsoleExporter"] = "true"
        };

        var envValues = new Dictionary<string, string?>
        {
            ["OTEL_EXPORTER_OTLP_ENDPOINT"] = "http://localhost:4317",
            ["OTEL_TRACES_SAMPLER_ARG"] = "0.5",
            ["OTEL_RESOURCE_ATTRIBUTES"] = "deployment.environment=production,service.version=1.0.0"
        };

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal("TestService", result.ServiceName);
        Assert.Equal("Batch", result.ExportProcessorType);
        Assert.Equal(0.5, result.SamplingRatio);
        Assert.Equal("http://localhost:4317", result.OtelExporterEndpoint);
        Assert.True(result.EnableConsoleExporter);
        Assert.Equal(2, result.ResourceAttributes.Count);
        Assert.Equal("production", result.ResourceAttributes["deployment.environment"]);
        Assert.Equal("1.0.0", result.ResourceAttributes["service.version"]);
    }

    [Fact]
    public void BuildOptions_WithMissingServiceName_UsesDefaultValue()
    {
        // Arrange
        var configValues = new Dictionary<string, string?>();
        var envValues = new Dictionary<string, string?>();

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.Equal("HE.Remediation.WebApp", result.ServiceName);
    }

    [Fact]
    public void BuildOptions_WithNullServiceName_UsesDefaultValue()
    {
        // Arrange
        var configValues = new Dictionary<string, string?>
        {
            ["AWSXRay:ServiceName"] = null
        };
        var envValues = new Dictionary<string, string?>();

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.Equal("HE.Remediation.WebApp", result.ServiceName);
    }

    [Fact]
    public void BuildOptions_WithMissingSamplingRatio_UsesDefaultValue()
    {
        // Arrange
        var configValues = new Dictionary<string, string?>();
        var envValues = new Dictionary<string, string?>();

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.Equal(0.05, result.SamplingRatio);
    }

    [Fact]
    public void BuildOptions_WithInvalidSamplingRatio_UsesDefaultValue()
    {
        // Arrange
        var configValues = new Dictionary<string, string?>();
        var envValues = new Dictionary<string, string?>
        {
            ["OTEL_TRACES_SAMPLER_ARG"] = "invalid"
        };

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.Equal(0.05, result.SamplingRatio);
    }

    [Fact]
    public void BuildOptions_WithZeroSamplingRatio_UsesZero()
    {
        // Arrange
        var configValues = new Dictionary<string, string?>();
        var envValues = new Dictionary<string, string?>
        {
            ["OTEL_TRACES_SAMPLER_ARG"] = "0"
        };

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.Equal(0.0, result.SamplingRatio);
    }

    [Fact]
    public void BuildOptions_WithMissingConsoleExporter_ReturnsFalse()
    {
        // Arrange
        var configValues = new Dictionary<string, string?>();
        var envValues = new Dictionary<string, string?>();

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.False(result.EnableConsoleExporter);
    }

    [Fact]
    public void BuildOptions_WithInvalidConsoleExporter_ReturnsFalse()
    {
        // Arrange
        var configValues = new Dictionary<string, string?>
        {
            ["OpenTelemetry:EnableConsoleExporter"] = "not-a-bool"
        };
        var envValues = new Dictionary<string, string?>();

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.False(result.EnableConsoleExporter);
    }

    [Theory]
    [InlineData("true", true)]
    [InlineData("True", true)]
    [InlineData("TRUE", true)]
    [InlineData("false", false)]
    [InlineData("False", false)]
    [InlineData("FALSE", false)]
    public void BuildOptions_WithVariousConsoleExporterValues_ParsesCorrectly(string value, bool expected)
    {
        // Arrange
        var configValues = new Dictionary<string, string?>
        {
            ["OpenTelemetry:EnableConsoleExporter"] = value
        };
        var envValues = new Dictionary<string, string?>();

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.Equal(expected, result.EnableConsoleExporter);
    }

    [Fact]
    public void BuildOptions_WithMissingEndpoint_ReturnsNullEndpoint()
    {
        // Arrange
        var configValues = new Dictionary<string, string?>();
        var envValues = new Dictionary<string, string?>();

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.Null(result.OtelExporterEndpoint);
    }

    [Fact]
    public void BuildOptions_WithEmptyResourceAttributes_ReturnsEmptyDictionary()
    {
        // Arrange
        var configValues = new Dictionary<string, string?>
        {
            ["AWSXRay:ResourceAttributes"] = ""
        };
        var envValues = new Dictionary<string, string?>();

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.NotNull(result.ResourceAttributes);
        Assert.Empty(result.ResourceAttributes);
    }

    [Fact]
    public void BuildOptions_WithNullResourceAttributes_ReturnsEmptyDictionary()
    {
        // Arrange
        var configValues = new Dictionary<string, string?>
        {
            ["AWSXRay:ResourceAttributes"] = null
        };
        var envValues = new Dictionary<string, string?>();

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.NotNull(result.ResourceAttributes);
        Assert.Empty(result.ResourceAttributes);
    }

    [Fact]
    public void BuildOptions_WithSingleResourceAttribute_ParsesCorrectly()
    {
        // Arrange
        var configValues = new Dictionary<string, string?>();
        var envValues = new Dictionary<string, string?>
        {
            ["OTEL_RESOURCE_ATTRIBUTES"] = "environment=test"
        };

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.Single(result.ResourceAttributes);
        Assert.Equal("test", result.ResourceAttributes["environment"]);
    }

    [Fact]
    public void BuildOptions_WithMultipleResourceAttributes_ParsesAllCorrectly()
    {
        // Arrange
        var configValues = new Dictionary<string, string?>();
        var envValues = new Dictionary<string, string?>
        {
            ["OTEL_RESOURCE_ATTRIBUTES"] = "env=prod,version=2.0,region=us-east-1"
        };

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.Equal(3, result.ResourceAttributes.Count);
        Assert.Equal("prod", result.ResourceAttributes["env"]);
        Assert.Equal("2.0", result.ResourceAttributes["version"]);
        Assert.Equal("us-east-1", result.ResourceAttributes["region"]);
    }

    [Fact]
    public void BuildOptions_WithResourceAttributesWithSpaces_TrimsCorrectly()
    {
        // Arrange
        var configValues = new Dictionary<string, string?>();
        var envValues = new Dictionary<string, string?>
        {
            ["OTEL_RESOURCE_ATTRIBUTES"] = "  key1  =  value1  ,  key2  =  value2  "
        };

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.Equal(2, result.ResourceAttributes.Count);
        Assert.Equal("value1", result.ResourceAttributes["key1"]);
        Assert.Equal("value2", result.ResourceAttributes["key2"]);
    }

    [Fact]
    public void BuildOptions_WithResourceAttributeWithEqualsInValue_ParsesCorrectly()
    {
        // Arrange
        var configValues = new Dictionary<string, string?>();
        var envValues = new Dictionary<string, string?>
        {
            ["OTEL_RESOURCE_ATTRIBUTES"] = "url=https://example.com?param=value"
        };

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.Single(result.ResourceAttributes);
        Assert.Equal("https://example.com?param=value", result.ResourceAttributes["url"]);
    }

    [Fact]
    public void BuildOptions_WithMalformedResourceAttribute_SkipsInvalidEntry()
    {
        // Arrange
        var configValues = new Dictionary<string, string?>();
        var envValues = new Dictionary<string, string?>
        {
            ["OTEL_RESOURCE_ATTRIBUTES"] = "validkey=validvalue,invalidentry,anotherkey=anothervalue"
        };

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.Equal(2, result.ResourceAttributes.Count);
        Assert.Equal("validvalue", result.ResourceAttributes["validkey"]);
        Assert.Equal("anothervalue", result.ResourceAttributes["anotherkey"]);
    }

    [Fact]
    public void BuildOptions_WithEmptyKeyInResourceAttribute_SkipsEntry()
    {
        // Arrange
        var configValues = new Dictionary<string, string?>();
        var envValues = new Dictionary<string, string?>
        {
            ["OTEL_RESOURCE_ATTRIBUTES"] = "=value,validkey=validvalue"
        };

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.Single(result.ResourceAttributes);
        Assert.Equal("validvalue", result.ResourceAttributes["validkey"]);
    }

    [Fact]
    public void BuildOptions_WithWhitespaceOnlyKeyInResourceAttribute_SkipsEntry()
    {
        // Arrange
        var configValues = new Dictionary<string, string?>();
        var envValues = new Dictionary<string, string?>
        {
            ["OTEL_RESOURCE_ATTRIBUTES"] = "   =value,validkey=validvalue"
        };

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.Single(result.ResourceAttributes);
        Assert.Equal("validvalue", result.ResourceAttributes["validkey"]);
    }

    [Theory]
    [InlineData("0.05")]
    [InlineData("0.1")]
    [InlineData("0.25")]
    [InlineData("0.5")]
    [InlineData("0.75")]
    [InlineData("1.0")]
    public void BuildOptions_WithVariousSamplingRatios_ParsesCorrectly(string samplingRatio)
    {
        // Arrange
        var configValues = new Dictionary<string, string?>();
        var envValues = new Dictionary<string, string?>
        {
            ["OTEL_TRACES_SAMPLER_ARG"] = samplingRatio
        };

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.Equal(double.Parse(samplingRatio), result.SamplingRatio);
    }

    [Theory]
    [InlineData("Batch")]
    [InlineData("Simple")]
    [InlineData(null)]
    [InlineData("")]
    public void BuildOptions_WithVariousExportProcessorTypes_StoresCorrectly(string? processorType)
    {
        // Arrange
        var configValues = new Dictionary<string, string?>
        {
            ["AWSXRay:ExportProcessorType"] = processorType
        };
        var envValues = new Dictionary<string, string?>();

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.Equal(processorType, result.ExportProcessorType);
    }

    [Fact]
    public void BuildOptions_WithComplexScenario_ParsesAllFieldsCorrectly()
    {
        // Arrange
        var configValues = new Dictionary<string, string?>
        {
            ["AWSXRay:ServiceName"] = "MyComplexService",
            ["AWSXRay:ExportProcessorType"] = "Simple",
            ["OpenTelemetry:EnableConsoleExporter"] = "false"
        };

        var envValues = new Dictionary<string, string?>
        {
            ["OTEL_EXPORTER_OTLP_ENDPOINT"] = "https://otel-collector.example.com:4317",
            ["OTEL_TRACES_SAMPLER_ARG"] = "0.15",
            ["OTEL_RESOURCE_ATTRIBUTES"] = "deployment.environment=staging,service.version=3.2.1,datacenter=eu-west,team=platform"
        };

        // Act
        var result = _builder.BuildOptions(
            key => configValues.GetValueOrDefault(key),
            key => envValues.GetValueOrDefault(key)
        );

        // Assert
        Assert.Equal("MyComplexService", result.ServiceName);
        Assert.Equal("Simple", result.ExportProcessorType);
        Assert.Equal(0.15, result.SamplingRatio);
        Assert.Equal("https://otel-collector.example.com:4317", result.OtelExporterEndpoint);
        Assert.False(result.EnableConsoleExporter);
        Assert.Equal(4, result.ResourceAttributes.Count);
        Assert.Equal("staging", result.ResourceAttributes["deployment.environment"]);
        Assert.Equal("3.2.1", result.ResourceAttributes["service.version"]);
        Assert.Equal("eu-west", result.ResourceAttributes["datacenter"]);
        Assert.Equal("platform", result.ResourceAttributes["team"]);
    }
}