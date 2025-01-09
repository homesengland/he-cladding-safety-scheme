namespace HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.KeyDates;

public class KeyDatesResult
{
    public DateTime? StartDate { get; set; }
    
    public DateTime? UnsafeCladdingRemovalDate { get; set; }
    
    public DateTime? ExpectedDateForCompletion { get; set; }
}