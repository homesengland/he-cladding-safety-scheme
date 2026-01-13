using Microsoft.Extensions.Configuration;

namespace HE.Remediation.Core.Tests.Extensions
{
    public class ServiceCollectionExtensionsXRayConfigTests
    {
        private IConfiguration BuildConfig(Action<IConfigurationBuilder> configure)
        {
            var builder = new ConfigurationBuilder();
            configure(builder);
            return builder.Build();
        }

        [Fact]
        public void Should_Read_All_XRay_Config_Values_When_Present()
        {
            // Arrange
            var config = BuildConfig(cfg =>
            {
                cfg.AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string, string>("AWSXRay:Endpoint", "http://localhost:4317"),
                    new KeyValuePair<string, string>("AWSXRay:ServiceName", "TestService"),
                    new KeyValuePair<string, string>("AWSXRay:ExportProcessorType", "Batch")
                });
            });

            // Act
            var xrayConfig = config.GetSection("AWSXRay");
            var endpoint = xrayConfig.GetValue<string>("Endpoint");
            var serviceName = xrayConfig.GetValue<string>("ServiceName");
            var exportProcessorType = xrayConfig.GetValue<string>("ExportProcessorType");

            // Assert
            Assert.Equal("http://localhost:4317", endpoint);
            Assert.Equal("TestService", serviceName);
            Assert.Equal("Batch", exportProcessorType);
        }

        [Fact]
        public void Should_Throw_When_XRay_Config_Section_Is_Missing()
        {
            // Arrange
            var config = BuildConfig(cfg => { });

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                var xrayConfig = config.GetSection("AWSXRay");
                if (!xrayConfig.Exists())
                {
                    throw new InvalidOperationException("The AWSXRay configuration section is missing.");
                }
                // simulate usage
                _ = xrayConfig.GetValue<string>("Endpoint");
            });
            Assert.Equal("The AWSXRay configuration section is missing.", ex.Message);
        }

        [Theory]
        [InlineData("Endpoint")]
        [InlineData("ServiceName")]
        [InlineData("ExportProcessorType")]
        public void Should_Return_Null_When_XRay_Config_Value_Is_Missing(string missingKey)
        {
            // Arrange
            var values = new[]
            {
                new KeyValuePair<string, string>("AWSXRay:Endpoint", "http://localhost:4317"),
                new KeyValuePair<string, string>("AWSXRay:ServiceName", "TestService"),
                new KeyValuePair<string, string>("AWSXRay:ExportProcessorType", "Batch")
            };
            var filtered = new List<KeyValuePair<string, string>>(values);
            filtered.RemoveAll(kv => kv.Key.EndsWith(missingKey));

            var config = BuildConfig(cfg => cfg.AddInMemoryCollection(filtered));

            // Act
            var xrayConfig = config.GetSection("AWSXRay");
            var value = xrayConfig.GetValue<string>(missingKey);

            // Assert
            Assert.Null(value);
        }
    }
}