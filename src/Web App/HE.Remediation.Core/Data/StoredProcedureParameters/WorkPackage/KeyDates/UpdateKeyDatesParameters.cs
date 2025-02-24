namespace HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.KeyDates;

public class UpdateKeyDatesParameters
{
    public DateTime? StartDate { get; set; }
    
    public DateTime? UnsafeCladdingRemovalDate { get; set; }
    
    public DateTime? ExpectedDateForCompletion { get; set; }
}