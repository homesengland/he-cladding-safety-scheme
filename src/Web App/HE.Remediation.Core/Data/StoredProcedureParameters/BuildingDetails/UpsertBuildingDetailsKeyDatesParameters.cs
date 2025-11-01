namespace HE.Remediation.Core.Data.StoredProcedureParameters.BuildingDetails;

public class UpdateBuildingDetailsKeyDatesParameters
{
    public Guid ApplicationId { get; set; }

    public DateTime? StartDate { get; set; }
    
    public DateTime? UnsafeCladdingRemovalDate { get; set; }
    
    public DateTime? ExpectedDateForCompletion { get; set; }
}