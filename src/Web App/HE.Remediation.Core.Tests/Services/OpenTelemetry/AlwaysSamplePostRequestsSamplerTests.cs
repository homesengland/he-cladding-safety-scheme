using System.Diagnostics;
using HE.Remediation.Core.Services.OpenTelemetry;
using OpenTelemetry.Trace;
using Xunit;

namespace HE.Remediation.Core.Tests.Services.OpenTelemetry;

public class AlwaysSamplePostRequestsSamplerTests
{
    [Fact]
    public void ShouldSample_PostRequest_ReturnsRecordAndSample()
    {
        // Arrange
        var sampler = new AlwaysSamplePostRequestsSampler(0.1); // 10% ratio for non-POST
        var samplingParameters = CreateSamplingParameters("POST");

        // Act
        var result = sampler.ShouldSample(samplingParameters);

        // Assert
        Assert.Equal(SamplingDecision.RecordAndSample, result.Decision);
    }

    [Fact]
    public void ShouldSample_PostRequestLowerCase_ReturnsRecordAndSample()
    {
        // Arrange
        var sampler = new AlwaysSamplePostRequestsSampler(0.1);
        var samplingParameters = CreateSamplingParameters("post");

        // Act
        var result = sampler.ShouldSample(samplingParameters);

        // Assert
        Assert.Equal(SamplingDecision.RecordAndSample, result.Decision);
    }

    [Fact]
    public void ShouldSample_PostRequestMixedCase_ReturnsRecordAndSample()
    {
        // Arrange
        var sampler = new AlwaysSamplePostRequestsSampler(0.1);
        var samplingParameters = CreateSamplingParameters("PoSt");

        // Act
        var result = sampler.ShouldSample(samplingParameters);

        // Assert
        Assert.Equal(SamplingDecision.RecordAndSample, result.Decision);
    }

    [Fact]
    public void ShouldSample_GetRequest_UsesDefaultSampler()
    {
        // Arrange
        var sampler = new AlwaysSamplePostRequestsSampler(1.0); // 100% ratio - always sample
        var samplingParameters = CreateSamplingParameters("GET");

        // Act
        var result = sampler.ShouldSample(samplingParameters);

        // Assert
        Assert.Equal(SamplingDecision.RecordAndSample, result.Decision);
    }

    [Fact]
    public void ShouldSample_GetRequestWithLowRatio_MayDrop()
    {
        // Arrange
        var sampler = new AlwaysSamplePostRequestsSampler(0.0); // 0% ratio - never sample
        var samplingParameters = CreateSamplingParameters("GET");

        // Act
        var result = sampler.ShouldSample(samplingParameters);

        // Assert
        Assert.Equal(SamplingDecision.Drop, result.Decision);
    }

    [Fact]
    public void ShouldSample_PutRequest_UsesDefaultSampler()
    {
        // Arrange
        var sampler = new AlwaysSamplePostRequestsSampler(1.0);
        var samplingParameters = CreateSamplingParameters("PUT");

        // Act
        var result = sampler.ShouldSample(samplingParameters);

        // Assert
        Assert.Equal(SamplingDecision.RecordAndSample, result.Decision);
    }

    [Fact]
    public void ShouldSample_DeleteRequest_UsesDefaultSampler()
    {
        // Arrange
        var sampler = new AlwaysSamplePostRequestsSampler(1.0);
        var samplingParameters = CreateSamplingParameters("DELETE");

        // Act
        var result = sampler.ShouldSample(samplingParameters);

        // Assert
        Assert.Equal(SamplingDecision.RecordAndSample, result.Decision);
    }

    [Fact]
    public void ShouldSample_PatchRequest_UsesDefaultSampler()
    {
        // Arrange
        var sampler = new AlwaysSamplePostRequestsSampler(0.0);
        var samplingParameters = CreateSamplingParameters("PATCH");

        // Act
        var result = sampler.ShouldSample(samplingParameters);

        // Assert
        Assert.Equal(SamplingDecision.Drop, result.Decision);
    }

    [Fact]
    public void ShouldSample_NoHttpMethodTag_UsesDefaultSampler()
    {
        // Arrange
        var sampler = new AlwaysSamplePostRequestsSampler(1.0);
        var samplingParameters = CreateSamplingParametersWithoutMethod();

        // Act
        var result = sampler.ShouldSample(samplingParameters);

        // Assert
        Assert.Equal(SamplingDecision.RecordAndSample, result.Decision);
    }

    [Fact]
    public void ShouldSample_PostRequestRegardlessOfRatio_AlwaysSamples()
    {
        // Arrange - Even with 0% ratio, POST should always be sampled
        var sampler = new AlwaysSamplePostRequestsSampler(0.0);
        var samplingParameters = CreateSamplingParameters("POST");

        // Act
        var result = sampler.ShouldSample(samplingParameters);

        // Assert
        Assert.Equal(SamplingDecision.RecordAndSample, result.Decision);
    }

    [Fact]
    public void ShouldSample_MultiplePostRequests_AllSampled()
    {
        // Arrange
        var sampler = new AlwaysSamplePostRequestsSampler(0.05); // 5% ratio

        // Act & Assert - Multiple POST requests should all be sampled
        for (int i = 0; i < 100; i++)
        {
            var samplingParameters = CreateSamplingParameters("POST");
            var result = sampler.ShouldSample(samplingParameters);
            Assert.Equal(SamplingDecision.RecordAndSample, result.Decision);
        }
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(0.1)]
    [InlineData(0.5)]
    [InlineData(1.0)]
    public void ShouldSample_PostRequestWithVariousRatios_AlwaysSamples(double ratio)
    {
        // Arrange
        var sampler = new AlwaysSamplePostRequestsSampler(ratio);
        var samplingParameters = CreateSamplingParameters("POST");

        // Act
        var result = sampler.ShouldSample(samplingParameters);

        // Assert
        Assert.Equal(SamplingDecision.RecordAndSample, result.Decision);
    }

    [Fact]
    public void Description_ContainsCustomSamplerInfo()
    {
        // Arrange
        var sampler = new AlwaysSamplePostRequestsSampler(0.1);

        // Act
        var description = sampler.Description;

        // Assert
        Assert.Contains("AlwaysSamplePostRequests", description);
    }

    private SamplingParameters CreateSamplingParameters(string httpMethod)
    {
        var activityContext = new ActivityContext(
            ActivityTraceId.CreateRandom(),
            ActivitySpanId.CreateRandom(),
            ActivityTraceFlags.None);

        var tags = new List<KeyValuePair<string, object>>
        {
            new KeyValuePair<string, object>("http.method", httpMethod)
        };

        return new SamplingParameters(
            parentContext: activityContext,
            traceId: activityContext.TraceId,
            name: "TestActivity",
            kind: ActivityKind.Server,
            tags: tags,
            links: null);
    }

    private SamplingParameters CreateSamplingParametersWithoutMethod()
    {
        var activityContext = new ActivityContext(
            ActivityTraceId.CreateRandom(),
            ActivitySpanId.CreateRandom(),
            ActivityTraceFlags.None);

        var tags = new List<KeyValuePair<string, object>>
        {
            new KeyValuePair<string, object>("other.tag", "value")
        };

        return new SamplingParameters(
            parentContext: activityContext,
            traceId: activityContext.TraceId,
            name: "TestActivity",
            kind: ActivityKind.Server,
            tags: tags,
            links: null);
    }
}
