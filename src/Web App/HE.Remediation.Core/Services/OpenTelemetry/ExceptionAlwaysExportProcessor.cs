using OpenTelemetry;
using System.Diagnostics;

namespace HE.Remediation.Core.Services.OpenTelemetry;

#nullable enable
/// <summary>
/// A processor that adds additional tags to activities with exceptions for easier filtering.
/// Note: Exception details are already captured by RecordException=true in instrumentation options.
/// This processor adds convenience tags for querying and filtering exception traces.
/// </summary>
public class ExceptionAlwaysExportProcessor : BaseProcessor<Activity>
{
    public override void OnEnd(Activity activity)
    {
        if (activity.Status == ActivityStatusCode.Error || 
            activity.Events.Any(e => e.Name == "exception"))
        {
            activity.SetTag("has.exception", true);

            var exceptionCount = activity.Events.Count(e => e.Name == "exception");
            if (exceptionCount > 0)
            {
                activity.SetTag("exception.count", exceptionCount);
            }
        }
        
        base.OnEnd(activity);
    }
}
#nullable disable
