using OpenTelemetry.Trace;

namespace HE.Remediation.Core.Services.OpenTelemetry;

/// <summary>
/// Custom sampler that applies parent-based and ratio-based sampling.
/// Note: Exception capture is handled by RecordException=true in SQL instrumentation,
/// not by sampling logic (since sampling decisions are made before exceptions occur).
/// </summary>
public class CustomSqlSampler : Sampler
{
    private readonly Sampler _rootSampler;

    public CustomSqlSampler(double samplingRatio)
    {
        _rootSampler = new ParentBasedSampler(new TraceIdRatioBasedSampler(samplingRatio));
    }

    public override SamplingResult ShouldSample(in SamplingParameters samplingParameters)
    {
        return _rootSampler.ShouldSample(samplingParameters);
    }
}

