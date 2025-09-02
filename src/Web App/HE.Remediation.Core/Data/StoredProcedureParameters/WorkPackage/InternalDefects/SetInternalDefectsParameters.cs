namespace HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.InternalDefects;

public class SetInternalDefectsParameters
{
    public Guid ApplicationId { get; set; }
    public decimal? InternalDefectsCosts { get; set; }
    public string Description { get; set; }
}
