using OpenTelemetry.Trace;

namespace HE.Remediation.Core.Services.OpenTelemetry;

/// <summary>
/// Custom sampler that always samples POST requests while using ratio-based sampling for other methods
/// </summary>
public class AlwaysSamplePostRequestsSampler : Sampler
{
    private readonly Sampler _defaultSampler;

    public AlwaysSamplePostRequestsSampler(double samplingRatio)
    {
        _defaultSampler = new TraceIdRatioBasedSampler(samplingRatio);
    }

    public new string Description => $"AlwaysSamplePostRequestsSampler({_defaultSampler.Description})";

    public override SamplingResult ShouldSample(in SamplingParameters samplingParameters)
    {       
        var httpMethodTag = samplingParameters.Tags?.FirstOrDefault(t => t.Key == "http.method");
        
        if (httpMethodTag?.Value?.ToString()?.Equals("POST", StringComparison.OrdinalIgnoreCase) == true)
        {
            return new SamplingResult(SamplingDecision.RecordAndSample);
        }
        
        return _defaultSampler.ShouldSample(samplingParameters);
    }
}
