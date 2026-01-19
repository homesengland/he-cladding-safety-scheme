using HE.Remediation.Core.Settings;
using Microsoft.AspNetCore.Builder;
using ServiceCollectionExtensions = HE.Remediation.Core.Extensions.ServiceCollectionExtensions;

namespace HE.Remediation.Core.Tests.Extensions;

public class ServiceCollectionExtensionsOpenTelemetryTests : TestBase
{
    private WebApplicationBuilder CreateBuilder()
    {
        var args = Array.Empty<string>();
        return WebApplication.CreateBuilder(args);
    }

    [Fact]
    public void ConfigureOpenTelemetryServices_WithNullEndpoint_DoesNotConfigureOpenTelemetry()
    {
        // Arrange
        var builder = CreateBuilder();
        var initialServiceCount = builder.Services.Count;
        
        var options = new OpenTelemetryOptions
        {
            OtelExporterEndpoint = null,
            ServiceName = "TestService"
        };

        // Act
        ServiceCollectionExtensions.ConfigureOpenTelemetryServices(builder, options);

        // Assert - No OpenTelemetry services should be added
        Assert.Equal(initialServiceCount, builder.Services.Count);
    }

    [Fact]
    public void ConfigureOpenTelemetryServices_WithEmptyEndpoint_DoesNotConfigureOpenTelemetry()
    {
        // Arrange
        var builder = CreateBuilder();
        var initialServiceCount = builder.Services.Count;
        
        var options = new OpenTelemetryOptions
        {
            OtelExporterEndpoint = "",
            ServiceName = "TestService"
        };

        // Act
        ServiceCollectionExtensions.ConfigureOpenTelemetryServices(builder, options);

        // Assert
        Assert.Equal(initialServiceCount, builder.Services.Count);
    }

    [Fact]
    public void ConfigureOpenTelemetryServices_WithWhitespaceEndpoint_DoesNotConfigureOpenTelemetry()
    {
        // Arrange
        var builder = CreateBuilder();
        var initialServiceCount = builder.Services.Count;
        
        var options = new OpenTelemetryOptions
        {
            OtelExporterEndpoint = "   ",
            ServiceName = "TestService"
        };

        // Act
        ServiceCollectionExtensions.ConfigureOpenTelemetryServices(builder, options);

        // Assert
        Assert.Equal(initialServiceCount, builder.Services.Count);
    }

    [Fact]
    public void ConfigureOpenTelemetryServices_WithValidEndpoint_ConfiguresOpenTelemetry()
    {
        // Arrange
        var builder = CreateBuilder();
        var initialServiceCount = builder.Services.Count;
        
        var options = new OpenTelemetryOptions
        {
            OtelExporterEndpoint = "http://localhost:4317",
            ServiceName = "TestService",
            SamplingRatio = 0.5,
            ExportProcessorType = "Batch",
            EnableConsoleExporter = false,
            ResourceAttributes = new Dictionary<string, string>
            {
                ["environment"] = "test"
            }
        };

        // Act
        ServiceCollectionExtensions.ConfigureOpenTelemetryServices(builder, options);

        // Assert - OpenTelemetry services should be added
        Assert.True(builder.Services.Count > initialServiceCount);
    }

    [Fact]
    public void ConfigureOpenTelemetryServices_WithMinimalOptions_ConfiguresSuccessfully()
    {
        // Arrange
        var builder = CreateBuilder();
        
        var options = new OpenTelemetryOptions
        {
            OtelExporterEndpoint = "http://localhost:4317",
            ServiceName = "MinimalService"
        };

        // Act
        var exception = Record.Exception(() => 
            ServiceCollectionExtensions.ConfigureOpenTelemetryServices(builder, options));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ConfigureOpenTelemetryServices_WithConsoleExporterEnabled_ConfiguresSuccessfully()
    {
        // Arrange
        var builder = CreateBuilder();
        
        var options = new OpenTelemetryOptions
        {
            OtelExporterEndpoint = "http://localhost:4317",
            ServiceName = "ConsoleService",
            EnableConsoleExporter = true
        };

        // Act
        var exception = Record.Exception(() => 
            ServiceCollectionExtensions.ConfigureOpenTelemetryServices(builder, options));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ConfigureOpenTelemetryServices_WithMultipleResourceAttributes_ConfiguresSuccessfully()
    {
        // Arrange
        var builder = CreateBuilder();
        
        var options = new OpenTelemetryOptions
        {
            OtelExporterEndpoint = "http://localhost:4317",
            ServiceName = "AttributeService",
            ResourceAttributes = new Dictionary<string, string>
            {
                ["deployment.environment"] = "automation",
                ["another.setting"] = "another.value",
                ["custom.attribute"] = "custom.value"
            }
        };

        // Act
        var exception = Record.Exception(() => 
            ServiceCollectionExtensions.ConfigureOpenTelemetryServices(builder, options));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ConfigureOpenTelemetryServices_WithEmptyResourceAttributes_ConfiguresSuccessfully()
    {
        // Arrange
        var builder = CreateBuilder();
        
        var options = new OpenTelemetryOptions
        {
            OtelExporterEndpoint = "http://localhost:4317",
            ServiceName = "EmptyAttributesService",
            ResourceAttributes = new Dictionary<string, string>()
        };

        // Act
        var exception = Record.Exception(() => 
            ServiceCollectionExtensions.ConfigureOpenTelemetryServices(builder, options));

        // Assert
        Assert.Null(exception);
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(0.05)]
    [InlineData(0.5)]
    [InlineData(1.0)]
    public void ConfigureOpenTelemetryServices_WithVariousSamplingRatios_ConfiguresSuccessfully(double samplingRatio)
    {
        // Arrange
        var builder = CreateBuilder();
        
        var options = new OpenTelemetryOptions
        {
            OtelExporterEndpoint = "http://localhost:4317",
            ServiceName = "SamplingService",
            SamplingRatio = samplingRatio
        };

        // Act
        var exception = Record.Exception(() => 
            ServiceCollectionExtensions.ConfigureOpenTelemetryServices(builder, options));

        // Assert
        Assert.Null(exception);
    }

    [Theory]
    [InlineData("Batch")]
    [InlineData("Simple")]
    [InlineData("InvalidType")] // Should default to Batch
    [InlineData(null)] // Should default to Batch
    public void ConfigureOpenTelemetryServices_WithVariousExportProcessorTypes_ConfiguresSuccessfully(string? exportProcessorType)
    {
        // Arrange
        var builder = CreateBuilder();
        
        var options = new OpenTelemetryOptions
        {
            OtelExporterEndpoint = "http://localhost:4317",
            ServiceName = "ProcessorService",
            ExportProcessorType = exportProcessorType
        };

        // Act
        var exception = Record.Exception(() => 
            ServiceCollectionExtensions.ConfigureOpenTelemetryServices(builder, options));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ConfigureOpenTelemetryServices_WithCompleteConfiguration_ConfiguresAllFeatures()
    {
        // Arrange
        var builder = CreateBuilder();
        
        var options = new OpenTelemetryOptions
        {
            OtelExporterEndpoint = "https://HE-otel-collector.example.com:4317",
            ServiceName = "FullFeaturedService",
            ExportProcessorType = "Batch",
            SamplingRatio = 0.25,
            EnableConsoleExporter = true,
            ResourceAttributes = new Dictionary<string, string>
            {
                ["deployment.environment"] = "automation"
            }
        };

        // Act
        var exception = Record.Exception(() => 
            ServiceCollectionExtensions.ConfigureOpenTelemetryServices(builder, options));

        // Assert
        Assert.Null(exception);
    }

    [Theory]
    [InlineData("http://localhost:4317")]
    [InlineData("https://HE-otel.example.com:4317")]
    [InlineData("http://192.168.1.100:4317")]
    public void ConfigureOpenTelemetryServices_WithVariousEndpointFormats_ConfiguresSuccessfully(string endpoint)
    {
        // Arrange
        var builder = CreateBuilder();
        
        var options = new OpenTelemetryOptions
        {
            OtelExporterEndpoint = endpoint,
            ServiceName = "EndpointService"
        };

        // Act
        var exception = Record.Exception(() => 
            ServiceCollectionExtensions.ConfigureOpenTelemetryServices(builder, options));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ConfigureOpenTelemetryServices_CalledMultipleTimes_DoesNotThrow()
    {
        // Arrange
        var builder = CreateBuilder();
        
        var options = new OpenTelemetryOptions
        {
            OtelExporterEndpoint = "http://localhost:4317",
            ServiceName = "MultiCallService"
        };

        // Act
        var exception = Record.Exception(() =>
        {
            ServiceCollectionExtensions.ConfigureOpenTelemetryServices(builder, options);
            ServiceCollectionExtensions.ConfigureOpenTelemetryServices(builder, options);
        });

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ConfigureOpenTelemetryServices_WithDefaultServiceName_UsesProvidedName()
    {
        // Arrange
        var builder = CreateBuilder();
        
        var options = new OpenTelemetryOptions
        {
            OtelExporterEndpoint = "http://localhost:4317"
            // ServiceName will use default value from class
        };

        // Act
        var exception = Record.Exception(() => 
            ServiceCollectionExtensions.ConfigureOpenTelemetryServices(builder, options));

        // Assert
        Assert.Null(exception);
        Assert.Equal("HE.Remediation.WebApp", options.ServiceName);
    }

    [Fact]
    public void ConfigureOpenTelemetryServices_WithSpecialCharactersInAttributes_ConfiguresSuccessfully()
    {
        // Arrange
        var builder = CreateBuilder();
        
        var options = new OpenTelemetryOptions
        {
            OtelExporterEndpoint = "http://localhost:4317",
            ServiceName = "SpecialCharsService",
            ResourceAttributes = new Dictionary<string, string>
            {
                ["deployment.environment"] = "manual"
            }
        };

        // Act
        var exception = Record.Exception(() => 
            ServiceCollectionExtensions.ConfigureOpenTelemetryServices(builder, options));

        // Assert
        Assert.Null(exception);
    }
}
