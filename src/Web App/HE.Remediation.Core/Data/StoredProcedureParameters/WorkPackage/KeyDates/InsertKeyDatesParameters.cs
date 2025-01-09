namespace HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.KeyDates;

public class InsertKeyDatesParameters
{
    public DateTime? StartDate { get; set; }
    
    public DateTime? UnsafeCladdingRemovalDate { get; set; }
    
    public DateTime? ExpectedDateForCompletion { get; set; }
}