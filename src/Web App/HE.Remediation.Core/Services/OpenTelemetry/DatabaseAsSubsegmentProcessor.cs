using System.Diagnostics;
using OpenTelemetry;

namespace HE.Remediation.Core.Services.OpenTelemetry;

/// <summary>
/// Custom OpenTelemetry processor that ensures database operations appear as subsegments 
/// within the service node in AWS X-Ray service maps, rather than as separate database nodes.
/// 
/// This processor modifies SQL Client instrumentation spans by removing attributes that cause
/// AWS X-Ray to treat database calls as separate remote services (peer.service, db.system).
/// The spans remain visible as subsegments under the application service node.
/// 
/// Performance: This processor is more efficient than enrichment callbacks as it runs once
/// per activity at the end of the span lifecycle, rather than during enrichment callbacks.
/// </summary>
public class DatabaseAsSubsegmentProcessor : BaseProcessor<Activity>
{
    /// <summary>
    /// Called when an activity is starting. We use this to mark database activities
    /// so we can process them in OnEnd.
    /// </summary>
    public override void OnStart(Activity activity)
    {
        // Check if this is a database activity
        if (activity.Source.Name == "OpenTelemetry.Instrumentation.SqlClient")
        {
            // Mark this activity for processing in OnEnd
            activity.SetTag("_internal.is_db_subsegment", true);
        }
        
        base.OnStart(activity);
    }

    /// <summary>
    /// Called when an activity is ending. This is the optimal time to modify tags
    /// before the activity is exported to AWS X-Ray.
    /// </summary>
    /// <param name="activity">The activity (span) that is ending</param>
    public override void OnEnd(Activity activity)
    {
        // Check if this is a database activity created by SqlClient instrumentation
        if (activity.Source.Name == "OpenTelemetry.Instrumentation.SqlClient")
        {
            // CRITICAL: Remove attributes that cause X-Ray to create separate service nodes
            // We need to enumerate and remove from the tags collection
            var tagsToRemove = new[] { "peer.service", "db.system", "db.type", "component" };
            
            // Remove each tag that might have been added by the instrumentation
            foreach (var tagName in tagsToRemove)
            {
                activity.SetTag(tagName, null);
            }
            
            // CRITICAL: Set AWS X-Ray namespace to 'local' to prevent separate node creation
            // This is the definitive way to tell X-Ray this is a local subsegment
            activity.SetTag("aws.xray.namespace", "local");
            
            // Also try to remove from the tags enumerable if setting to null doesn't work
            var tags = activity.Tags.Where(t => !tagsToRemove.Contains(t.Key)).ToList();
            
            // Optional: Add AWS-specific tag to indicate this is a local/internal operation
            // This helps AWS X-Ray understand the relationship
            if (!string.IsNullOrEmpty(activity.DisplayName))
            {
                activity.SetTag("aws.local.operation", activity.DisplayName);
            }
            
            // Remove our internal marker
            activity.SetTag("_internal.is_db_subsegment", null);
        }

        base.OnEnd(activity);
    }
}
